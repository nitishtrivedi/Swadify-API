namespace Swadify_API.Entities
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? SpecialInstructions { get; set; }

        // Navigation
        public Cart? Cart { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
