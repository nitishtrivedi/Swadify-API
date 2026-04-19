using Swadify_API.DTOs;
using Swadify_API.Helpers;

namespace Swadify_API.Interfaces
{
    public interface IMenuService
    {
        Task<MenuItemResponseDto> CreateMenuItemAsync(int requesterId, CreateMenuItemDto dto);
        Task<MenuItemResponseDto> UpdateMenuItemAsync(int itemId, int requesterId, UpdateMenuItemDto dto);
        Task<bool> DeleteMenuItemAsync(int itemId, int requesterId);
        Task<MenuItemResponseDto?> GetMenuItemByIdAsync(int id);
        Task<PagedResponse<MenuItemResponseDto>> GetMenuByRestaurantAsync(int restaurantId, int page, int pageSize, int? categoryId, bool? isVeg);
        Task<bool> UploadMenuItemImageAsync(int itemId, int requesterId, IFormFile file);
        Task<List<MenuCategoryDto>> GetMenuCategoriesAsync();
    }
}
