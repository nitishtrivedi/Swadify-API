namespace Swadify_API.Enums
{
    public enum NotificationType
    {
        OrderPlaced = 1,
        OrderConfirmed = 2,
        OrderPreparing = 3,
        OrderReadyForPickup = 4,
        OrderOutForDelivery = 5,
        OrderDelivered = 6,
        OrderCancelled = 7,
        PaymentSuccessful = 8,
        PaymentFailed = 9,
        NewOrderAlert = 10,
        DeliveryAssigned = 11,
        General = 12
    }
}
