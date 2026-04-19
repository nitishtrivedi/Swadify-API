using Swadify_API.DTOs;

namespace Swadify_API.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterCustomerAsync(CustomerRegisterDto dto);
        Task<AuthResponseDto> RegisterSuperAdminAsync(SuperAdminRegisterDto dto);
        Task<AuthResponseDto> RegisterDeliveryPartnerAsync(DeliveryPartnerRegisterDto dto);
        Task<AuthResponseDto> AdminCreateAdminAsync(AdminCreateAdminDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
        Task LogoutAsync(int userId);
    }
}
