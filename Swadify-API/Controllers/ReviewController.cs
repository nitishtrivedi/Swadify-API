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
    [Produces("application/json")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service) => _service = service;

        /// <summary>Get reviews for a restaurant</summary>
        [HttpGet("restaurant/{restaurantId:int}")]
        public async Task<IActionResult> GetRestaurantReviews(int restaurantId,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetRestaurantReviewsAsync(restaurantId, page, pageSize);
            return Ok(result);
        }

        /// <summary>Create a review for a completed order</summary>
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
        {
            var customerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.CreateReviewAsync(customerId, dto);
            return CreatedAtAction(nameof(Create), ApiResponse<ReviewResponseDto>.Created(result, "Review submitted."));
        }

        /// <summary>Admin reply to a review</summary>
        [HttpPost("{reviewId:int}/reply")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Reply(int reviewId, [FromBody] AdminReplyDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.AdminReplyAsync(reviewId, adminId, dto);
            return Ok(ApiResponse<ReviewResponseDto>.Ok(result, "Reply added."));
        }

        /// <summary>Toggle review approval</summary>
        [HttpPatch("{reviewId:int}/toggle-approval")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ToggleApproval(int reviewId)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isApproved = await _service.ToggleApprovalAsync(reviewId, adminId);
            return Ok(ApiResponse<object>.Ok(new { isApproved }, isApproved ? "Review approved." : "Review hidden."));
        }
    }
}
