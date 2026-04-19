using Swadify_API.Enums;

namespace Swadify_API.Interfaces
{
        public interface INotificationService
        {
            Task SendNotificationAsync(int userId, string title, string message, NotificationType type, int? relatedEntityId = null);
            Task SendToRoleAsync(UserRole role, string title, string message, NotificationType type);
        }
}
