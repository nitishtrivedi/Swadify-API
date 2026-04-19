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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth) => _auth = auth;

        /// <summary>Register a new customer account</summary>
        [HttpPost("register/customer")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 201)]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegisterDto dto)
        {
            var result = await _auth.RegisterCustomerAsync(dto);
            return CreatedAtAction(nameof(RegisterCustomer), ApiResponse<AuthResponseDto>.Created(result, "Customer registered successfully."));
        }

        /// <summary>Register a new super admin account</summary>
        [HttpPost("register/super-admin")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 201)]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] SuperAdminRegisterDto dto)
        {
            var result = await _auth.RegisterSuperAdminAsync(dto);
            return CreatedAtAction(nameof(RegisterSuperAdmin), ApiResponse<AuthResponseDto>.Created(result, "Super admin registered successfully."));
        }

        /// <summary>Register a new delivery partner</summary>
        [HttpPost("register/delivery-partner")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 201)]
        public async Task<IActionResult> RegisterDeliveryPartner([FromBody] DeliveryPartnerRegisterDto dto)
        {
            var result = await _auth.RegisterDeliveryPartnerAsync(dto);
            return CreatedAtAction(nameof(RegisterDeliveryPartner), ApiResponse<AuthResponseDto>.Created(result, "Delivery partner registered successfully."));
        }

        /// <summary>Login with email, username, or phone number</summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _auth.LoginAsync(dto);
            return Ok(ApiResponse<AuthResponseDto>.Ok(result, "Login successful."));
        }

        /// <summary>Refresh access token using refresh token</summary>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 200)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
        {
            var result = await _auth.RefreshTokenAsync(dto);
            return Ok(ApiResponse<AuthResponseDto>.Ok(result, "Token refreshed."));
        }

        /// <summary>Logout current user</summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _auth.LogoutAsync(userId);
            return Ok(ApiResponse<object>.Ok(null!, "Logged out successfully."));
        }
    }
}
