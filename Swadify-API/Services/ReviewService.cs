using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;

namespace Swadify_API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _db;

        public ReviewService(AppDbContext db) => _db = db;

        public async Task<ReviewResponseDto> CreateReviewAsync(int customerId, CreateReviewDto dto)
        {
            var order = await _db.Orders
                .FirstOrDefaultAsync(o => o.Id == dto.OrderId && o.CustomerId == customerId)
                ?? throw new KeyNotFoundException("Order not found.");

            if (order.Status != OrderStatus.Delivered)
                throw new InvalidOperationException("You can only review completed orders.");

            if (await _db.Reviews.AnyAsync(r => r.OrderId == dto.OrderId))
                throw new InvalidOperationException("You have already reviewed this order.");

            var review = new Review
            {
                CustomerId = customerId,
                OrderId = dto.OrderId,
                RestaurantId = order.RestaurantId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                FoodRating = dto.FoodRating,
                DeliveryRating = dto.DeliveryRating
            };

            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            // Update restaurant average rating
            await UpdateRestaurantRating(order.RestaurantId);

            return await MapToDto(review);
        }

        public async Task<PagedResponse<ReviewResponseDto>> GetRestaurantReviewsAsync(int restaurantId, int page, int pageSize)
        {
            var query = _db.Reviews
                .Include(r => r.Customer)
                .Where(r => r.RestaurantId == restaurantId && r.IsApproved);

            var total = await query.CountAsync();
            var reviews = await query.OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var dtos = new List<ReviewResponseDto>();
            foreach (var r in reviews) dtos.Add(await MapToDto(r));

            return new PagedResponse<ReviewResponseDto> { Data = dtos, Page = page, PageSize = pageSize, TotalCount = total };
        }

        public async Task<ReviewResponseDto> AdminReplyAsync(int reviewId, int adminId, AdminReplyDto dto)
        {
            var review = await _db.Reviews.Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.Id == reviewId)
                ?? throw new KeyNotFoundException("Review not found.");

            review.AdminReply = dto.Reply;
            review.AdminRepliedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return await MapToDto(review);
        }

        public async Task<bool> ToggleApprovalAsync(int reviewId, int adminId)
        {
            var review = await _db.Reviews.FindAsync(reviewId)
                ?? throw new KeyNotFoundException("Review not found.");

            review.IsApproved = !review.IsApproved;
            await _db.SaveChangesAsync();
            return review.IsApproved;
        }

        private async Task UpdateRestaurantRating(int restaurantId)
        {
            var restaurant = await _db.Restaurants.FindAsync(restaurantId);
            if (restaurant == null) return;

            var ratings = await _db.Reviews.Where(r => r.RestaurantId == restaurantId && r.IsApproved).ToListAsync();
            restaurant.TotalRatings = ratings.Count;
            restaurant.AverageRating = ratings.Any() ? Math.Round(ratings.Average(r => r.Rating), 1) : 0;
            await _db.SaveChangesAsync();
        }

        private static Task<ReviewResponseDto> MapToDto(Review r) => Task.FromResult(new ReviewResponseDto
        {
            Id = r.Id,
            CustomerName = r.Customer != null ? $"{r.Customer.FirstName} {r.Customer.LastName}" : "Anonymous",
            CustomerImage = r.Customer?.ProfileImageUrl,
            Rating = r.Rating,
            Comment = r.Comment,
            FoodRating = r.FoodRating,
            DeliveryRating = r.DeliveryRating,
            IsApproved = r.IsApproved,
            AdminReply = r.AdminReply,
            AdminRepliedAt = r.AdminRepliedAt,
            CreatedAt = r.CreatedAt
        });
    }
}
