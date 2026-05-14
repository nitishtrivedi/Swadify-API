using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Enums;
using Swadify_API.Interfaces;
using System.Security.Claims;

namespace Swadify_API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/orders")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController(AppDbContext context, INotificationService notifications) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly INotificationService _notifications = notifications;

        [HttpGet("get-my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(adminIdClaim))
                return Unauthorized(new { message = "Admin ID claim is missing" });

            var adminId = int.Parse(adminIdClaim);

            var activeOrders = await _context.Orders
                .Include(o => o.Restaurant)
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .Where(o => o.Restaurant.OwnerId == adminId)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new
                {
                    o.Id,
                    o.CustomerId,
                    o.Customer.FirstName,
                    o.Customer.LastName,
                    o.RestaurantId,
                    Restaurant = new
                    {
                        o.Restaurant.Id,
                        o.Restaurant.Name,
                        o.Restaurant.LogoUrl
                    },

                    o.DeliveryPartnerId,
                    o.OrderNumber,
                    o.UniqueDeliveryCode,
                    o.Status,
                    o.PaymentStatus,
                    o.PaymentMethod,

                    o.SubTotal,
                    o.DeliveryFee,
                    o.TaxAmount,
                    o.DiscountAmount,
                    o.TotalAmount,

                    o.DeliveryAddressLine1,
                    o.DeliveryAddressLine2,
                    o.DeliveryCity,
                    o.DeliveryState,
                    o.DeliveryPinCode,

                    o.SpecialInstructions,

                    o.CreatedAt,
                    o.UpdatedAt,

                    OrderItems = o.OrderItems.Select(i => new
                    {
                        i.Id,
                        i.MenuItemId,
                        i.ItemName,
                        i.Quantity,
                        i.UnitPrice,
                        i.TotalPrice
                    })
                })
                .ToListAsync();
            return Ok(activeOrders);
        }

        [HttpPatch("update-order-status/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusDto dto)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return NotFound(new { message = "Order not found" });

            order.Status = dto.Status;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            //Notify customer about order status update
            NotificationType notificationType = order.Status switch
            {
                OrderStatus.Accepted => NotificationType.OrderConfirmed,

                OrderStatus.Preparing => NotificationType.OrderPreparing,

                OrderStatus.ReadyForPickup => NotificationType.OrderReadyForPickup,

                OrderStatus.OutForDelivery => NotificationType.OrderOutForDelivery,

                OrderStatus.Delivered => NotificationType.OrderDelivered,

                OrderStatus.Cancelled => NotificationType.OrderCancelled,

                _ => NotificationType.General
            };

            await _notifications.SendNotificationAsync(
                order.CustomerId,
                "Order Status Updated!",
                $"Your order #{order.OrderNumber} status has been updated.",
                notificationType,
                order.Id);
            return Ok(new { message = "Order status updated successfully", status = order.Status });
        }
    }
}
