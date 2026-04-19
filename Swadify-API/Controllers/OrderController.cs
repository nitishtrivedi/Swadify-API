using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swadify_API.DTOs;
using Swadify_API.Enums;
using Swadify_API.Helpers;
using Swadify_API.Interfaces;
using System.Security.Claims;

namespace Swadify_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        private UserRole UserRole => Enum.Parse<UserRole>(User.FindFirst(ClaimTypes.Role)!.Value);

        public OrderController(IOrderService service) => _service = service;

        /// <summary>Place a new order from cart</summary>
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var result = await _service.CreateOrderAsync(UserId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<OrderResponseDto>.Created(result, "Order placed successfully."));
        }

        /// <summary>Get order by ID</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetOrderByIdAsync(id, UserId, UserRole);
            if (result == null) return NotFound(ApiResponse<OrderResponseDto>.NotFound());
            return Ok(ApiResponse<OrderResponseDto>.Ok(result));
        }

        /// <summary>Get my orders (Customer)</summary>
        [HttpGet("my")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] OrderStatus? status = null)
        {
            var result = await _service.GetMyOrdersAsync(UserId, page, pageSize, status);
            return Ok(result);
        }

        /// <summary>Get restaurant orders (Admin)</summary>
        [HttpGet("restaurant/{restaurantId:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> GetRestaurantOrders(int restaurantId,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] OrderStatus? status = null)
        {
            var result = await _service.GetRestaurantOrdersAsync(restaurantId, UserId, page, pageSize, status);
            return Ok(result);
        }

        /// <summary>Get all orders (SuperAdmin)</summary>
        [HttpGet("all")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] OrderStatus? status = null)
        {
            var result = await _service.GetAllOrdersAsync(page, pageSize, status);
            return Ok(result);
        }

        /// <summary>Update order status</summary>
        [HttpPatch("{id:int}/status")]
        [Authorize(Roles = "Admin,SuperAdmin,DeliveryPartner")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var result = await _service.UpdateOrderStatusAsync(id, UserId, UserRole, dto);
            return Ok(ApiResponse<OrderResponseDto>.Ok(result, "Order status updated."));
        }

        /// <summary>Assign delivery partner to order</summary>
        [HttpPost("{id:int}/assign-delivery")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> AssignDelivery(int id, [FromBody] AssignDeliveryPartnerDto dto)
        {
            var result = await _service.AssignDeliveryPartnerAsync(id, UserId, dto);
            return Ok(ApiResponse<OrderResponseDto>.Ok(result, "Delivery partner assigned."));
        }

        /// <summary>Get delivery partner orders</summary>
        [HttpGet("delivery-assignments")]
        [Authorize(Roles = "DeliveryPartner")]
        public async Task<IActionResult> GetDeliveryAssignments(
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] OrderStatus? status = null)
        {
            var result = await _service.GetDeliveryPartnerOrdersAsync(UserId, page, pageSize, status);
            return Ok(result);
        }
    }
}
