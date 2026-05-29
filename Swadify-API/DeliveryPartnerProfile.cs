using Swadify_API.Entities;
using Swadify_API.Enums;

namespace Swadify_API
{
    public class DeliveryPartnerProfile : BaseEntity
    {
        public int UserId { get; set; }
        public string VehicleType { get; set; } = string.Empty; // Bike, Cycle, Car
        public string VehicleNumber { get; set; } = string.Empty;
        public string? LicenseNumber { get; set; }
        public bool IsAvailable { get; set; } = false;
        public bool IsOnline { get; set; } = false;
        public double? CurrentLatitude { get; set; }
        public double? CurrentLongitude { get; set; }
        public DateTime? LastLocationUpdate { get; set; }
        public int TotalDeliveries { get; set; } = 0;
        public double AverageRating { get; set; } = 0.0;

        public decimal TotalEarnings { get; set; } = 0;
        public decimal PendingEarnings { get; set; } = 0;
        public decimal WithdrawnEarnings { get; set; } = 0;
        public ApplicationStatus ApplicationStatus { get; set; } = ApplicationStatus.Pending;
        public string? AadharNumber { get; set; }  // Add this
        public string? RejectionReason { get; set; }  // Add this

        // Navigation
        public User? User { get; set; }
    }
}
