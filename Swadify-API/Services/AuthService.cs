using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;

namespace Swadify_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly JwtHelper _jwt;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;
        private readonly INotificationService _notifications; // ✅ Add this

        public AuthService(AppDbContext db, JwtHelper jwt, IConfiguration config, ILogger<AuthService> logger, INotificationService notifications) // ✅ Inject
        {
            _db = db;
            _jwt = jwt;
            _config = config;
            _logger = logger;
            _notifications = notifications; // ✅ Store it
        }

        public async Task<AuthResponseDto> RegisterCustomerAsync(CustomerRegisterDto dto)
        {
            await ValidateUniqueFields(dto.Email, dto.Username, dto.PhoneNumber);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username.ToLower(),
                Email = dto.Email.ToLower(),
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = PasswordHelper.Hash(dto.Password),
                Role = UserRole.Customer
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            _logger.LogInformation("New customer registered: {Email}", user.Email);
            return BuildAuthResponse(user);
        }

        public async Task<AuthResponseDto> RegisterSuperAdminAsync(SuperAdminRegisterDto dto)
        {
            await ValidateUniqueFields(dto.Email, dto.Username, dto.PhoneNumber);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username.ToLower(),
                Email = dto.Email.ToLower(),
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = PasswordHelper.Hash(dto.Password),
                Role = UserRole.SuperAdmin
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            _logger.LogInformation("New super admin registered: {Email}", user.Email);
            return BuildAuthResponse(user);
        }

        public async Task<AuthResponseDto> RegisterDeliveryPartnerAsync(DeliveryPartnerRegisterDto dto)
        {
            await ValidateUniqueFields(dto.Email, dto.Username, dto.PhoneNumber);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username.ToLower(),
                Email = dto.Email.ToLower(),
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = PasswordHelper.Hash(dto.Password),
                Role = UserRole.DeliveryPartner,
                IsActive = false,
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var profile = new DeliveryPartnerProfile
            {
                UserId = user.Id,
                VehicleType = dto.VehicleType,
                VehicleNumber = dto.VehicleNumber,
                LicenseNumber = dto.LicenseNumber,
                AadharNumber = dto.AadharNumber,
                ApplicationStatus = ApplicationStatus.Pending
            };

            _db.DeliveryPartnerProfiles.Add(profile);
            await _db.SaveChangesAsync();

            // ✅ Notify SuperAdmins of new DP application
            try
            {
                await _notifications.SendToRoleAsync(
                    UserRole.SuperAdmin,
                    "New Delivery Partner Application",
                    $"{user.FirstName} {user.LastName} ({user.Email}) has applied for delivery partnership with vehicle {dto.VehicleNumber}",
                    NotificationType.General
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send DP application notification");
            }

            _logger.LogInformation("New delivery partner registered: {Email}", user.Email);
            return BuildAuthResponse(user);
        }

        public async Task<AuthResponseDto> AdminCreateAdminAsync(AdminCreateAdminDto dto)
        {
            await ValidateUniqueFields(dto.Email, dto.Username, dto.PhoneNumber);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username.ToLower(),
                Email = dto.Email.ToLower(),
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = PasswordHelper.Hash(dto.Password),
                Role = UserRole.Admin
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            _logger.LogInformation("New admin created: {Email}", user.Email);
            return BuildAuthResponse(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var identifier = dto.Identifier.ToLower().Trim();

            var user = await _db.Users.FirstOrDefaultAsync(u =>
                u.Email == identifier ||
                u.Username == identifier ||
                u.PhoneNumber == identifier);

            if (user == null || !PasswordHelper.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Account has been deactivated.");

            var refreshToken = _jwt.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            user.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            _logger.LogInformation("User logged in: {Email}", user.Email);
            return BuildAuthResponse(user, refreshToken);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var principal = _jwt.GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal == null)
                throw new UnauthorizedAccessException("Invalid access token.");

            var userId = int.Parse(principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var user = await _db.Users.FindAsync(userId)
                ?? throw new UnauthorizedAccessException("User not found.");

            if (user.RefreshToken != dto.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");

            var newRefreshToken = _jwt.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _db.SaveChangesAsync();

            return BuildAuthResponse(user, newRefreshToken);
        }


        private async Task ValidateUniqueFields(string email, string username, string phone)
        {
            if (await _db.Users.AnyAsync(u => u.Email == email.ToLower()))
                throw new InvalidOperationException("Email already registered.");
            if (await _db.Users.AnyAsync(u => u.Username == username.ToLower()))
                throw new InvalidOperationException("Username already taken.");
            if (await _db.Users.AnyAsync(u => u.PhoneNumber == phone))
                throw new InvalidOperationException("Phone number already registered.");
        }

        private AuthResponseDto BuildAuthResponse(User user, string? refreshToken = null)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var expiryMinutes = double.Parse(jwtSettings["ExpiryInMinutes"]!);

            return new AuthResponseDto
            {
                AccessToken = _jwt.GenerateAccessToken(user),
                RefreshToken = refreshToken ?? user.RefreshToken ?? string.Empty,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes),
                User = new UserProfileDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    ProfileImageUrl = user.ProfileImageUrl,
                    IsActive = user.IsActive
                }
            };
        }

        public async Task LogoutAsync(int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiry = null;
                await _db.SaveChangesAsync();
            }
        }
    }
}
