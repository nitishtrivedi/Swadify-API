using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Helpers;

namespace Swadify_API.Controllers.SuperAdmins
{
    [ApiController]
    [Route("api/super-admins/user-management")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserManagementController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return Ok(users);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] SuperAdminCreateUserDto dto)
        {
            // Username exists
            bool usernameExists = await _context.Users
                .AnyAsync(x => x.Username.ToLower() == dto.Username.ToLower());

            if (usernameExists)
            {
                return BadRequest(new
                {
                    message = "Username already exists"
                });
            }

            // Email exists
            bool emailExists = await _context.Users
                .AnyAsync(x => x.Email.ToLower() == dto.Email.ToLower());

            if (emailExists)
            {
                return BadRequest(new
                {
                    message = "Email already exists"
                });
            }


            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName ?? string.Empty,
                Username = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.Phone ?? string.Empty,
                PasswordHash = PasswordHelper.Hash(dto.Password),
                Role = (UserRole)dto.Role,
                IsActive = true,
                IsEmailVerified = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            // If role is DeliveryPartner, create DeliveryPartnerProfile
            if ((UserRole)dto.Role == UserRole.DeliveryPartner)
            {
                var deliveryProfile = new DeliveryPartnerProfile
                {
                    UserId = user.Id,
                    VehicleType = string.Empty,  // Empty defaults — Admin can update later
                    VehicleNumber = string.Empty,
                    IsAvailable = false,
                    IsOnline = false
                };

                _context.DeliveryPartnerProfiles.Add(deliveryProfile);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                message = "User created successfully"
            });
        }

        [HttpPatch("edit-user/{id}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] SuperAdminUpdateUserDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            // Username check
            if (!string.IsNullOrWhiteSpace(dto.Username))
            {
                bool usernameExists = await _context.Users.AnyAsync(x =>
                    x.Id != id &&
                    x.Username.ToLower() == dto.Username.ToLower());

                if (usernameExists)
                {
                    return BadRequest(new
                    {
                        message = "Username already exists"
                    });
                }

                user.Username = dto.Username;
            }

            // Email check
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                bool emailExists = await _context.Users.AnyAsync(x =>
                    x.Id != id &&
                    x.Email.ToLower() == dto.Email.ToLower());

                if (emailExists)
                {
                    return BadRequest(new
                    {
                        message = "Email already exists"
                    });
                }

                user.Email = dto.Email;
            }

            // Phone check
            if (!string.IsNullOrWhiteSpace(dto.Phone))
            {
                bool phoneExists = await _context.Users.AnyAsync(x =>
                    x.Id != id &&
                    x.PhoneNumber == dto.Phone);

                if (phoneExists)
                {
                    return BadRequest(new
                    {
                        message = "Phone number already exists"
                    });
                }

                user.PhoneNumber = dto.Phone;
            }

            // Optional updates
            if (!string.IsNullOrWhiteSpace(dto.FirstName))
            {
                user.FirstName = dto.FirstName;
            }

            if (dto.LastName != null)
            {
                user.LastName = dto.LastName;
            }

            if (dto.Role.HasValue)
            {
                user.Role = dto.Role.Value;
            }

            if (dto.IsActive.HasValue)
            {
                user.IsActive = dto.IsActive.Value;
            }

            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User updated successfully"
            });
        }

        [HttpPatch("toggle-status/{id}")]
        public async Task<IActionResult> ToggleUserStatus(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            // Optional: prevent disabling self
            var currentUserId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value
            );

            if (user.Id == currentUserId)
            {
                return BadRequest(new
                {
                    message = "You cannot disable your own account"
                });
            }

            user.IsActive = !user.IsActive;

            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = user.IsActive
                    ? "User enabled successfully"
                    : "User disabled successfully",

                isActive = user.IsActive
            });
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "User not found"
                });
            }

            // Prevent self delete
            var currentUserId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!
                    .Value
            );

            if (user.Id == currentUserId)
            {
                return BadRequest(new
                {
                    message = "You cannot delete your own account"
                });
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User deleted successfully"
            });
        }
    }
}
