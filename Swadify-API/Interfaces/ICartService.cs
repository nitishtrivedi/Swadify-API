using Swadify_API.DTOs;

namespace Swadify_API.Interfaces
{
    public interface ICartService
    {
        Task<CartResponseDto> GetCartAsync(int customerId);
        Task<CartResponseDto> AddToCartAsync(int customerId, AddToCartDto dto);
        Task<CartResponseDto> UpdateCartItemAsync(int customerId, int cartItemId, UpdateCartItemDto dto);
        Task<bool> ClearCartAsync(int customerId);
        Task<bool> RemoveCartItemAsync(int customerId, int cartItemId);
    }
}

