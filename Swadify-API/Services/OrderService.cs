using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;

namespace Swadify_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly INotificationService _notifications;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext db, INotificationService notifications, ILogger<OrderService> logger)
        {
            _db = db;
            _notifications = notifications;
            _logger = logger;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(int customerId, CreateOrderDto dto)
        {
            var cart = await _db.Carts
                .Include(c => c.CartItems!).ThenInclude(ci => ci.MenuItem)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId)
                ?? throw new InvalidOperationException("Cart is empty.");

            if (!cart.CartItems!.Any())
                throw new InvalidOperationException("Cart is empty.");

            var restaurant = await _db.Restaurants.FindAsync(dto.RestaurantId)
                ?? throw new KeyNotFoundException("Restaurant not found.");

            if (restaurant.Status != RestaurantStatus.Open)
                throw new InvalidOperationException("Restaurant is currently closed.");

            // Validate cart belongs to this restaurant
            if (cart.RestaurantId != dto.RestaurantId)
                throw new InvalidOperationException("Cart items are from a different restaurant.");

            decimal subTotal = cart.CartItems!.Sum(ci => ci.UnitPrice * ci.Quantity);

            if (subTotal < restaurant.MinimumOrderAmount)
                throw new InvalidOperationException($"Minimum order amount is ₹{restaurant.MinimumOrderAmount}.");

            decimal taxAmount = Math.Round(subTotal * 0.05m, 2); // 5% tax
            decimal totalAmount = subTotal + restaurant.DeliveryFee + taxAmount;


            var order = new Order
            {
                CustomerId = customerId,
                RestaurantId = dto.RestaurantId,
                OrderNumber = OrderNumberHelper.Generate(),
                UniqueDeliveryCode = OrderNumberHelper.GenerateDeliveryCode(),
                Status = OrderStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = dto.PaymentMethod,
                SubTotal = subTotal,
                DeliveryFee = restaurant.DeliveryFee,
                TaxAmount = taxAmount,
                TotalAmount = totalAmount,
                DeliveryAddressLine1 = dto.DeliveryAddressLine1,
                DeliveryAddressLine2 = dto.DeliveryAddressLine2,
                DeliveryCity = dto.DeliveryCity,
                DeliveryState = dto.DeliveryState,
                DeliveryPinCode = dto.DeliveryPinCode,
                DeliveryLatitude = dto.DeliveryLatitude,
                DeliveryLongitude = dto.DeliveryLongitude,
                SpecialInstructions = dto.SpecialInstructions,
                EstimatedDeliveryTime = DateTime.UtcNow.AddMinutes(restaurant.EstimatedDeliveryTimeMinutes)
            };

            _db.Orders.Add(order);

            foreach (var cartItem in cart.CartItems!)
            {
                _db.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    MenuItemId = cartItem.MenuItemId,
                    ItemName = cartItem.MenuItem!.Name,
                    ItemDescription = cartItem.MenuItem.Description,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice,
                    TotalPrice = cartItem.UnitPrice * cartItem.Quantity,
                    SpecialInstructions = cartItem.SpecialInstructions
                });
            }

            // Create payment record
            _db.Payments.Add(new Payment
            {
                OrderId = order.Id,
                Amount = totalAmount,
                Method = dto.PaymentMethod,
                Status = PaymentStatus.Pending
            });

            // Clear cart
            _db.Carts.Remove(cart);

            await _db.SaveChangesAsync();

            // Notify restaurant owner
            await _notifications.SendNotificationAsync(
                restaurant.OwnerId,
                "New Order Received!",
                $"Order #{order.OrderNumber} for ₹{totalAmount} has been placed.",
                NotificationType.NewOrderAlert,
                order.Id);

            // Notify customer
            await _notifications.SendNotificationAsync(
                customerId,
                "Order Placed Successfully!",
                $"Your order #{order.OrderNumber} has been placed.",
                NotificationType.OrderPlaced,
                order.Id);

            _logger.LogInformation("Order created: {OrderNumber}", order.OrderNumber);
            return await GetOrderByIdAsync(order.Id, customerId, UserRole.Customer) ?? throw new Exception("Failed to retrieve order.");
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int orderId, int requesterId, UserRole requesterRole)
        {
            var order = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Restaurant)
                .Include(o => o.DeliveryPartner)
                .Include(o => o.OrderItems!)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            // Access control
            if (requesterRole == UserRole.Customer && order.CustomerId != requesterId) return null;
            if (requesterRole == UserRole.DeliveryPartner && order.DeliveryPartnerId != requesterId) return null;
            if (requesterRole == UserRole.Admin)
            {
                var adminOwnsRestaurant = await _db.Restaurants.AnyAsync(r => r.Id == order.RestaurantId && r.OwnerId == requesterId);
                if (!adminOwnsRestaurant) return null;
            }

            return MapToDto(order);
        }

        public async Task<PagedResponse<OrderResponseDto>> GetMyOrdersAsync(int customerId, int page, int pageSize, OrderStatus? status)
        {
            var query = _db.Orders
                .Include(o => o.Restaurant)
                .Include(o => o.OrderItems)
                .Where(o => o.CustomerId == customerId);

            if (status.HasValue) query = query.Where(o => o.Status == status.Value);

            var total = await query.CountAsync();
            var orders = await query.OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<OrderResponseDto>
            {
                Data = orders.Select(MapToDto),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<PagedResponse<OrderResponseDto>> GetRestaurantOrdersAsync(int restaurantId, int requesterId, int page, int pageSize, OrderStatus? status)
        {
            var requester = await _db.Users.FindAsync(requesterId)!;

            if (requester!.Role == UserRole.Admin)
            {
                var ownsIt = await _db.Restaurants.AnyAsync(r => r.Id == restaurantId && r.OwnerId == requesterId);
                if (!ownsIt) throw new UnauthorizedAccessException("Access denied.");
            }

            var query = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.DeliveryPartner)
                .Include(o => o.OrderItems)
                .Where(o => o.RestaurantId == restaurantId);

            if (status.HasValue) query = query.Where(o => o.Status == status.Value);

            var total = await query.CountAsync();
            var orders = await query.OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<OrderResponseDto>
            {
                Data = orders.Select(MapToDto),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<PagedResponse<OrderResponseDto>> GetAllOrdersAsync(int page, int pageSize, OrderStatus? status)
        {
            var query = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Restaurant)
                .Include(o => o.DeliveryPartner)
                .Include(o => o.OrderItems)
                .AsQueryable();

            if (status.HasValue) query = query.Where(o => o.Status == status.Value);

            var total = await query.CountAsync();
            var orders = await query.OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<OrderResponseDto>
            {
                Data = orders.Select(MapToDto),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<OrderResponseDto> UpdateOrderStatusAsync(int orderId, int requesterId, UserRole requesterRole, UpdateOrderStatusDto dto)
        {
            var order = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Restaurant)
                .Include(o => o.DeliveryPartner)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new KeyNotFoundException("Order not found.");

            // Access control
            if (requesterRole == UserRole.Admin)
            {
                var ownsIt = await _db.Restaurants.AnyAsync(r => r.Id == order.RestaurantId && r.OwnerId == requesterId);
                if (!ownsIt) throw new UnauthorizedAccessException("Access denied.");
            }
            else if (requesterRole == UserRole.DeliveryPartner)
            {
                if (order.DeliveryPartnerId != requesterId) throw new UnauthorizedAccessException("Access denied.");
                // Delivery partner can only mark Delivered or Failed
                if (dto.Status != OrderStatus.Delivered && dto.Status != OrderStatus.Failed)
                    throw new InvalidOperationException("Delivery partner can only mark order as Delivered or Failed.");
            }

            order.Status = dto.Status;
            if (!string.IsNullOrEmpty(dto.CancellationReason)) order.CancellationReason = dto.CancellationReason;
            if (dto.Status == OrderStatus.Delivered) order.ActualDeliveryTime = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            // Notify customer
            var notifType = dto.Status switch
            {
                OrderStatus.Confirmed => NotificationType.OrderConfirmed,
                OrderStatus.Preparing => NotificationType.OrderPreparing,
                OrderStatus.OutForDelivery => NotificationType.OrderOutForDelivery,
                OrderStatus.Delivered => NotificationType.OrderDelivered,
                OrderStatus.Cancelled => NotificationType.OrderCancelled,
                _ => NotificationType.General
            };

            await _notifications.SendNotificationAsync(order.CustomerId, $"Order {dto.Status}",
                $"Your order #{order.OrderNumber} is now {dto.Status}.", notifType, order.Id);

            _logger.LogInformation("Order {OrderNumber} status updated to {Status}", order.OrderNumber, dto.Status);
            return MapToDto(order);
        }

        public async Task<OrderResponseDto> AssignDeliveryPartnerAsync(int orderId, int requesterId, AssignDeliveryPartnerDto dto)
        {
            var order = await _db.Orders
                .Include(o => o.Restaurant)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new KeyNotFoundException("Order not found.");

            var requester = await _db.Users.FindAsync(requesterId)!;
            if (requester!.Role == UserRole.Admin)
            {
                var ownsIt = await _db.Restaurants.AnyAsync(r => r.Id == order.RestaurantId && r.OwnerId == requesterId);
                if (!ownsIt) throw new UnauthorizedAccessException("Access denied.");
            }

            var deliveryPartner = await _db.Users.FindAsync(dto.DeliveryPartnerId)
                ?? throw new KeyNotFoundException("Delivery partner not found.");

            if (deliveryPartner.Role != UserRole.DeliveryPartner)
                throw new InvalidOperationException("User is not a delivery partner.");

            order.DeliveryPartnerId = dto.DeliveryPartnerId;
            order.Status = OrderStatus.AssignedToDelivery;

            await _db.SaveChangesAsync();

            await _notifications.SendNotificationAsync(
                dto.DeliveryPartnerId,
                "New Delivery Assignment",
                $"You have been assigned order #{order.OrderNumber}. Delivery code: {order.UniqueDeliveryCode}",
                NotificationType.DeliveryAssigned,
                order.Id);

            _logger.LogInformation("Delivery partner {PartnerId} assigned to order {OrderNumber}", dto.DeliveryPartnerId, order.OrderNumber);
            return MapToDto(order);
        }

        public async Task<PagedResponse<OrderResponseDto>> GetDeliveryPartnerOrdersAsync(int deliveryPartnerId, int page, int pageSize, OrderStatus? status)
        {
            var query = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Restaurant)
                .Include(o => o.OrderItems)
                .Where(o => o.DeliveryPartnerId == deliveryPartnerId);

            if (status.HasValue) query = query.Where(o => o.Status == status.Value);

            var total = await query.CountAsync();
            var orders = await query.OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<OrderResponseDto>
            {
                Data = orders.Select(MapToDto),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        private static OrderResponseDto MapToDto(Order o)
        {
            return new OrderResponseDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                UniqueDeliveryCode = o.UniqueDeliveryCode,
                CustomerName = o.Customer != null ? $"{o.Customer.FirstName} {o.Customer.LastName}" : string.Empty,
                CustomerPhone = o.Customer?.PhoneNumber ?? string.Empty,
                RestaurantName = o.Restaurant?.Name ?? string.Empty,
                DeliveryPartnerName = o.DeliveryPartner != null ? $"{o.DeliveryPartner.FirstName} {o.DeliveryPartner.LastName}" : null,
                DeliveryPartnerPhone = o.DeliveryPartner?.PhoneNumber,
                Status = o.Status.ToString(),
                PaymentStatus = o.PaymentStatus.ToString(),
                PaymentMethod = o.PaymentMethod.ToString(),
                SubTotal = o.SubTotal,
                DeliveryFee = o.DeliveryFee,
                TaxAmount = o.TaxAmount,
                DiscountAmount = o.DiscountAmount,
                TotalAmount = o.TotalAmount,
                DeliveryAddress = $"{o.DeliveryAddressLine1}, {o.DeliveryCity}, {o.DeliveryState} - {o.DeliveryPinCode}",
                SpecialInstructions = o.SpecialInstructions,
                EstimatedDeliveryTime = o.EstimatedDeliveryTime,
                ActualDeliveryTime = o.ActualDeliveryTime,
                CancellationReason = o.CancellationReason,
                Items = o.OrderItems?.Select(oi => new OrderItemResponseDto
                {
                    Id = oi.Id,
                    MenuItemId = oi.MenuItemId,
                    ItemName = oi.ItemName,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice,
                    SpecialInstructions = oi.SpecialInstructions
                }).ToList() ?? [],
                CreatedAt = o.CreatedAt
            };
        }
    }
}
