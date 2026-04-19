using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Enums;
using Swadify_API.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Swadify_API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        private readonly INotificationService _notifications;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(AppDbContext db, IConfiguration config, INotificationService notifications, ILogger<PaymentService> logger)
        {
            _db = db;
            _config = config;
            _notifications = notifications;
            _logger = logger;
        }

        public async Task<PaymentResponseDto> InitiatePaymentAsync(int userId, InitiatePaymentDto dto)
        {
            var order = await _db.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == dto.OrderId && o.CustomerId == userId)
                ?? throw new KeyNotFoundException("Order not found.");

            if (order.PaymentMethod == PaymentMethod.COD)
                throw new InvalidOperationException("COD orders don't require online payment initiation.");

            if (order.PaymentStatus == PaymentStatus.Completed)
                throw new InvalidOperationException("Order is already paid.");

            var client = new RazorpayClient(_config["Razorpay:KeyId"], _config["Razorpay:KeySecret"]);

            var options = new Dictionary<string, object>
        {
            { "amount", (int)(order.TotalAmount * 100) }, // paise
            { "currency", "INR" },
            { "receipt", order.OrderNumber },
            { "notes", new Dictionary<string, string> { { "orderId", order.Id.ToString() } } }
        };

            var razorpayOrder = client.Order.Create(options);
            var razorpayOrderId = razorpayOrder["id"].ToString();

            order.RazorpayOrderId = razorpayOrderId;
            await _db.SaveChangesAsync();

            _logger.LogInformation("Payment initiated for order {OrderNumber}", order.OrderNumber);

            return new PaymentResponseDto
            {
                Success = true,
                RazorpayOrderId = razorpayOrderId,
                Amount = order.TotalAmount,
                Currency = "INR",
                RazorpayKeyId = _config["Razorpay:KeyId"],
                OrderNumber = order.OrderNumber,
                CustomerName = $"{order.Customer!.FirstName} {order.Customer.LastName}",
                CustomerEmail = order.Customer.Email,
                CustomerPhone = order.Customer.PhoneNumber
            };
        }

        public async Task<bool> VerifyPaymentAsync(VerifyPaymentDto dto)
        {
            var order = await _db.Orders
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == dto.OrderId)
                ?? throw new KeyNotFoundException("Order not found.");

            // Verify signature
            var text = $"{dto.RazorpayOrderId}|{dto.RazorpayPaymentId}";
            var secret = _config["Razorpay:KeySecret"]!;
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
            var computedSignature = Convert.ToHexString(hash).ToLower();

            if (computedSignature != dto.RazorpaySignature)
            {
                _logger.LogWarning("Payment signature verification failed for order {OrderId}", dto.OrderId);
                return false;
            }

            order.RazorpayPaymentId = dto.RazorpayPaymentId;
            order.RazorpaySignature = dto.RazorpaySignature;
            order.PaymentStatus = PaymentStatus.Completed;
            order.Status = OrderStatus.Confirmed;

            if (order.Payment != null)
            {
                order.Payment.Status = PaymentStatus.Completed;
                order.Payment.RazorpayPaymentId = dto.RazorpayPaymentId;
                order.Payment.RazorpaySignature = dto.RazorpaySignature;
                order.Payment.PaidAt = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();

            await _notifications.SendNotificationAsync(order.CustomerId, "Payment Successful!",
                $"Payment for order #{order.OrderNumber} confirmed.", NotificationType.PaymentSuccessful, order.Id);

            _logger.LogInformation("Payment verified for order {OrderNumber}", order.OrderNumber);
            return true;
        }

        public async Task<bool> HandleWebhookAsync(string payload, string signature)
        {
            var secret = _config["Razorpay:WebhookSecret"]!;
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            var computed = Convert.ToHexString(hash).ToLower();

            if (computed != signature)
            {
                _logger.LogWarning("Webhook signature mismatch.");
                return await Task.FromResult(false);
            }

            _logger.LogInformation("Webhook received and verified.");
            return true;
        }
    }
}
