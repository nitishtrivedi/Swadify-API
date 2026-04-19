using Swadify_API.DTOs;
using Swadify_API.Helpers;

namespace Swadify_API.Interfaces
{
    public interface IDeliveryService
    {
        Task<bool> UpdateLocationAsync(int deliveryPartnerId, UpdateLocationDto dto);
        Task<DeliveryAssignmentDto?> GetCurrentAssignmentAsync(int deliveryPartnerId);
        Task<PagedResponse<DeliveryAssignmentDto>> GetAssignmentHistoryAsync(int deliveryPartnerId, int page, int pageSize);
        Task<bool> UpdateStatusAsync(int deliveryPartnerId, DeliveryPartnerStatusDto dto);
        Task<LocationResponseDto?> GetDeliveryPartnerLocationAsync(int orderId, int requesterId);
    }
}
