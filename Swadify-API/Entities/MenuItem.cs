namespace Swadify_API.Entities
{
    public class MenuItem : BaseEntity
    {
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }

        public bool IsAvailable { get; set; } = true;
        public bool IsVegetarian { get; set; } = false;
        public bool IsVegan { get; set; } = false;
        public bool IsGlutenFree { get; set; } = false;
        public bool IsBestseller { get; set; } = false;
        public bool IsSpicy { get; set; } = false;

        public int PreparationTimeMinutes { get; set; } = 15;
        public double AverageRating { get; set; } = 0.0;
        public int TotalRatings { get; set; } = 0;

        // Nutritional info (optional)
        public int? CaloriesKcal { get; set; }

        // Navigation
        public Restaurant? Restaurant { get; set; }
        public MenuCategory? Category { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
