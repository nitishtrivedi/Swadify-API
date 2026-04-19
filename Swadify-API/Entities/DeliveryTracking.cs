namespace Swadify_API.Entities
{
    public class DeliveryTracking : BaseEntity
    {
        public int OrderId { get; set; }
        public int DeliveryPartnerId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? SpeedKmh { get; set; }

        // Navigation
        public Order? Order { get; set; }
        public User? DeliveryPartner { get; set; }
    }
}
