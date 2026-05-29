using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Interfaces;
using System.Security.Claims;

namespace Swadify_API.Controllers.DeliveryPartner
{
    [ApiController]
    [Route("api/delivery-partner")]
    [Authorize(Roles = "DeliveryPartner")]
    public class DPDashboardController(AppDbContext context, INotificationService notifications) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly INotificationService _notifications = notifications;

        [HttpGet("dashboard/active-deliveries")]
        public async Task<IActionResult> GetActiveDeliveries()
        {
            var activeDeliveries = await _context.Orders
                .Include(o => o.Restaurant)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)!
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.DeliveryPartnerId == null && o.Status == Enums.OrderStatus.ReadyForPickup)
                .Select(o => new
                {
                    id = o.Id.ToString(),
                    customerId = o.CustomerId.ToString(),
                    customerName = o.Customer != null ? $"{o.Customer.FirstName} {o.Customer.LastName}".Trim() : "",
                    orderNumber = o.OrderNumber,
                    status = o.Status.ToString(),
                    paymentMethod = o.PaymentMethod.ToString(),
                    paymentStatus = o.PaymentStatus.ToString(),
                    subtotal = o.SubTotal,
                    deliveryFee = o.DeliveryFee,
                    discount = o.DiscountAmount,
                    total = o.TotalAmount,
                    otp = o.UniqueDeliveryCode,
                    cancelReason = o.CancellationReason,
                    createdAt = o.CreatedAt,
                    updatedAt = o.UpdatedAt,
                    deliveryPartnerId = o.DeliveryPartnerId != null ? o.DeliveryPartnerId.ToString() : null,
                    deliveryPartnerName = "",
                    restaurant = new
                    {
                        id = o.Restaurant.Id.ToString(),
                        name = o.Restaurant.Name,
                        logoUrl = o.Restaurant.LogoUrl
                    },
                    deliveryAddress = new
                    {
                        line1 = o.DeliveryAddressLine1,
                        line2 = o.DeliveryAddressLine2,
                        city = o.DeliveryCity,
                        state = o.DeliveryState,
                        pincode = o.DeliveryPinCode,
                        lat = o.DeliveryLatitude,
                        lng = o.DeliveryLongitude
                    },
                    items = o.OrderItems.Select(oi => new
                    {
                        quantity = oi.Quantity,
                        menuItem = new
                        {
                            id = oi.MenuItem.Id.ToString(),
                            categoryId = oi.MenuItem.CategoryId.ToString(),
                            restaurantId = oi.MenuItem.RestaurantId.ToString(),
                            name = oi.MenuItem.Name,
                            description = oi.MenuItem.Description,
                            price = oi.UnitPrice,
                            imageUrl = oi.MenuItem.ImageUrl,
                            isVeg = oi.MenuItem.IsVegetarian,
                            isAvailable = oi.MenuItem.IsAvailable,
                            preparationTimeMin = oi.MenuItem.PreparationTimeMinutes
                        }
                    })
                })
                .ToListAsync();
            return Ok(activeDeliveries);
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var deliveryPartner = await _context.Users.Include(x => x.DeliveryProfile).FirstOrDefaultAsync(x => x.Id == id);
            if (deliveryPartner == null)
            {
                return NotFound();
            }
            if (deliveryPartner.DeliveryProfile == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                id = deliveryPartner.Id,
                firstName = deliveryPartner.FirstName,
                lastName = deliveryPartner.LastName,
                email = deliveryPartner.Email,
                phoneNumber = deliveryPartner.PhoneNumber,
                isOnline = deliveryPartner.DeliveryProfile.IsOnline,
                isAvailable = deliveryPartner.DeliveryProfile.IsAvailable
            });
        }

        [HttpGet("orders/get-my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var deliveryPartnerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var assignedOrders = await _context.Orders
                .Include(o => o.Restaurant)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)!
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.DeliveryPartnerId == deliveryPartnerId)
                .Select(o => new
                {
                    id = o.Id.ToString(),
                    customerId = o.CustomerId.ToString(),
                    customerName = o.Customer != null ? $"{o.Customer.FirstName} {o.Customer.LastName}".Trim() : "",
                    orderNumber = o.OrderNumber,
                    status = o.Status.ToString(),
                    DeliveryAssignmentStatus = o.DeliveryAssignmentStatus.ToString(),
                    paymentMethod = o.PaymentMethod.ToString(),
                    paymentStatus = o.PaymentStatus.ToString(),
                    subtotal = o.SubTotal,
                    deliveryFee = o.DeliveryFee,
                    discount = o.DiscountAmount,
                    total = o.TotalAmount,
                    otp = o.UniqueDeliveryCode,
                    cancelReason = o.CancellationReason,
                    createdAt = o.CreatedAt,
                    updatedAt = o.UpdatedAt,
                    deliveryPartnerId = o.DeliveryPartnerId != null ? o.DeliveryPartnerId.ToString() : null,
                    deliveryPartnerName = "",
                    restaurant = new
                    {
                        id = o.Restaurant.Id.ToString(),
                        name = o.Restaurant.Name,
                        logoUrl = o.Restaurant.LogoUrl
                    },
                    deliveryAddress = new
                    {
                        line1 = o.DeliveryAddressLine1,
                        line2 = o.DeliveryAddressLine2,
                        city = o.DeliveryCity,
                        state = o.DeliveryState,
                        pincode = o.DeliveryPinCode,
                        lat = o.DeliveryLatitude,
                        lng = o.DeliveryLongitude
                    },
                    items = o.OrderItems.Select(oi => new
                    {
                        quantity = oi.Quantity,
                        menuItem = new
                        {
                            id = oi.MenuItem.Id.ToString(),
                            categoryId = oi.MenuItem.CategoryId.ToString(),
                            restaurantId = oi.MenuItem.RestaurantId.ToString(),
                            name = oi.MenuItem.Name,
                            description = oi.MenuItem.Description,
                            price = oi.UnitPrice,
                            imageUrl = oi.MenuItem.ImageUrl,
                            isVeg = oi.MenuItem.IsVegetarian,
                            isAvailable = oi.MenuItem.IsAvailable,
                            preparationTimeMin = oi.MenuItem.PreparationTimeMinutes
                        }
                    })
                })
                .ToListAsync();
            return Ok(assignedOrders);
        }

        [HttpGet("earnings")]
        public async Task<IActionResult> GetEarningsAsync([FromQuery] string period)
        {
            var deliveryPartnerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            DateTime fromDate = period.ToLower() switch
            {
                "today" => DateTime.UtcNow.Date,

                "week" => DateTime.UtcNow.AddDays(-7),

                "month" => DateTime.UtcNow.AddMonths(-1),

                "year" => DateTime.UtcNow.AddYears(-1),

                _ => DateTime.UtcNow.AddDays(-7)
            };

            var earnings = await _context.DeliveryPartnerEarnings
                .Where(x =>
                    x.DeliveryPartnerId == deliveryPartnerId &&
                    x.EarnedAt >= fromDate)
                .OrderByDescending(x => x.EarnedAt)
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = earnings
            });
        }




        [HttpPatch("toggle/{id}")]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var deliveryPartner = await _context.Users.Include(x => x.DeliveryProfile).FirstOrDefaultAsync(x => x.Id == id);
            if (deliveryPartner == null)
            {
                return NotFound();
            }

            if (deliveryPartner.DeliveryProfile == null)
            {
                return NotFound();
            }

            deliveryPartner.DeliveryProfile.IsOnline = !deliveryPartner.DeliveryProfile.IsOnline;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = deliveryPartner.Id,

                isOnline = deliveryPartner.DeliveryProfile.IsOnline,

                isAvailable = deliveryPartner.DeliveryProfile.IsAvailable
            });
        }

        [HttpPatch("orders/accept-order/{orderId}")]
        public async Task<IActionResult> AcceptOrder(int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound(new
                {
                    message = "Order not found"
                });
            }

            var deliveryPartnerId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"
            );

            // ─────────────────────────────────────────────
            // ADMIN ASSIGNED FLOW
            // ─────────────────────────────────────────────

            if (
                order.DeliveryPartnerId == deliveryPartnerId &&
                order.DeliveryAssignmentStatus ==
                    DeliveryAssignmentStatus.Pending
            )
            {
                order.DeliveryAssignmentStatus =
                    DeliveryAssignmentStatus.Accepted;

                order.Status = OrderStatus.AssignedToDelivery;

                order.DeliveryAcceptedAt = DateTime.UtcNow;

                var profile = await _context.DeliveryPartnerProfiles
                    .FirstOrDefaultAsync(x => x.UserId == deliveryPartnerId);

                if (profile != null)
                {
                    profile.IsAvailable = false;
                }

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Assigned order accepted successfully"
                });
            }

            // ─────────────────────────────────────────────
            // OPEN MARKETPLACE FLOW
            // ─────────────────────────────────────────────

            if (
                order.DeliveryPartnerId == null &&
                order.Status == OrderStatus.ReadyForPickup
            )
            {
                order.DeliveryPartnerId = deliveryPartnerId;

                order.Status = OrderStatus.AssignedToDelivery;

                order.DeliveryAssignmentStatus =
                    DeliveryAssignmentStatus.Accepted;

                order.DeliveryAcceptedAt = DateTime.UtcNow;

                var profile = await _context.DeliveryPartnerProfiles
                    .FirstOrDefaultAsync(x => x.UserId == deliveryPartnerId);

                if (profile != null)
                {
                    profile.IsAvailable = false;
                }

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Order accepted successfully"
                });
            }

            return BadRequest(new
            {
                message = "Order cannot be accepted"
            });
        }

        [HttpPatch("orders/reject-assignment/{orderId}")]
        public async Task<IActionResult> RejectAssignment(int orderId)
        {
            var deliveryPartnerId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"
            );

            var order = await _context.Orders
                .FirstOrDefaultAsync(o =>
                    o.Id == orderId &&
                    o.DeliveryPartnerId == deliveryPartnerId);

            if (order == null)
            {
                return NotFound(new
                {
                    message = "Order not found"
                });
            }

            if (
                order.DeliveryAssignmentStatus !=
                DeliveryAssignmentStatus.Pending
            )
            {
                return BadRequest(new
                {
                    message = "Assignment already processed"
                });
            }

            order.DeliveryPartnerId = null;

            order.DeliveryAssignmentStatus =
                DeliveryAssignmentStatus.Rejected;

            order.DeliveryRejectedAt = DateTime.UtcNow;

            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Notify admin that assignment was rejected

            var adminId = await _context.Restaurants
                .Where(r => r.Id == order.RestaurantId)
                .Select(r => r.OwnerId)
                .FirstOrDefaultAsync();

            await _notifications.SendNotificationAsync(
                adminId,
                "Delivery Assignment Rejected",
                $"Delivery partner rejected Order #{order.OrderNumber}",
                NotificationType.General,
                order.Id
            );

            return Ok(new
            {
                message = "Assignment rejected successfully"
            });
        }

        [HttpPatch("orders/update-status/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusDto2 dto)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound(new
                {
                    message = "Order not found"
                });
            }

            var deliveryPartnerId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"
            );

            if (order.DeliveryPartnerId != deliveryPartnerId)
            {
                return BadRequest(new
                {
                    message = "You are not assigned to this order"
                });
            }

            // Parse enum safely
            if (!Enum.TryParse<Enums.OrderStatus>(
                dto.Status,
                true,
                out var parsedStatus))
            {
                return BadRequest(new
                {
                    message = "Invalid order status"
                });
            }

            // Allow only delivery partner statuses
            var allowedStatuses = new[]
            {
                Enums.OrderStatus.OutForDelivery,
                Enums.OrderStatus.Delivered,
                Enums.OrderStatus.Cancelled,
                Enums.OrderStatus.Failed
            };

            if (!allowedStatuses.Contains(parsedStatus))
            {
                return BadRequest(new
                {
                    message = "Status update not allowed"
                });
            }

            order.Status = parsedStatus;
            order.UpdatedAt = DateTime.UtcNow;

            // ─────────────────────────────────────────────
            // CREATE EARNING ONLY WHEN DELIVERED
            // ─────────────────────────────────────────────

            if (parsedStatus == Enums.OrderStatus.Delivered)
            {
                // Prevent duplicate earnings
                var earningExists = await _context.DeliveryPartnerEarnings
                    .AnyAsync(x => x.OrderId == order.Id);

                if (!earningExists)
                {
                    // Calculate earning
                    decimal earningAmount =
                        Math.Round((order.TotalAmount * 0.08m) + 15m, 2);

                    // Create earning entry
                    var earning = new DeliveryPartnerEarning
                    {
                        DeliveryPartnerId = deliveryPartnerId,
                        OrderId = order.Id,

                        Amount = earningAmount,
                        DeliveryFee = earningAmount,
                        NetAmount = earningAmount,

                        EarnedAt = DateTime.UtcNow
                    };

                    _context.DeliveryPartnerEarnings.Add(earning);

                    // Update delivery profile
                    var profile = await _context.DeliveryPartnerProfiles
                        .FirstOrDefaultAsync(x => x.UserId == deliveryPartnerId);

                    if (profile != null)
                    {
                        profile.TotalDeliveries += 1;
                        profile.TotalEarnings += earningAmount;
                        profile.PendingEarnings += earningAmount;
                        profile.IsAvailable = true;
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = $"Order status updated to {parsedStatus}",
                status = parsedStatus.ToString()
            });
        }
    }

    public class UpdateOrderStatusDto2
    {
        public string Status { get; set; } = string.Empty;
    }
}
