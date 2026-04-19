using Razorpay.Api;
using Swadify_API.Enums;

namespace Swadify_API.Entities
{
    public class Restaurant : BaseEntity
    {
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? CoverImageUrl { get; set; }

        // Contact
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Address
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PinCode { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Status & Availability
        public RestaurantStatus Status { get; set; } = RestaurantStatus.Closed;
        public bool IsActive { get; set; } = true;
        public bool IsVerified { get; set; } = false;
        public TimeOnly OpeningTime { get; set; }
        public TimeOnly ClosingTime { get; set; }

        // Ratings
        public double AverageRating { get; set; } = 0.0;
        public int TotalRatings { get; set; } = 0;

        // Delivery settings
        public decimal DeliveryFee { get; set; } = 0;
        public decimal MinimumOrderAmount { get; set; } = 0;
        public int EstimatedDeliveryTimeMinutes { get; set; } = 30;
        public double DeliveryRadiusKm { get; set; } = 5.0;

        // Navigation
        public User? Owner { get; set; }
        public RestaurantCategory? Category { get; set; }
        public ICollection<MenuItem>? MenuItems { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
