using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;

namespace Swadify_API.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly AppDbContext _db;
        private readonly ICloudinaryService _cloudinary;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(AppDbContext db, ICloudinaryService cloudinary, ILogger<RestaurantService> logger)
        {
            _db = db;
            _cloudinary = cloudinary;
            _logger = logger;
        }

        public async Task<RestaurantResponseDto> CreateRestaurantAsync(int ownerId, CreateRestaurantDto dto)
        {
            var owner = await _db.Users.FindAsync(ownerId)
                ?? throw new KeyNotFoundException("Owner not found.");

            if (owner.Role != UserRole.Admin && owner.Role != UserRole.SuperAdmin)
                throw new UnauthorizedAccessException("Only Admins can create restaurants.");

            var category = await _db.RestaurantCategories.FindAsync(dto.CategoryId)
                ?? throw new KeyNotFoundException("Restaurant category not found.");

            var restaurant = new Restaurant
            {
                OwnerId = ownerId,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                PinCode = dto.PinCode,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                OpeningTime = dto.OpeningTime,
                ClosingTime = dto.ClosingTime,
                DeliveryFee = dto.DeliveryFee,
                MinimumOrderAmount = dto.MinimumOrderAmount,
                EstimatedDeliveryTimeMinutes = dto.EstimatedDeliveryTimeMinutes,
                DeliveryRadiusKm = dto.DeliveryRadiusKm,
                IsFeatured = dto.IsFeatured
            };

            _db.Restaurants.Add(restaurant);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Restaurant created: {Name} by Owner {OwnerId}", restaurant.Name, ownerId);
            return await MapToResponseDto(restaurant, owner);
        }


        public async Task<RestaurantResponseDto> UpdateRestaurantAsync(int restaurantId, int requesterId, UpdateRestaurantDto dto)
        {
            var restaurant = await _db.Restaurants.Include(r => r.Owner).Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == restaurantId)
                ?? throw new KeyNotFoundException("Restaurant not found.");

            var requester = await _db.Users.FindAsync(requesterId)
                ?? throw new KeyNotFoundException("User not found.");

            if (requester.Role != UserRole.SuperAdmin && restaurant.OwnerId != requesterId)
                throw new UnauthorizedAccessException("You don't own this restaurant.");

            if (dto.Name != null) restaurant.Name = dto.Name;
            if (dto.Description != null) restaurant.Description = dto.Description;
            if (dto.CategoryId.HasValue) restaurant.CategoryId = dto.CategoryId.Value;
            if (dto.PhoneNumber != null) restaurant.PhoneNumber = dto.PhoneNumber;
            if (dto.Email != null) restaurant.Email = dto.Email;
            if (dto.AddressLine1 != null) restaurant.AddressLine1 = dto.AddressLine1;
            if (dto.AddressLine2 != null) restaurant.AddressLine2 = dto.AddressLine2;
            if (dto.City != null) restaurant.City = dto.City;
            if (dto.State != null) restaurant.State = dto.State;
            if (dto.PinCode != null) restaurant.PinCode = dto.PinCode;
            if (dto.Latitude.HasValue) restaurant.Latitude = dto.Latitude.Value;
            if (dto.Longitude.HasValue) restaurant.Longitude = dto.Longitude.Value;
            if (dto.Status.HasValue) restaurant.Status = dto.Status.Value;
            if (dto.OpeningTime.HasValue) restaurant.OpeningTime = dto.OpeningTime.Value;
            if (dto.ClosingTime.HasValue) restaurant.ClosingTime = dto.ClosingTime.Value;
            if (dto.DeliveryFee.HasValue) restaurant.DeliveryFee = dto.DeliveryFee.Value;
            if (dto.MinimumOrderAmount.HasValue) restaurant.MinimumOrderAmount = dto.MinimumOrderAmount.Value;
            if (dto.EstimatedDeliveryTimeMinutes.HasValue) restaurant.EstimatedDeliveryTimeMinutes = dto.EstimatedDeliveryTimeMinutes.Value;
            if (dto.DeliveryRadiusKm.HasValue) restaurant.DeliveryRadiusKm = dto.DeliveryRadiusKm.Value;
            if (dto.IsFeatured.HasValue) restaurant.IsFeatured = dto.IsFeatured.Value;

            await _db.SaveChangesAsync();
            return await MapToResponseDto(restaurant, restaurant.Owner!);
        }

        public async Task<RestaurantResponseDto?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _db.Restaurants
                .Include(r => r.Owner)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null) return null;
            return await MapToResponseDto(restaurant, restaurant.Owner!);
        }


        public async Task<PagedResponse<RestaurantResponseDto>> GetRestaurantsAsync(int page, int pageSize, string? search, int? categoryId, bool? isOpen)
        {
            var query = _db.Restaurants
                .Include(r => r.Owner)
                .Include(r => r.Category)
                .Where(r => r.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(r => r.Name.ToLower().Contains(search.ToLower()) ||
                                         r.City.ToLower().Contains(search.ToLower()));

            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId.Value);

            if (isOpen.HasValue && isOpen.Value)
                query = query.Where(r => r.Status == RestaurantStatus.Open);

            var total = await query.CountAsync();
            var restaurants = await query
                .OrderByDescending(r => r.AverageRating)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = new List<RestaurantResponseDto>();
            foreach (var r in restaurants)
                dtos.Add(await MapToResponseDto(r, r.Owner!));

            return new PagedResponse<RestaurantResponseDto>
            {
                Data = dtos,
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

       

        public async Task<PagedResponse<RestaurantResponseDto>> GetMyRestaurantsAsync(int ownerId, int page, int pageSize)
        {
            var query = _db.Restaurants
                .Include(r => r.Owner)
                .Include(r => r.Category)
                .Where(r => r.OwnerId == ownerId);

            var total = await query.CountAsync();
            var restaurants = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = new List<RestaurantResponseDto>();
            foreach (var r in restaurants)
                dtos.Add(await MapToResponseDto(r, r.Owner!));

            return new PagedResponse<RestaurantResponseDto> { Data = dtos, Page = page, PageSize = pageSize, TotalCount = total };
        }

        public async Task<bool> UploadLogoAsync(int restaurantId, int requesterId, IFormFile file)
        {
            var restaurant = await GetOwnedRestaurantAsync(restaurantId, requesterId);
            var url = await _cloudinary.UploadImageAsync(file, "restaurant-logos");
            if (url == null) return false;
            restaurant.LogoUrl = url;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UploadCoverImageAsync(int restaurantId, int requesterId, IFormFile file)
        {
            var restaurant = await GetOwnedRestaurantAsync(restaurantId, requesterId);
            var url = await _cloudinary.UploadImageAsync(file, "restaurant-covers");
            if (url == null) return false;
            restaurant.CoverImageUrl = url;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveStatusAsync(int restaurantId, int requesterId)
        {
            var restaurant = await GetOwnedRestaurantAsync(restaurantId, requesterId);
            restaurant.IsActive = !restaurant.IsActive;
            await _db.SaveChangesAsync();
            return restaurant.IsActive;
        }

        public async Task<bool> VerifyRestaurantAsync(int restaurantId)
        {
            var restaurant = await _db.Restaurants.FindAsync(restaurantId)
                ?? throw new KeyNotFoundException("Restaurant not found.");
            restaurant.IsVerified = true;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<RestaurantCategoryDto>> GetCategoriesAsync()
        {
            return await _db.RestaurantCategories
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .Select(c => new RestaurantCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    IconUrl = c.IconUrl,
                    DisplayOrder = c.DisplayOrder
                })
                .ToListAsync();
        }

        private async Task<Restaurant> GetOwnedRestaurantAsync(int restaurantId, int requesterId)
        {
            var requester = await _db.Users.FindAsync(requesterId)
                ?? throw new KeyNotFoundException("User not found.");

            var restaurant = await _db.Restaurants.FindAsync(restaurantId)
                ?? throw new KeyNotFoundException("Restaurant not found.");

            if (requester.Role != UserRole.SuperAdmin && restaurant.OwnerId != requesterId)
                throw new UnauthorizedAccessException("Access denied.");

            return restaurant;
        }

        private static Task<RestaurantResponseDto> MapToResponseDto(Restaurant r, User owner)
        {
            return Task.FromResult(new RestaurantResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                LogoUrl = r.LogoUrl,
                CoverImageUrl = r.CoverImageUrl,
                CategoryName = r.Category?.Name ?? string.Empty,
                PhoneNumber = r.PhoneNumber,
                Email = r.Email,
                Address = r.AddressLine1 + (string.IsNullOrEmpty(r.AddressLine2) ? "" : ", " + r.AddressLine2),
                City = r.City,
                State = r.State,
                PinCode = r.PinCode,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                Status = r.Status.ToString(),
                IsActive = r.IsActive,
                IsVerified = r.IsVerified,
                IsFeatured = r.IsFeatured,
                OpeningTime = r.OpeningTime.ToString("hh:mm tt"),
                ClosingTime = r.ClosingTime.ToString("hh:mm tt"),
                AverageRating = r.AverageRating,
                TotalRatings = r.TotalRatings,
                DeliveryFee = r.DeliveryFee,
                MinimumOrderAmount = r.MinimumOrderAmount,
                EstimatedDeliveryTimeMinutes = r.EstimatedDeliveryTimeMinutes,
                DeliveryRadiusKm = r.DeliveryRadiusKm,
                OwnerName = $"{owner.FirstName} {owner.LastName}",
                CreatedAt = r.CreatedAt
            });
        }

        public async Task<RestaurantResponseDto> GetFeaturedRestaurantsAsync()
        {
            var restaurant = await _db.Restaurants
                .Include(r => r.Owner)
                .Include(r => r.Category)
                .Where(r => r.IsFeatured && r.IsActive)
                .OrderByDescending(r => r.AverageRating)
                .FirstOrDefaultAsync();
            if (restaurant == null) throw new KeyNotFoundException("No featured restaurant found.");
            return await MapToResponseDto(restaurant, restaurant.Owner!);
        }

        public async Task<bool> DeleteRestaurantAsync(int id)
        {
            var restaurant = _db.Restaurants.Find(id);
            if (restaurant == null) throw new KeyNotFoundException("Restaurant not found.");
            restaurant.IsActive = false;
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
