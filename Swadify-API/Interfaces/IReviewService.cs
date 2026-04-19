using Swadify_API.DTOs;
using Swadify_API.Helpers;

namespace Swadify_API.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewResponseDto> CreateReviewAsync(int customerId, CreateReviewDto dto);
        Task<PagedResponse<ReviewResponseDto>> GetRestaurantReviewsAsync(int restaurantId, int page, int pageSize);
        Task<ReviewResponseDto> AdminReplyAsync(int reviewId, int adminId, AdminReplyDto dto);
        Task<bool> ToggleApprovalAsync(int reviewId, int adminId);
    }
}

