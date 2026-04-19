using Swadify_API.DTOs;
using Swadify_API.Enums;
using Swadify_API.Helpers;

namespace Swadify_API.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(int customerId, CreateOrderDto dto);
        Task<OrderResponseDto?> GetOrderByIdAsync(int orderId, int requesterId, UserRole requesterRole);
        Task<PagedResponse<OrderResponseDto>> GetMyOrdersAsync(int customerId, int page, int pageSize, OrderStatus? status);
        Task<PagedResponse<OrderResponseDto>> GetRestaurantOrdersAsync(int restaurantId, int requesterId, int page, int pageSize, OrderStatus? status);
        Task<PagedResponse<OrderResponseDto>> GetAllOrdersAsync(int page, int pageSize, OrderStatus? status); // SuperAdmin
        Task<OrderResponseDto> UpdateOrderStatusAsync(int orderId, int requesterId, UserRole requesterRole, UpdateOrderStatusDto dto);
        Task<OrderResponseDto> AssignDeliveryPartnerAsync(int orderId, int requesterId, AssignDeliveryPartnerDto dto);
        Task<PagedResponse<OrderResponseDto>> GetDeliveryPartnerOrdersAsync(int deliveryPartnerId, int page, int pageSize, OrderStatus? status);
    }
}
