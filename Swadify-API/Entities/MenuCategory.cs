using System.Text.Json.Serialization;

namespace Swadify_API.Entities
{
    public class MenuCategory : BaseEntity
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        // Navigation
        [JsonIgnore]
        public Restaurant? Restaurant { get; set; }
        public ICollection<MenuItem>? MenuItems { get; set; }
    }
}
