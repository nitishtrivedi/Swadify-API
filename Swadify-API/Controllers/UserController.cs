using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
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
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ICloudinaryService _cloudinary;
        private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public UserController(AppDbContext db, ICloudinaryService cloudinary)
        {
            _db = db;
            _cloudinary = cloudinary;
        }

        /// <summary>Get current user profile</summary>
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _db.Users.FindAsync(UserId);
            if (user == null) return NotFound();

            return Ok(ApiResponse<object>.Ok(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Username,
                user.Email,
                user.PhoneNumber,
                Role = user.Role.ToString(),
                user.ProfileImageUrl,
                user.IsActive,
                user.AddressLine1,
                user.AddressLine2,
                user.City,
                user.State,
                user.PinCode
            }));
        }

        /// <summary>Update user profile</summary>
        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var user = await _db.Users.FindAsync(UserId);
            if (user == null) return NotFound();

            if (dto.FirstName != null) user.FirstName = dto.FirstName;
            if (dto.LastName != null) user.LastName = dto.LastName;
            if (dto.PhoneNumber != null) user.PhoneNumber = dto.PhoneNumber;
            if (dto.AddressLine1 != null) user.AddressLine1 = dto.AddressLine1;
            if (dto.AddressLine2 != null) user.AddressLine2 = dto.AddressLine2;
            if (dto.City != null) user.City = dto.City;
            if (dto.State != null) user.State = dto.State;
            if (dto.PinCode != null) user.PinCode = dto.PinCode;

            await _db.SaveChangesAsync();
            return Ok(ApiResponse<object>.Ok(null!, "Profile updated."));
        }

        /// <summary>Change password</summary>
        [HttpPatch("me/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var user = await _db.Users.FindAsync(UserId);
            if (user == null) return NotFound();

            if (!PasswordHelper.Verify(dto.CurrentPassword, user.PasswordHash))
                return BadRequest(ApiResponse<object>.Fail("Current password is incorrect."));

            user.PasswordHash = PasswordHelper.Hash(dto.NewPassword);
            await _db.SaveChangesAsync();
            return Ok(ApiResponse<object>.Ok(null!, "Password changed successfully."));
        }

        /// <summary>Upload profile picture</summary>
        [HttpPost("me/avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            var user = await _db.Users.FindAsync(UserId);
            if (user == null) return NotFound();

            var url = await _cloudinary.UploadImageAsync(file, "avatars");
            if (url == null) return BadRequest(ApiResponse<object>.Fail("Image upload failed."));

            user.ProfileImageUrl = url;
            await _db.SaveChangesAsync();
            return Ok(ApiResponse<object>.Ok(new { imageUrl = url }, "Avatar uploaded."));
        }

        /// <summary>Get all users (SuperAdmin)</summary>
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var query = _db.Users.AsQueryable();
            var total = await query.CountAsync();
            var users = await query.OrderByDescending(u => u.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new PagedResponse<object>
            {
                Data = users.Select(u => new
                {
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.Username,
                    Role = u.Role.ToString(),
                    u.IsActive,
                    u.CreatedAt
                }),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            });
        }

        /// <summary>Toggle user active status (SuperAdmin)</summary>
        [HttpPatch("{id:int}/toggle-active")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.IsActive = !user.IsActive;
            await _db.SaveChangesAsync();
            return Ok(ApiResponse<object>.Ok(new { user.IsActive }, user.IsActive ? "User activated." : "User deactivated."));
        }
    }
}
