using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;

namespace Swadify_API.Services
{
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _db;
        private readonly ICloudinaryService _cloudinary;

        public MenuService(AppDbContext db, ICloudinaryService cloudinary)
        {
            _db = db;
            _cloudinary = cloudinary;
        }

        public async Task<MenuItemResponseDto> CreateMenuItemAsync(int requesterId, CreateMenuItemDto dto)
        {
            var requester = await _db.Users.FindAsync(requesterId)!;

            if (requester!.Role == UserRole.Admin)
            {
                var ownsIt = await _db.Restaurants.AnyAsync(r => r.Id == dto.RestaurantId && r.OwnerId == requesterId);
                if (!ownsIt) throw new UnauthorizedAccessException("You don't own this restaurant.");
            }

            var menuItem = new MenuItem
            {
                RestaurantId = dto.RestaurantId,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DiscountedPrice = dto.DiscountedPrice,
                IsVegetarian = dto.IsVegetarian,
                IsVegan = dto.IsVegan,
                IsGlutenFree = dto.IsGlutenFree,
                IsSpicy = dto.IsSpicy,
                PreparationTimeMinutes = dto.PreparationTimeMinutes,
                CaloriesKcal = dto.CaloriesKcal
            };

            _db.MenuItems.Add(menuItem);
            await _db.SaveChangesAsync();
            return await GetMenuItemByIdAsync(menuItem.Id) ?? throw new Exception("Failed to retrieve item.");
        }

        public async Task<MenuItemResponseDto> UpdateMenuItemAsync(int itemId, int requesterId, UpdateMenuItemDto dto)
        {
            var item = await _db.MenuItems.Include(m => m.Restaurant).FirstOrDefaultAsync(m => m.Id == itemId)
                ?? throw new KeyNotFoundException("Menu item not found.");

            var requester = await _db.Users.FindAsync(requesterId)!;
            if (requester!.Role == UserRole.Admin && item.Restaurant?.OwnerId != requesterId)
                throw new UnauthorizedAccessException("Access denied.");

            if (dto.CategoryId.HasValue) item.CategoryId = dto.CategoryId.Value;
            if (dto.Name != null) item.Name = dto.Name;
            if (dto.Description != null) item.Description = dto.Description;
            if (dto.Price.HasValue) item.Price = dto.Price.Value;
            if (dto.DiscountedPrice.HasValue) item.DiscountedPrice = dto.DiscountedPrice.Value;
            if (dto.IsAvailable.HasValue) item.IsAvailable = dto.IsAvailable.Value;
            if (dto.IsVegetarian.HasValue) item.IsVegetarian = dto.IsVegetarian.Value;
            if (dto.IsVegan.HasValue) item.IsVegan = dto.IsVegan.Value;
            if (dto.IsGlutenFree.HasValue) item.IsGlutenFree = dto.IsGlutenFree.Value;
            if (dto.IsSpicy.HasValue) item.IsSpicy = dto.IsSpicy.Value;
            if (dto.IsBestseller.HasValue) item.IsBestseller = dto.IsBestseller.Value;
            if (dto.PreparationTimeMinutes.HasValue) item.PreparationTimeMinutes = dto.PreparationTimeMinutes.Value;
            if (dto.CaloriesKcal.HasValue) item.CaloriesKcal = dto.CaloriesKcal.Value;

            await _db.SaveChangesAsync();
            return await GetMenuItemByIdAsync(item.Id) ?? throw new Exception("Failed.");
        }

        public async Task<bool> DeleteMenuItemAsync(int itemId, int requesterId)
        {
            var item = await _db.MenuItems.Include(m => m.Restaurant).FirstOrDefaultAsync(m => m.Id == itemId)
                ?? throw new KeyNotFoundException("Menu item not found.");

            var requester = await _db.Users.FindAsync(requesterId)!;
            if (requester!.Role == UserRole.Admin && item.Restaurant?.OwnerId != requesterId)
                throw new UnauthorizedAccessException("Access denied.");

            _db.MenuItems.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<MenuItemResponseDto?> GetMenuItemByIdAsync(int id)
        {
            var item = await _db.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);

            return item == null ? null : MapToDto(item);
        }

        public async Task<PagedResponse<MenuItemResponseDto>> GetMenuByRestaurantAsync(int restaurantId, int page, int pageSize, int? categoryId, bool? isVeg)
        {
            var query = _db.MenuItems
                .Include(m => m.Category)
                .Include(m => m.Restaurant)
                .Where(m => m.RestaurantId == restaurantId);

            if (categoryId.HasValue) query = query.Where(m => m.CategoryId == categoryId.Value);
            if (isVeg.HasValue && isVeg.Value) query = query.Where(m => m.IsVegetarian);

            var total = await query.CountAsync();
            var items = await query.OrderBy(m => m.Category!.DisplayOrder).ThenBy(m => m.Name)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<MenuItemResponseDto>
            {
                Data = items.Select(MapToDto),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<bool> UploadMenuItemImageAsync(int itemId, int requesterId, IFormFile file)
        {
            var item = await _db.MenuItems.Include(m => m.Restaurant).FirstOrDefaultAsync(m => m.Id == itemId)
                ?? throw new KeyNotFoundException("Menu item not found.");

            var url = await _cloudinary.UploadImageAsync(file, "menu-items");
            if (url == null) return false;
            item.ImageUrl = url;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<MenuCategoryDto>> GetMenuCategoriesAsync()
        {
            return await _db.MenuCategories
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                //.Select(c => new MenuCategoryDto { Id = c.Id, Name = c.Name, DisplayOrder = c.DisplayOrder })
                .Select(c => new MenuCategoryDto { Name = c.Name, DisplayOrder = c.DisplayOrder })
                .ToListAsync();
        }

        private static MenuItemResponseDto MapToDto(MenuItem m) => new()
        {
            Id = m.Id,
            RestaurantId = m.RestaurantId,
            RestaurantName = m.Restaurant?.Name ?? string.Empty,
            CategoryName = m.Category?.Name ?? string.Empty,
            Name = m.Name,
            Description = m.Description,
            ImageUrl = m.ImageUrl,
            Price = m.Price,
            DiscountedPrice = m.DiscountedPrice,
            IsAvailable = m.IsAvailable,
            IsVegetarian = m.IsVegetarian,
            IsVegan = m.IsVegan,
            IsGlutenFree = m.IsGlutenFree,
            IsBestseller = m.IsBestseller,
            IsSpicy = m.IsSpicy,
            PreparationTimeMinutes = m.PreparationTimeMinutes,
            AverageRating = m.AverageRating,
            TotalRatings = m.TotalRatings,
            CaloriesKcal = m.CaloriesKcal
        };
    }
}
