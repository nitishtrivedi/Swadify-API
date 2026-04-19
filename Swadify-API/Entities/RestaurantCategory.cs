namespace Swadify_API.Entities
{
    public class RestaurantCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        // Navigation
        public ICollection<Restaurant>? Restaurants { get; set; }
    }
}
