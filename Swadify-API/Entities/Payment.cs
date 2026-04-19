using Swadify_API.Enums;

namespace Swadify_API.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public PaymentMethod Method { get; set; }

        public string? RazorpayOrderId { get; set; }
        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }

        public string? FailureReason { get; set; }
        public DateTime? PaidAt { get; set; }

        // Navigation
        public Order? Order { get; set; }
    }
}
