using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Helpers;

namespace Swadify_API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/delivery-partners")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    [Produces("application/json")]
    public class AdminDeliveryPartnerController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

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
                    CurrentLocation = dp.CurrentLatitude!= null && dp.CurrentLongitude != null ? new LocationDto
                    {
                        Lat = dp.CurrentLatitude.Value,
                        Lng= dp.CurrentLongitude.Value
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
    }
}
