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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service) => _service = service;

        /// <summary>Initiate Razorpay payment for an order</summary>
        [HttpPost("initiate")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Initiate([FromBody] InitiatePaymentDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.InitiatePaymentAsync(userId, dto);
            return Ok(ApiResponse<PaymentResponseDto>.Ok(result, "Payment initiated."));
        }

        /// <summary>Verify Razorpay payment after success callback</summary>
        [HttpPost("verify")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Verify([FromBody] VerifyPaymentDto dto)
        {
            var success = await _service.VerifyPaymentAsync(dto);
            if (!success) return BadRequest(ApiResponse<object>.Fail("Payment verification failed."));
            return Ok(ApiResponse<object>.Ok(null!, "Payment verified successfully."));
        }

        /// <summary>Razorpay webhook (no auth)</summary>
        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> Webhook()
        {
            var payload = await new StreamReader(Request.Body).ReadToEndAsync();
            var signature = Request.Headers["X-Razorpay-Signature"].ToString();
            var success = await _service.HandleWebhookAsync(payload, signature);
            return success ? Ok() : BadRequest();
        }
    }
}
