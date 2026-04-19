using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.Entities;
using Swadify_API.Enums;
using Swadify_API.Hubs;
using Swadify_API.Interfaces;

namespace Swadify_API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(AppDbContext db, IHubContext<NotificationHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(int userId, string title, string message, NotificationType type, int? relatedEntityId = null)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                RelatedEntityId = relatedEntityId
            };

            _db.Notifications.Add(notification);
            await _db.SaveChangesAsync();

            // Send real-time via SignalR
            await _hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveNotification", new
            {
                Id = notification.Id,
                Title = title,
                Message = message,
                Type = type.ToString(),
                CreatedAt = notification.CreatedAt
            });
        }

        public async Task SendToRoleAsync(UserRole role, string title, string message, NotificationType type)
        {
            var users = await _db.Users.Where(u => u.Role == role && u.IsActive).ToListAsync();

            var notifications = users.Select(u => new Notification
            {
                UserId = u.Id,
                Title = title,
                Message = message,
                Type = type
            }).ToList();

            _db.Notifications.AddRange(notifications);
            await _db.SaveChangesAsync();

            await _hubContext.Clients.Group(role.ToString()).SendAsync("ReceiveNotification", new
            {
                Title = title,
                Message = message,
                Type = type.ToString()
            });
        }
    }
}
