using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swadify_API.DTOs;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;
using System.Security.Claims;

namespace Swadify_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _service;
        private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public DeliveryController(IDeliveryService service) => _service = service;

        /// <summary>Update delivery partner location (real-time)</summary>
        [HttpPost("location")]
        [Authorize(Roles = "DeliveryPartner")]
        public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationDto dto)
        {
            await _service.UpdateLocationAsync(UserId, dto);
            return Ok(ApiResponse<object>.Ok(null!, "Location updated."));
        }

        /// <summary>Get current active assignment</summary>
        [HttpGet("current-assignment")]
        [Authorize(Roles = "DeliveryPartner")]
        public async Task<IActionResult> GetCurrentAssignment()
        {
            var result = await _service.GetCurrentAssignmentAsync(UserId);
            if (result == null) return Ok(ApiResponse<DeliveryAssignmentDto>.Ok(null!, "No active assignment."));
            return Ok(ApiResponse<DeliveryAssignmentDto>.Ok(result));
        }

        /// <summary>Get delivery history</summary>
        [HttpGet("history")]
        [Authorize(Roles = "DeliveryPartner")]
        public async Task<IActionResult> GetHistory([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAssignmentHistoryAsync(UserId, page, pageSize);
            return Ok(result);
        }

        /// <summary>Update availability status</summary>
        [HttpPatch("status")]
        [Authorize(Roles = "DeliveryPartner")]
        public async Task<IActionResult> UpdateStatus([FromBody] DeliveryPartnerStatusDto dto)
        {
            await _service.UpdateStatusAsync(UserId, dto);
            return Ok(ApiResponse<object>.Ok(null!, "Status updated."));
        }

        /// <summary>Get live delivery partner location for an order</summary>
        [HttpGet("track/{orderId:int}")]
        [Authorize(Roles = "Customer,Admin,SuperAdmin")]
        public async Task<IActionResult> TrackDelivery(int orderId)
        {
            var result = await _service.GetDeliveryPartnerLocationAsync(orderId, UserId);
            if (result == null) return NotFound(ApiResponse<LocationResponseDto>.NotFound("Location not available."));
            return Ok(ApiResponse<LocationResponseDto>.Ok(result));
        }
    }
}
