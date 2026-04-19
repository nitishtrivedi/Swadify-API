using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swadify_API.DTOs;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;

namespace Swadify_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    [Produces("application/json")]
    public class SuperAdminController : ControllerBase
    {
        private readonly IAuthService _auth;

        public SuperAdminController(IAuthService auth) => _auth = auth;

        /// <summary>Create a new restaurant owner (Admin)</summary>
        [HttpPost("create-admin")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), 201)]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminCreateAdminDto dto)
        {
            var result = await _auth.AdminCreateAdminAsync(dto);
            return CreatedAtAction(nameof(CreateAdmin), ApiResponse<AuthResponseDto>.Created(result, "Admin created successfully."));
        }
    }
}
