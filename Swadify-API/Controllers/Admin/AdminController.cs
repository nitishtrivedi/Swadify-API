using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Interfaces;
using System.Linq;
using System.Security.Claims;

namespace Swadify_API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController(AppDbContext context, ICloudinaryService cloudinaryService) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;


        #region Dashboard

        [HttpGet("dashboard/stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(adminIdClaim))
                return Unauthorized(new { message = "Admin ID claim is missing" });

            var adminId = int.Parse(adminIdClaim);

            //Restaurants owned by this admin
            var restaurantIds = await _context.Restaurants
                .Where(r => r.OwnerId == adminId)
                .Select(r => r.Id)
                .ToListAsync();

            // 3) Base query for orders of this admin
            var orders = _context.Orders
                .Where(o => restaurantIds.Contains(o.RestaurantId));

            // 4) Aggregations
            var totalOrders = await orders.CountAsync();

            var activeOrders = await orders.CountAsync(o =>
                o.Status == Enums.OrderStatus.Pending ||
                o.Status == Enums.OrderStatus.Confirmed ||
                o.Status == Enums.OrderStatus.Preparing ||
                o.Status == Enums.OrderStatus.OutForDelivery
            );

            var totalRevenue = await orders
                .Where(o => o.Status == Enums.OrderStatus.Delivered)
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            var todayRevenue = await orders
                .Where(o =>
                    o.Status == Enums.OrderStatus.Delivered &&
                    o.CreatedAt >= DateTime.UtcNow.Date
                )
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            var totalRestaurants = restaurantIds.Count;

            // NOTE: If partners are later mapped per restaurant, filter by restaurantIds
            var activePartners = await _context.Users
                .CountAsync(u => u.Role == Enums.UserRole.DeliveryPartner && u.IsActive);

            var avgRating = await _context.Reviews
                .Where(r => restaurantIds.Contains(r.RestaurantId))
                .AverageAsync(r => (double?)r.Rating) ?? 0;

            var newCustomers = await _context.Users
                .CountAsync(u =>
                    u.Role == Enums.UserRole.Customer &&
                    u.CreatedAt >= DateTime.UtcNow.Date
                );

            var todayOrders = await orders.CountAsync(o =>
                    o.CreatedAt >= DateTime.UtcNow.Date
                );

            // 5) Map to DTO
            var dto = new AdminDashStatsDto
            {
                TotalOrders = totalOrders,
                ActiveOrders = activeOrders,
                TotalRevenue = totalRevenue,
                TodayRevenue = todayRevenue,
                TotalRestaurants = totalRestaurants,
                ActivePartners = activePartners,
                AvgRating = Math.Round(avgRating, 1),
                NewCustomers = newCustomers,
                TodayOrders = todayOrders
            };

            return Ok(dto);



        }

        [HttpGet("dashboard/recent-orders")]
        public async Task<IActionResult> GetRecentOrders()
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(adminIdClaim))
                return Unauthorized();

            var adminId = int.Parse(adminIdClaim);

            var recentOrders = await _context.Orders
                .Where(o => o.Restaurant.OwnerId == adminId)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new AdminRecentOrderDto
                {
                    Id = o.Id.ToString(),
                    CustomerName = o.Customer != null
                ? o.Customer.FirstName + " " + o.Customer.LastName
                : "N/A",
                    RestaurantName = o.Restaurant != null
                ? o.Restaurant.Name
                : "N/A",
                    Total = o.TotalAmount,
                    Status = o.Status.ToString(),
                    CreatedAt = o.CreatedAt
                })
                .Take(5)
                .ToListAsync();

            return Ok(recentOrders);
        }

        [HttpGet("dashboard/chart-data")]
        public async Task<IActionResult> GetChartData([FromQuery] string period = "7D")
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var restaurantIds = await _context.Restaurants
                .Where(r => r.OwnerId == adminId).Select(r => r.Id).ToListAsync();

            var days = period == "7D" ? 7 : period == "30D" ? 30 : 90;
            var from = DateTime.UtcNow.Date.AddDays(-(days - 1));

            var data = await _context.Orders
                .Where(o => restaurantIds.Contains(o.RestaurantId)
                         && o.Status == Enums.OrderStatus.Delivered
                         && o.CreatedAt >= from)
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new { Date = g.Key, Revenue = g.Sum(o => o.TotalAmount), Orders = g.Count() })
                .OrderBy(x => x.Date)
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("dashboard/active-partners")]
        public async Task<IActionResult> GetActivePartners()
        {
            var partners = await _context.Users
                .Where(u => u.Role == Enums.UserRole.DeliveryPartner && u.IsActive)
                .Select(u => new {
                    Name = u.FirstName + " " + u.LastName,
                    Orders = _context.Orders.Count(o =>
                        o.DeliveryPartnerId == u.Id &&
                        o.CreatedAt >= DateTime.UtcNow.Date),
                    Online = u.IsActive
                })
                .OrderByDescending(x => x.Orders)
                .Take(5)
                .ToListAsync();

            return Ok(partners);
        }

        [HttpGet("dashboard/top-restaurants")]
        public async Task<IActionResult> GetTopRestaurants()
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var restaurants = await _context.Restaurants
                .Where(r => r.OwnerId == adminId)
                .Select(r => new {
                    Name = r.Name,
                    Orders = _context.Orders.Count(o =>
                        o.RestaurantId == r.Id &&
                        o.CreatedAt >= DateTime.UtcNow.Date),
                    Revenue = _context.Orders
                        .Where(o => o.RestaurantId == r.Id &&
                                    o.Status == Enums.OrderStatus.Delivered &&
                                    o.CreatedAt >= DateTime.UtcNow.Date)
                        .Sum(o => (decimal?)o.TotalAmount) ?? 0
                })
                .OrderByDescending(x => x.Revenue)
                .Take(5)
                .ToListAsync();

            return Ok(restaurants);
        }

        #endregion

        #region Restaurants

        [HttpGet("restaurants/get-all")]
        public async Task<IActionResult> GetMyRestaurants([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var query = _context.Restaurants.Where(r => r.OwnerId == adminId);
            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { items, total, page, pageSize });
        }

        [HttpGet("restaurants/get-by-id/{id}")]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == adminId);

            if (restaurant == null) return NotFound();
            return Ok(restaurant);
        }

        [HttpPatch("restaurants/{id}/status")]
        public async Task<IActionResult> UpdateRestaurantStatus(int id, [FromBody] UpdateRestaurantStatusDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == adminId);

            if (restaurant == null) return NotFound();

            restaurant.Status = dto.Status;
            await _context.SaveChangesAsync();
            return Ok(restaurant);
        }

        [HttpPost("restaurants/create-new")]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var restaurant = new Restaurant
            {
                OwnerId = adminId,
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
                IsFeatured = dto.IsFeatured,
                Status = RestaurantStatus.Closed,
                IsActive = true,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
        }

        [HttpPost("restaurants/{id}/logo")]
        public async Task<IActionResult> UploadLogo(int id, [FromForm] IFormFile image)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == adminId);

            if (restaurant == null) return NotFound();
            if (image == null || image.Length == 0) return BadRequest(new { message = "No image provided" });

            var url = await _cloudinaryService.UploadImageAsync(image, "logos");
            if (url == null) return StatusCode(500, new { message = "Image upload failed" });

            restaurant.LogoUrl = url;
            await _context.SaveChangesAsync();
            return Ok(new { url });
        }

        [HttpPost("restaurants/{id}/cover")]
        public async Task<IActionResult> UploadCover(int id, [FromForm] IFormFile image)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == adminId);

            if (restaurant == null) return NotFound();
            if (image == null || image.Length == 0) return BadRequest(new { message = "No image provided" });

            var url = await _cloudinaryService.UploadImageAsync(image, "covers");
            if (url == null) return StatusCode(500, new { message = "Image upload failed" });

            restaurant.CoverImageUrl = url;
            await _context.SaveChangesAsync();
            return Ok(new { url });
        }

        [HttpDelete("restaurants/delete-restaurant/{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == adminId);
            if (restaurant == null) return NotFound();
            restaurant.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok(new {message =  "Restaurant Deleted"});
        }

        [HttpPut("restaurants/update-restaurant/{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] CreateRestaurantDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == id && r.OwnerId == adminId);

            if (restaurant == null) return NotFound();

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.CategoryId = dto.CategoryId;
            restaurant.PhoneNumber = dto.PhoneNumber;
            restaurant.Email = dto.Email;
            restaurant.AddressLine1 = dto.AddressLine1;
            restaurant.AddressLine2 = dto.AddressLine2;
            restaurant.City = dto.City;
            restaurant.State = dto.State;
            restaurant.PinCode = dto.PinCode;
            restaurant.Latitude = dto.Latitude;
            restaurant.Longitude = dto.Longitude;
            restaurant.OpeningTime = dto.OpeningTime;
            restaurant.ClosingTime = dto.ClosingTime;
            restaurant.DeliveryFee = dto.DeliveryFee;
            restaurant.MinimumOrderAmount = dto.MinimumOrderAmount;
            restaurant.EstimatedDeliveryTimeMinutes = dto.EstimatedDeliveryTimeMinutes;
            restaurant.DeliveryRadiusKm = dto.DeliveryRadiusKm;
            restaurant.IsFeatured = dto.IsFeatured;
            restaurant.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(restaurant);
        }




        #endregion

        #region Categories

        [HttpGet("restaurants/categories")]
        public async Task<IActionResult> GetRestaurantCategories()
        {
            var categories = await _context.RestaurantCategories
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
            return Ok(categories);
        }

        [HttpGet("restaurants/{restaurantId}/menuCategories")]
        public async Task<IActionResult> GetMenuCategories(int restaurantId)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == restaurantId && r.OwnerId == adminId);
            if (restaurant == null) return NotFound();

            var categories = await _context.MenuCategories
                .Where(c => c.RestaurantId == restaurantId && c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description,
                    c.DisplayOrder,
                    RestaurantId = c.RestaurantId,
                    Items = c.MenuItems!
                        .Where(i => i.IsActive)
                        .Select(i => new
                        {
                            i.Id,
                            i.RestaurantId,
                            i.CategoryId,
                            i.Name,
                            i.Description,
                            i.Price,
                            i.IsVegetarian,
                            i.IsVegan,
                            i.IsGlutenFree,
                            i.IsBestseller,
                            i.IsSpicy,
                            i.IsAvailable,
                            i.PreparationTimeMinutes,
                            i.ImageUrl,
                            i.Tags
                        })
                })
                .ToListAsync();

            return Ok(categories);
        }

        [HttpPost("restaurants/{restaurantId}/menuCategories")]
        public async Task<IActionResult> CreateMenuCategory(int restaurantId, [FromBody] MenuCategoryDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == restaurantId && r.OwnerId == adminId);
            if (restaurant == null) return NotFound();

            var category = new MenuCategory
            {
                RestaurantId = restaurantId,
                Name = dto.Name,
                Description = dto.Description,
                DisplayOrder = dto.DisplayOrder,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.MenuCategories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut("menuCategories/{id}")]
        public async Task<IActionResult> UpdateMenuCategory(int id, [FromBody] MenuCategoryDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var category = await _context.MenuCategories
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(c => c.Id == id && c.Restaurant!.OwnerId == adminId);
            if (category == null) return NotFound();

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.DisplayOrder = dto.DisplayOrder;
            category.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete("menuCategories/{id}")]
        public async Task<IActionResult> DeleteMenuCategory(int id)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var category = await _context.MenuCategories
                .Include(c => c.Restaurant)
                .FirstOrDefaultAsync(c => c.Id == id && c.Restaurant!.OwnerId == adminId);
            if (category == null) return NotFound();

            category.IsActive = false;
            category.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region Menu Items

        [HttpPost("menuItems")]
        public async Task<IActionResult> CreateMenuItem([FromBody] AdminCreateMenuItemDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == dto.RestaurantId && r.OwnerId == adminId);
            if (restaurant == null) return NotFound();

            var category = await _context.MenuCategories
                .FirstOrDefaultAsync(c => c.Id == dto.CategoryId && c.RestaurantId == dto.RestaurantId);
            if (category == null) return BadRequest(new { message = "Category not found for this restaurant" });

            var item = new MenuItem
            {
                RestaurantId = dto.RestaurantId,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description ?? string.Empty,
                Price = dto.Price,
                IsVegetarian = dto.IsVegetarian,
                PreparationTimeMinutes = dto.PreparationTimeMinutes,
                IsBestseller = dto.Tags?.Contains("bestseller") ?? false,
                IsSpicy = dto.Tags?.Contains("spicy") ?? false,
                Tags = SerializeTags(dto.Tags),  // store as string
                IsAvailable = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
            return Ok(MapMenuItem(item));
        }

        [HttpPut("menuItems/{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] AdminCreateMenuItemDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var item = await _context.MenuItems
                .Include(i => i.Restaurant)
                .FirstOrDefaultAsync(i => i.Id == id && i.Restaurant!.OwnerId == adminId);
            if (item == null) return NotFound();

            item.CategoryId = dto.CategoryId;
            item.Name = dto.Name;
            item.Description = dto.Description ?? string.Empty;
            item.Price = dto.Price;
            item.IsVegetarian = dto.IsVegetarian;
            item.PreparationTimeMinutes = dto.PreparationTimeMinutes;
            item.IsBestseller = dto.Tags?.Contains("bestseller") ?? false;
            item.IsSpicy = dto.Tags?.Contains("spicy") ?? false;
            item.Tags = SerializeTags(dto.Tags);  // store as string
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(MapMenuItem(item));
        }

        [HttpPatch("menuItems/{id}/toggle")]
        public async Task<IActionResult> ToggleMenuItem(int id)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var item = await _context.MenuItems
                .Include(i => i.Restaurant)
                .FirstOrDefaultAsync(i => i.Id == id && i.Restaurant!.OwnerId == adminId);
            if (item == null) return NotFound();

            item.IsAvailable = !item.IsAvailable;
            item.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(MapMenuItem(item));
        }

        [HttpDelete("menuItems/{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var item = await _context.MenuItems
                .Include(i => i.Restaurant)
                .FirstOrDefaultAsync(i => i.Id == id && i.Restaurant!.OwnerId == adminId);
            if (item == null) return NotFound();

            item.IsActive = false;
            item.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("menuItems/{id}/image")]
        public async Task<IActionResult> UploadMenuItemImage(int id, [FromForm] IFormFile image)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var item = await _context.MenuItems
                .Include(i => i.Restaurant)
                .FirstOrDefaultAsync(i => i.Id == id && i.Restaurant!.OwnerId == adminId);
            if (item == null) return NotFound();

            var url = await _cloudinaryService.UploadImageAsync(image, "menu-items");
            if (url == null) return StatusCode(500, new { message = "Image upload failed" });

            item.ImageUrl = url;
            item.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(new { url });
        }

        private static object MapMenuItem(MenuItem i) => new
        {
            i.Id,
            i.RestaurantId,
            i.CategoryId,
            i.Name,
            i.Description,
            i.Price,
            i.ImageUrl,
            i.IsAvailable,
            i.IsVegetarian,
            i.PreparationTimeMinutes,
            i.IsBestseller,
            i.IsSpicy,
            Tags = BuildTags(i)
        };

        private static List<string> BuildTags(MenuItem i)
        {
            var tags = string.IsNullOrWhiteSpace(i.Tags)
                ? new List<string>()
                : i.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                       .Select(t => t.Trim())
                       .ToList();

            // Also add boolean-based tags if not already present
            if (i.IsBestseller && !tags.Contains("bestseller")) tags.Add("bestseller");
            if (i.IsSpicy && !tags.Contains("spicy")) tags.Add("spicy");

            return tags;
        }

        // Convert tags list to comma-separated string for storage
        private static string SerializeTags(List<string>? tags)
        {
            if (tags == null || tags.Count == 0) return "";
            return string.Join(",", tags.Select(t => t.Trim().ToLower()));
        }

        #endregion

    }
}
