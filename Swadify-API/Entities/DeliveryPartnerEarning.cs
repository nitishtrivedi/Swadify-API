namespace Swadify_API.Entities
{
    public class DeliveryPartnerEarning : BaseEntity
    {
        public int DeliveryPartnerId { get; set; }
        public int OrderId { get; set; }
        // Amount earned from this delivery
        public decimal Amount { get; set; }
        // Base delivery fee
        public decimal DeliveryFee { get; set; }
        // Bonus if applicable
        public decimal BonusAmount { get; set; } = 0;
        // Penalty if applicable
        public decimal PenaltyAmount { get; set; } = 0;
        // Final payable amount
        public decimal NetAmount { get; set; }
        public bool IsPaidOut { get; set; } = false;
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
        // Navigation
        public User? DeliveryPartner { get; set; }
        public Order? Order { get; set; }
    }
}
