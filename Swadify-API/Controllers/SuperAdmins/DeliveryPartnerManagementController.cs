using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Enums;
using Swadify_API.Helpers;

namespace Swadify_API.Controllers.SuperAdmins
{
    [ApiController]
    [Route("api/super-admins/delivery-partners")]
    [Authorize(Roles ="SuperAdmin")]
    public class DeliveryPartnerManagementController(AppDbContext context, ILogger<DeliveryPartnerManagementController> logger) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<DeliveryPartnerManagementController> _logger = logger;

        [HttpGet]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetDeliveryPartners([FromQuery] string? status)
        {
            var query = _context.DeliveryPartnerProfiles
                .Include(dp => dp.User)
                .AsQueryable();

            // Filter by status if provided
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<ApplicationStatus>(status, out var appStatus))
            {
                query = query.Where(dp => dp.ApplicationStatus == appStatus);
            }

            var partners = await query
                .OrderByDescending(dp => dp.CreatedAt)
                .Select(dp => new DeliveryPartnerApplicationDto
                {
                    Id = dp.Id,
                    UserId = dp.UserId,
                    FirstName = dp.User!.FirstName,
                    LastName = dp.User.LastName,
                    Username = dp.User.Username,
                    Email = dp.User.Email,
                    PhoneNumber = dp.User.PhoneNumber ?? string.Empty,
                    VehicleType = dp.VehicleType,
                    VehicleNumber = dp.VehicleNumber,
                    LicenseNumber = dp.LicenseNumber,
                    AadharNumber = dp.AadharNumber,
                    ApplicationStatus = dp.ApplicationStatus,
                    RejectionReason = dp.RejectionReason,
                    CreatedAt = dp.CreatedAt
                })
                .ToListAsync();
            return Ok(partners);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingApplications()
        {
            var pendingPartners = await _context.DeliveryPartnerProfiles
               .Include(dp => dp.User)
               .Where(dp => dp.ApplicationStatus == ApplicationStatus.Pending)
               .OrderByDescending(dp => dp.CreatedAt)
               .Select(dp => new DeliveryPartnerApplicationDto
               {
                   Id = dp.Id,
                   UserId = dp.UserId,
                   FirstName = dp.User!.FirstName,
                   LastName = dp.User.LastName,
                   Username = dp.User.Username,
                   Email = dp.User.Email,
                   PhoneNumber = dp.User.PhoneNumber ?? string.Empty,
                   VehicleType = dp.VehicleType,
                   VehicleNumber = dp.VehicleNumber,
                   LicenseNumber = dp.LicenseNumber,
                   AadharNumber = dp.AadharNumber,
                   ApplicationStatus = dp.ApplicationStatus,
                   RejectionReason = dp.RejectionReason,
                   CreatedAt = dp.CreatedAt
               })
               .ToListAsync();
            return Ok(pendingPartners);
        }

        [HttpPatch("{id}/approve")]
        public async Task<IActionResult> ApproveDeliveryPartner(int id)
        {
            var profile = await _context.DeliveryPartnerProfiles
                .Include(dp => dp.User)
                .FirstOrDefaultAsync(dp => dp.Id == id);

            if (profile == null)
                return NotFound("Delivery partner application not found.");

            if (profile.ApplicationStatus != ApplicationStatus.Pending)
                return BadRequest($"Application is already {profile.ApplicationStatus}.");

            // Update application status and activate user
            profile.ApplicationStatus = ApplicationStatus.Approved;
            profile.User!.IsActive = true;
            profile.User.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Delivery partner approved: {Email}", profile.User.Email);

            var result = new DeliveryPartnerApplicationDto
            {
                Id = profile.Id,
                UserId = profile.UserId,
                FirstName = profile.User.FirstName,
                LastName = profile.User.LastName,
                Username = profile.User.Username,
                Email = profile.User.Email,
                PhoneNumber = profile.User.PhoneNumber ?? string.Empty,
                VehicleType = profile.VehicleType,
                VehicleNumber = profile.VehicleNumber,
                LicenseNumber = profile.LicenseNumber,
                AadharNumber = profile.AadharNumber,
                ApplicationStatus = profile.ApplicationStatus,
                RejectionReason = profile.RejectionReason,
                CreatedAt = profile.CreatedAt
            };

            return Ok(result);
        }

        [HttpPatch("{id}/reject")]
        public async Task<IActionResult> RejectDeliveryPartner(int id, [FromBody] RejectDeliveryPartnerDto dto)
        {
            var profile = await _context.DeliveryPartnerProfiles
                .Include(dp => dp.User)
                .FirstOrDefaultAsync(dp => dp.Id == id);

            if (profile == null)
                return NotFound(ApiResponse<object>.NotFound("Delivery partner application not found."));

            if (profile.ApplicationStatus != ApplicationStatus.Pending)
                return BadRequest($"Application is already {profile.ApplicationStatus}.");

            // Update application status and rejection reason
            profile.ApplicationStatus = ApplicationStatus.Rejected;
            profile.RejectionReason = dto.RejectionReason;
            profile.User!.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Delivery partner application rejected: {Email}", profile.User.Email);

            var result = new DeliveryPartnerApplicationDto
            {
                Id = profile.Id,
                UserId = profile.UserId,
                FirstName = profile.User.FirstName,
                LastName = profile.User.LastName,
                Username = profile.User.Username,
                Email = profile.User.Email,
                PhoneNumber = profile.User.PhoneNumber ?? string.Empty,
                VehicleType = profile.VehicleType,
                VehicleNumber = profile.VehicleNumber,
                LicenseNumber = profile.LicenseNumber,
                AadharNumber = profile.AadharNumber,
                ApplicationStatus = profile.ApplicationStatus,
                RejectionReason = profile.RejectionReason,
                CreatedAt = profile.CreatedAt
            };
            return Ok(new {message = $"Application for ID: {profile.Id} rejected successfully", data = result});
        }

    }
}
