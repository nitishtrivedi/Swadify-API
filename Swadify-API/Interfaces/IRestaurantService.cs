using Swadify_API.DTOs;
using Swadify_API.Helpers;

namespace Swadify_API.Interfaces
{
    public interface IRestaurantService
    {
        Task<RestaurantResponseDto> CreateRestaurantAsync(int ownerId, CreateRestaurantDto dto);
        Task<RestaurantResponseDto> UpdateRestaurantAsync(int restaurantId, int requesterId, UpdateRestaurantDto dto);
        Task<RestaurantResponseDto?> GetRestaurantByIdAsync(int id);
        Task<List<RestaurantResponseDto>> GetFeaturedRestaurantsAsync();
        Task<PagedResponse<RestaurantResponseDto>> GetRestaurantsAsync(int page, int pageSize, string? search, int? categoryId, bool? isOpen);
        Task<PagedResponse<RestaurantResponseDto>> GetMyRestaurantsAsync(int ownerId, int page, int pageSize);
        Task<bool> UploadLogoAsync(int restaurantId, int requesterId, IFormFile file);
        Task<bool> UploadCoverImageAsync(int restaurantId, int requesterId, IFormFile file);
        Task<bool> ToggleActiveStatusAsync(int restaurantId, int requesterId);
        Task<bool> VerifyRestaurantAsync(int restaurantId); // SuperAdmin only
        Task<List<RestaurantCategoryDto>> GetCategoriesAsync();
        Task<bool> DeleteRestaurantAsync(int id);
    }

    //public class MenuCategoryDto
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public int DisplayOrder { get; set; }
    //}
}
