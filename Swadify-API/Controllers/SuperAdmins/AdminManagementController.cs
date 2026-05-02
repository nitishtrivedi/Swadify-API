using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Helpers;

namespace Swadify_API.Controllers.SuperAdmins
{
    [ApiController]
    [Route("api/super-admins/admin-management")]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminManagementController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("get-admins")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _context.Users
                .Where(u => u.Role == Enums.UserRole.Admin)
                .Include(u => u.OwnedRestaurants)
                .ToListAsync();
            return Ok(admins);
        }

        [HttpPatch("update-admin/{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UpdateAdminDto dto)
        {
            var admin = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id && u.Role == Enums.UserRole.Admin);

            if (admin == null)
                return NotFound(new { message = "Admin not found" });

            // 🔹 Update fields
            admin.FirstName = dto.FirstName;
            admin.LastName = dto.LastName ?? string.Empty;
            admin.Username = dto.Username;
            admin.Email = dto.Email;

            // 🔹 Optional password update
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                // TODO: Replace with your hashing logic
                admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            admin.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Admin updated successfully",
                data = admin
            });
        }

        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminCreateAdminDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(ApiResponse<object>.Fail("Email already exists."));
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Username = dto.Username,
                PasswordHash = PasswordHelper.Hash(dto.Password),
                Role = dto.Role,
                IsActive = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new {message = "Admin Created" });
        }

        [HttpDelete("delete-admin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id && u.Role == Enums.UserRole.Admin);
            if (admin == null)
                return NotFound(new { message = "Admin not found" });
            _context.Users.Remove(admin);
            await _context.SaveChangesAsync();
            return Ok(new {message = "Admin Deleted" });
        }

        /// <summary>Toggle user active status (SuperAdmin)</summary>
        [HttpPatch("toggle-active/{id:int}")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();
            return Ok(new { message = user.IsActive ? "User activated." : "User deactivated." });
        }
    }


}
