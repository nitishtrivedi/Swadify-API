namespace Swadify_API.Entities
{
    public class MenuCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<MenuItem>? MenuItems { get; set; }
    }
}
