namespace Swadify_API.Enums
{
    public enum OrderStatus
    {
        Received = 1,
        Accepted = 2,
        Preparing = 3,
        ReadyForPickup = 4,

        AssignedToDelivery = 5,
        OutForDelivery = 6,
        Delivered = 7,

        Cancelled = 8,
        Failed = 9
    }
}
