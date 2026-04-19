namespace Swadify_API.Entities
{
    public class Cart : BaseEntity
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }

        // Navigation
        public User? Customer { get; set; }
        public Restaurant? Restaurant { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
