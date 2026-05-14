using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swadify_API.Data;

namespace Swadify_API.Controllers.DeliveryPartner
{
    [ApiController]
    [Route("api/delivery-partner")]
    [Authorize(Roles = "DeliveryPartner")]
    public class DPDashboardController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("dashboard/active-deliveries")]
        public async Task<IActionResult> GetActiveDeliveries()
        {
            var activeDeliveries = await _context.Orders
                .Include(o => o.Restaurant)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)!
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.DeliveryPartnerId == null && o.Status == Enums.OrderStatus.ReadyForPickup)
                .Select(o => new
                {
                    id = o.Id.ToString(),

                    customerId = o.CustomerId.ToString(),

                    customerName =o.Customer != null ? $"{o.Customer.FirstName} {o.Customer.LastName}".Trim() : "",

                    orderNumber = o.OrderNumber,

                    status = o.Status.ToString(),

                    paymentMethod = o.PaymentMethod.ToString(),

                    paymentStatus = o.PaymentStatus.ToString(),

                    subtotal = o.SubTotal,

                    deliveryFee = o.DeliveryFee,

                    discount = o.DiscountAmount,

                    total = o.TotalAmount,

                    otp = o.UniqueDeliveryCode,

                    cancelReason = o.CancellationReason,

                    createdAt = o.CreatedAt,

                    updatedAt = o.UpdatedAt,

                    deliveryPartnerId =
                o.DeliveryPartnerId != null
                    ? o.DeliveryPartnerId.ToString()
                    : null,

                    deliveryPartnerName = "",

                    restaurant = new
                    {
                        id = o.Restaurant.Id.ToString(),

                        name = o.Restaurant.Name,

                        logoUrl = o.Restaurant.LogoUrl
                    },

                    deliveryAddress = new
                    {
                        line1 = o.DeliveryAddressLine1,

                        line2 = o.DeliveryAddressLine2,

                        city = o.DeliveryCity,

                        state = o.DeliveryState,

                        pincode = o.DeliveryPinCode,

                        lat = o.DeliveryLatitude,

                        lng = o.DeliveryLongitude
                    },

                    items = o.OrderItems.Select(oi => new
                    {
                        quantity = oi.Quantity,

                        menuItem = new
                        {
                            id = oi.MenuItem.Id.ToString(),

                            categoryId = oi.MenuItem.CategoryId.ToString(),

                            restaurantId = oi.MenuItem.RestaurantId.ToString(),

                            name = oi.MenuItem.Name,

                            description = oi.MenuItem.Description,

                            price = oi.UnitPrice,

                            imageUrl = oi.MenuItem.ImageUrl,

                            isVeg = oi.MenuItem.IsVegetarian,

                            isAvailable = oi.MenuItem.IsAvailable,

                            preparationTimeMin =
                                oi.MenuItem.PreparationTimeMinutes
                        }
                    })
                })
                .ToListAsync();
            return Ok(activeDeliveries);
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var deliveryPartner = await _context.Users.Include(x => x.DeliveryProfile).FirstOrDefaultAsync(x => x.Id == id);
            if (deliveryPartner == null)
            {
                return NotFound();
            }
            if (deliveryPartner.DeliveryProfile == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                id = deliveryPartner.Id,
                firstName = deliveryPartner.FirstName,
                lastName = deliveryPartner.LastName,
                email = deliveryPartner.Email,
                phoneNumber = deliveryPartner.PhoneNumber,
                isOnline = deliveryPartner.DeliveryProfile.IsOnline,
                isAvailable = deliveryPartner.DeliveryProfile.IsAvailable
            });
        }

        [HttpPatch("toggle/{id}")]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var deliveryPartner = await _context.Users.Include(x => x.DeliveryProfile).FirstOrDefaultAsync(x => x.Id == id);
            if (deliveryPartner == null)
            {
                return NotFound();
            }

            if (deliveryPartner.DeliveryProfile == null)
            {
                return NotFound();
            }

            deliveryPartner.DeliveryProfile.IsOnline = !deliveryPartner.DeliveryProfile.IsOnline;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = deliveryPartner.Id,

                isOnline = deliveryPartner.DeliveryProfile.IsOnline,

                isAvailable = deliveryPartner.DeliveryProfile.IsAvailable
            });
        }
    }
}
