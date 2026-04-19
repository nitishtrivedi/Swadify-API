using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Helpers;
using Swadify_API.Hubs;
using Swadify_API.Interfaces;

namespace Swadify_API.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<DeliveryHub> _deliveryHub;

        public DeliveryService(AppDbContext db, IHubContext<DeliveryHub> deliveryHub)
        {
            _db = db;
            _deliveryHub = deliveryHub;
        }

        public async Task<bool> UpdateLocationAsync(int deliveryPartnerId, UpdateLocationDto dto)
        {
            var profile = await _db.DeliveryPartnerProfiles.FirstOrDefaultAsync(p => p.UserId == deliveryPartnerId)
                ?? throw new KeyNotFoundException("Delivery partner profile not found.");

            profile.CurrentLatitude = dto.Latitude;
            profile.CurrentLongitude = dto.Longitude;
            profile.LastLocationUpdate = DateTime.UtcNow;

            // Store tracking record
            var activeOrder = await _db.Orders.FirstOrDefaultAsync(o =>
                o.DeliveryPartnerId == deliveryPartnerId &&
                o.Status == OrderStatus.OutForDelivery);

            if (activeOrder != null)
            {
                _db.DeliveryTrackings.Add(new DeliveryTracking
                {
                    OrderId = activeOrder.Id,
                    DeliveryPartnerId = deliveryPartnerId,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    SpeedKmh = dto.SpeedKmh
                });

                // Broadcast location update to customer
                await _deliveryHub.Clients.Group($"order-{activeOrder.Id}")
                    .SendAsync("LocationUpdate", new
                    {
                        OrderId = activeOrder.Id,
                        Latitude = dto.Latitude,
                        Longitude = dto.Longitude,
                        Timestamp = DateTime.UtcNow
                    });
            }

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<DeliveryAssignmentDto?> GetCurrentAssignmentAsync(int deliveryPartnerId)
        {
            var order = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(o =>
                    o.DeliveryPartnerId == deliveryPartnerId &&
                    (o.Status == OrderStatus.AssignedToDelivery || o.Status == OrderStatus.OutForDelivery));

            if (order == null) return null;
            return MapToAssignmentDto(order);
        }

        public async Task<PagedResponse<DeliveryAssignmentDto>> GetAssignmentHistoryAsync(int deliveryPartnerId, int page, int pageSize)
        {
            var query = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Restaurant)
                .Where(o => o.DeliveryPartnerId == deliveryPartnerId &&
                            (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Failed));

            var total = await query.CountAsync();
            var orders = await query.OrderByDescending(o => o.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<DeliveryAssignmentDto>
            {
                Data = orders.Select(MapToAssignmentDto),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<bool> UpdateStatusAsync(int deliveryPartnerId, DeliveryPartnerStatusDto dto)
        {
            var profile = await _db.DeliveryPartnerProfiles.FirstOrDefaultAsync(p => p.UserId == deliveryPartnerId)
                ?? throw new KeyNotFoundException("Delivery partner profile not found.");

            profile.IsAvailable = dto.IsAvailable;
            profile.IsOnline = dto.IsOnline;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<LocationResponseDto?> GetDeliveryPartnerLocationAsync(int orderId, int requesterId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o =>
                o.Id == orderId && (o.CustomerId == requesterId || o.RestaurantId != 0));

            if (order?.DeliveryPartnerId == null) return null;

            var profile = await _db.DeliveryPartnerProfiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == order.DeliveryPartnerId);

            if (profile?.CurrentLatitude == null) return null;

            return new LocationResponseDto
            {
                DeliveryPartnerId = profile.UserId,
                DeliveryPartnerName = $"{profile.User!.FirstName} {profile.User.LastName}",
                Latitude = profile.CurrentLatitude.Value,
                Longitude = profile.CurrentLongitude!.Value,
                LastUpdated = profile.LastLocationUpdate ?? DateTime.UtcNow
            };
        }

        private static DeliveryAssignmentDto MapToAssignmentDto(Order o) => new()
        {
            OrderId = o.Id,
            OrderNumber = o.OrderNumber,
            UniqueDeliveryCode = o.UniqueDeliveryCode,
            CustomerName = o.Customer != null ? $"{o.Customer.FirstName} {o.Customer.LastName}" : string.Empty,
            CustomerPhone = o.Customer?.PhoneNumber ?? string.Empty,
            DeliveryAddress = $"{o.DeliveryAddressLine1}, {o.DeliveryCity}, {o.DeliveryState} - {o.DeliveryPinCode}",
            DeliveryLatitude = o.DeliveryLatitude,
            DeliveryLongitude = o.DeliveryLongitude,
            TotalAmount = o.TotalAmount,
            PaymentMethod = o.PaymentMethod.ToString(),
            IsPrepaid = o.PaymentMethod == PaymentMethod.Online && o.PaymentStatus == PaymentStatus.Completed,
            RestaurantName = o.Restaurant?.Name ?? string.Empty,
            RestaurantAddress = o.Restaurant != null ? $"{o.Restaurant.AddressLine1}, {o.Restaurant.City}" : string.Empty,
            SpecialInstructions = o.SpecialInstructions
        };
    }
}
