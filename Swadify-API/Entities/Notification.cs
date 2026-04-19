using Swadify_API.Enums;

namespace Swadify_API.Entities
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; } = false;
        public int? RelatedEntityId { get; set; }
        public string? ActionUrl { get; set; }

        // Navigation
        public User? User { get; set; }
    }
}
