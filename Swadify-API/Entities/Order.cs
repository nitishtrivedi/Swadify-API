using Razorpay.Api;
using Swadify_API.Enums;

namespace Swadify_API.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int? DeliveryPartnerId { get; set; }

        public string OrderNumber { get; set; } = string.Empty; // e.g. FD-20240101-0001
        public string UniqueDeliveryCode { get; set; } = string.Empty; // 4-digit
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; set; }

        // Amounts
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
        public decimal TotalAmount { get; set; }

        // Delivery address (snapshot at order time)
        public string DeliveryAddressLine1 { get; set; } = string.Empty;
        public string? DeliveryAddressLine2 { get; set; }
        public string DeliveryCity { get; set; } = string.Empty;
        public string DeliveryState { get; set; } = string.Empty;
        public string DeliveryPinCode { get; set; } = string.Empty;
        public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude { get; set; }

        public string? SpecialInstructions { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }

        // Razorpay
        public string? RazorpayOrderId { get; set; }
        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }

        public string? CancellationReason { get; set; }

        // Navigation
        public User? Customer { get; set; }
        public Restaurant? Restaurant { get; set; }
        public User? DeliveryPartner { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public Payment? Payment { get; set; }
        public Review? Review { get; set; }
    }
}
