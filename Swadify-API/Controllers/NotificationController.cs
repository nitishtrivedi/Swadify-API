using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.Helpers;
using System.Security.Claims;

namespace Swadify_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext _db;
        private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public NotificationController(AppDbContext db) => _db = db;

        /// <summary>Get my notifications</summary>
        [HttpGet]
        public async Task<IActionResult> GetMyNotifications([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var query = _db.Notifications.Where(n => n.UserId == UserId).OrderByDescending(n => n.CreatedAt);
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new PagedResponse<object>
            {
                Data = items.Select(n => new
                {
                    n.Id,
                    n.Title,
                    n.Message,
                    Type = n.Type.ToString(),
                    n.IsRead,
                    n.RelatedEntityId,
                    n.CreatedAt
                }),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            });
        }

        /// <summary>Mark notification as read</summary>
        [HttpPatch("{id:int}/read")]
        public async Task<IActionResult> MarkRead(int id)
        {
            var notification = await _db.Notifications.FirstOrDefaultAsync(n => n.Id == id && n.UserId == UserId);
            if (notification == null) return NotFound();
            notification.IsRead = true;
            await _db.SaveChangesAsync();
            return Ok(ApiResponse<object>.Ok(null!, "Marked as read."));
        }

        /// <summary>Mark all notifications as read</summary>
        [HttpPatch("read-all")]
        public async Task<IActionResult> MarkAllRead()
        {
            var unread = await _db.Notifications.Where(n => n.UserId == UserId && !n.IsRead).ToListAsync();
            unread.ForEach(n => n.IsRead = true);
            await _db.SaveChangesAsync();
            return Ok(ApiResponse<object>.Ok(null!, $"{unread.Count} notifications marked as read."));
        }

        /// <summary>Get unread notification count</summary>
        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var count = await _db.Notifications.CountAsync(n => n.UserId == UserId && !n.IsRead);
            return Ok(ApiResponse<object>.Ok(new { count }));
        }
    }
}
