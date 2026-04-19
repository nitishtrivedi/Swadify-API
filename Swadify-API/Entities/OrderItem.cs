namespace Swadify_API.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }

        // Snapshot of item at order time
        public string ItemName { get; set; } = string.Empty;
        public string? ItemDescription { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? SpecialInstructions { get; set; }

        // Navigation
        public Order? Order { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
