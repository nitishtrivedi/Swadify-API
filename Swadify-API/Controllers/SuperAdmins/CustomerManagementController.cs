using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;
using Swadify_API.DTOs;
using Swadify_API.Helpers;

namespace Swadify_API.Controllers.SuperAdmins
{
    [ApiController]
    [Route("api/super-admins/customer-management")]
    [Authorize(Roles = "SuperAdmin")]
    public class CustomerManagementController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("get-all-customers")]
        public async Task<IActionResult> GetAllCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var query = _context.Users.AsQueryable();
            var total = await query.CountAsync();
            var users = await query.OrderByDescending(u => u.CreatedAt).Where(u => u.Role == Enums.UserRole.Customer)
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new PagedResponse<object>
            {
                Data = users.Select(u => new
                {
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.Username,
                    Role = u.Role.ToString(),
                    u.IsActive,
                    u.CreatedAt,
                    u.IsEmailVerified
                }),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            });
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var totalAdmins = await _context.Users.CountAsync(u => u.Role == Enums.UserRole.Admin);
            var totalActiveAdmins = await _context.Users.CountAsync(u => u.Role == Enums.UserRole.Admin && u.IsActive);
            var totalCustomers = await _context.Users.CountAsync(u => u.Role == Enums.UserRole.Customer);
            var newCustomersToday = await _context.Users.CountAsync(u => u.Role == Enums.UserRole.Customer && u.CreatedAt >= DateTime.UtcNow.AddDays(-1));
            var totalOrders = await _context.Orders.CountAsync();
            var totalRevenue = await _context.Orders.Where(o => o.Status == Enums.OrderStatus.Delivered).SumAsync(o => o.TotalAmount);
            var totalRestaurants = await _context.Restaurants.CountAsync(r => r.IsActive);
            var totalPartners = await _context.Users.CountAsync(u => u.Role == Enums.UserRole.DeliveryPartner);
            var stats = new SuperAdminStatsDto
            {
                TotalAdmins = totalAdmins,
                ActiveAdmins = totalActiveAdmins,
                TotalCustomers = totalCustomers,
                NewCustomersToday = newCustomersToday,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                TotalRestaurants = totalRestaurants,
                TotalPartners = totalPartners
            };

            return Ok(stats);
        }
    }
}
