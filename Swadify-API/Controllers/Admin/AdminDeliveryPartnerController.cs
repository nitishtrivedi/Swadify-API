using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Enums;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;

namespace Swadify_API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/delivery-partners")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Produces("application/json")]
    public class AdminDeliveryPartnerController(AppDbContext context, INotificationService notifications) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly INotificationService _notifications = notifications;

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var query = _context.DeliveryPartnerProfiles
                .Include(p => p.User)
                .AsQueryable();
            var total = await query.CountAsync();

            var deliveryPartners = await query.OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(dp => new DeliveryPartnerResponseDto
                {
                    Id = dp.UserId.ToString(),
                    FirstName = dp.User!.FirstName,
                    LastName = dp.User.LastName,
                    Phone = dp.User.PhoneNumber ?? string.Empty,
                    IsAvailable = dp.IsAvailable,
                    IsOnline = dp.IsOnline,
                    Rating = dp.AverageRating,
                    TotalDeliveries = dp.TotalDeliveries,
                    VehicleNumber = dp.VehicleNumber,
                    LicenseNumber = dp.LicenseNumber,
                    CurrentLocation = dp.CurrentLatitude != null && dp.CurrentLongitude != null ? new LocationDto
                    {
                        Lat = dp.CurrentLatitude.Value,
                        Lng = dp.CurrentLongitude.Value
                    } : null,
                    LastLocationUpdate = dp.LastLocationUpdate,
                    IsActive = dp.User.IsActive,
                    Email = dp.User.Email,
                    CreatedAt = dp.CreatedAt
                }).ToListAsync();
            return Ok(new PagedResponse<DeliveryPartnerResponseDto>
            {
                Data = deliveryPartners,
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            });
        }

        [HttpGet("get-available")]
        public async Task<IActionResult> GetAvailableAsync()
        {
            var availableDeliveryPartners = await _context.DeliveryPartnerProfiles
                .Include(p => p.User)
                .Where(p => p.IsAvailable && p.User!.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .Select(dp => new DeliveryPartnerResponseDto
                {
                    Id = dp.UserId.ToString(),
                    FirstName = dp.User!.FirstName,
                    LastName = dp.User.LastName,
                    Phone = dp.User.PhoneNumber ?? string.Empty,
                    IsAvailable = dp.IsAvailable,
                    IsOnline = dp.IsOnline,
                    Rating = dp.AverageRating,
                    TotalDeliveries = dp.TotalDeliveries,
                    VehicleNumber = dp.VehicleNumber,
                    LicenseNumber = dp.LicenseNumber,
                    CurrentLocation = dp.CurrentLatitude != null && dp.CurrentLongitude != null ? new LocationDto
                    {
                        Lat = dp.CurrentLatitude.Value,
                        Lng = dp.CurrentLongitude.Value
                    } : null,
                }).ToListAsync();
            return Ok(availableDeliveryPartners);
        }

        [HttpPatch("assign-delivery-partner/{orderId}")]
        public async Task<IActionResult> AssignDeliveryPartner(
    int orderId,
    [FromBody] AssignDeliveryPartnerDto dto)
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

            // Only assign if ready for pickup
            if (order.Status != OrderStatus.ReadyForPickup)
            {
                return BadRequest(new
                {
                    message = "Order is not ready for pickup"
                });
            }

            // Prevent duplicate pending assignment
            if (
                order.DeliveryPartnerId != null &&
                order.DeliveryAssignmentStatus ==
                    DeliveryAssignmentStatus.Pending
            )
            {
                return BadRequest(new
                {
                    message = "Delivery assignment already pending"
                });
            }

            // Find DP
            var deliveryPartner = await _context.Users
                .Include(x => x.DeliveryProfile)
                .FirstOrDefaultAsync(x =>
                    x.Id == dto.DeliveryPartnerId &&
                    x.Role == UserRole.DeliveryPartner);

            if (deliveryPartner == null)
            {
                return NotFound(new
                {
                    message = "Delivery partner not found"
                });
            }

            if (deliveryPartner.DeliveryProfile == null)
            {
                return BadRequest(new
                {
                    message = "Delivery profile not found"
                });
            }

            if (!deliveryPartner.DeliveryProfile.IsAvailable)
            {
                return BadRequest(new
                {
                    message = "Delivery partner is not available"
                });
            }

            // ONLY SEND REQUEST
            order.DeliveryPartnerId = dto.DeliveryPartnerId;

            order.DeliveryAssignmentStatus =
                DeliveryAssignmentStatus.Pending;

            order.DeliveryAssignedAt = DateTime.UtcNow;

            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Notify DP
            await _notifications.SendNotificationAsync(
                deliveryPartner.Id,
                "New Delivery Request",
                $"Order #{order.OrderNumber} has been assigned to you.",
                NotificationType.General,
                order.Id
            );

            return Ok(new
            {
                message = "Assignment request sent successfully",
                orderId = order.Id,
                deliveryPartnerId = deliveryPartner.Id
            });
        }
    }
}
