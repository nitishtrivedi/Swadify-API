namespace Swadify_API.Entities
{
    public class Review : BaseEntity
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int RestaurantId { get; set; }
        public int? MenuItemId { get; set; }

        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }

        // For food item reviews
        public int? FoodRating { get; set; }
        // For delivery reviews
        public int? DeliveryRating { get; set; }

        public bool IsApproved { get; set; } = true;
        public string? AdminReply { get; set; }
        public DateTime? AdminRepliedAt { get; set; }

        // Image URLs (comma separated, optional)
        public string? ImageUrls { get; set; }

        // Navigation
        public User? Customer { get; set; }
        public Order? Order { get; set; }
        public Restaurant? Restaurant { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
