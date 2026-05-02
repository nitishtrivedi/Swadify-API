using Swadify_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Swadify_API.DTOs
{
    #region Authentication DTOs
    public class RegisterDto
    {
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string Username { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required, Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MinLength(8)] public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Customer; // Default customer
    }

    public class CustomerRegisterDto
    {
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string Username { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required, Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MinLength(8)] public string Password { get; set; } = string.Empty;
    }

    public class SuperAdminRegisterDto
    {
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string Username { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required, Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MinLength(8)] public string Password { get; set; } = string.Empty;
    }

    public class DeliveryPartnerRegisterDto
    {
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string Username { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required, Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MinLength(8)] public string Password { get; set; } = string.Empty;
        [Required] public string VehicleType { get; set; } = string.Empty;
        [Required] public string VehicleNumber { get; set; } = string.Empty;
        public string? LicenseNumber { get; set; }
    }

    #endregion

    #region Login DTOs
    public class LoginDto
    {
        [Required]
        public string Identifier { get; set; } = string.Empty; // email, username, or phone
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class RefreshTokenDto
    {
        [Required] public string AccessToken { get; set; } = string.Empty;
        [Required] public string RefreshToken { get; set; } = string.Empty;
    }
    #endregion

    #region Auth Response DTOs
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public UserProfileDto User { get; set; } = null!;
    }

    public class UserProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
    #endregion

    #region Restaurant DTOs
    public class CreateRestaurantDto
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Description { get; set; } = string.Empty;
        [Required] public int CategoryId { get; set; }
        [Required, Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        [Required] public string City { get; set; } = string.Empty;
        [Required] public string State { get; set; } = string.Empty;
        [Required] public string PinCode { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public TimeOnly OpeningTime { get; set; }
        public TimeOnly ClosingTime { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public int EstimatedDeliveryTimeMinutes { get; set; } = 30;
        public double DeliveryRadiusKm { get; set; } = 5.0;
        public bool IsFeatured { get; set; }
    }

    public class UpdateRestaurantDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public RestaurantStatus? Status { get; set; }
        public TimeOnly? OpeningTime { get; set; }
        public TimeOnly? ClosingTime { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal? MinimumOrderAmount { get; set; }
        public int? EstimatedDeliveryTimeMinutes { get; set; }
        public double? DeliveryRadiusKm { get; set; }
        public bool? IsFeatured { get; set; }
    }

    public class RestaurantResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PinCode { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public bool IsFeatured { get; set; }
        public string OpeningTime { get; set; } = string.Empty;
        public string ClosingTime { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public int EstimatedDeliveryTimeMinutes { get; set; }
        public double DeliveryRadiusKm { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
    #endregion

    #region Menu Item DTOs
    public class CreateMenuItemDto
    {
        [Required] public int RestaurantId { get; set; }
        [Required] public int CategoryId { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Description { get; set; } = string.Empty;
        [Required, Range(0.01, 100000)] public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsSpicy { get; set; }
        public int PreparationTimeMinutes { get; set; } = 15;
        public int? CaloriesKcal { get; set; }
    }

    public class UpdateMenuItemDto
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsVegetarian { get; set; }
        public bool? IsVegan { get; set; }
        public bool? IsGlutenFree { get; set; }
        public bool? IsSpicy { get; set; }
        public bool? IsBestseller { get; set; }
        public int? PreparationTimeMinutes { get; set; }
        public int? CaloriesKcal { get; set; }
    }

    public class MenuItemResponseDto
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public decimal EffectivePrice => DiscountedPrice.HasValue && DiscountedPrice > 0 ? DiscountedPrice.Value : Price;
        public bool IsAvailable { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsBestseller { get; set; }
        public bool IsSpicy { get; set; }
        public int PreparationTimeMinutes { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public int? CaloriesKcal { get; set; }
    }
    #endregion

    #region Order DTOs
    public class CreateOrderDto
    {
        [Required] public int RestaurantId { get; set; }
        [Required] public PaymentMethod PaymentMethod { get; set; }
        [Required] public string DeliveryAddressLine1 { get; set; } = string.Empty;
        public string? DeliveryAddressLine2 { get; set; }
        [Required] public string DeliveryCity { get; set; } = string.Empty;
        [Required] public string DeliveryState { get; set; } = string.Empty;
        [Required] public string DeliveryPinCode { get; set; } = string.Empty;
        public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude { get; set; }
        public string? SpecialInstructions { get; set; }
    }

    public class UpdateOrderStatusDto
    {
        [Required] public OrderStatus Status { get; set; }
        public string? CancellationReason { get; set; }
    }

    public class AssignDeliveryPartnerDto
    {
        [Required] public int DeliveryPartnerId { get; set; }
    }

    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string UniqueDeliveryCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string RestaurantName { get; set; } = string.Empty;
        public string? DeliveryPartnerName { get; set; }
        public string? DeliveryPartnerPhone { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string? SpecialInstructions { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }
        public string? CancellationReason { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = [];
        public DateTime CreatedAt { get; set; }
    }

    public class OrderItemResponseDto
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? SpecialInstructions { get; set; }
    }
    #endregion

    #region Cart DTOs
    public class AddToCartDto
    {
        [Required] public int MenuItemId { get; set; }
        [Required, Range(1, 50)] public int Quantity { get; set; }
        public string? SpecialInstructions { get; set; }
    }

    public class UpdateCartItemDto
    {
        [Required, Range(0, 50)] public int Quantity { get; set; } // 0 = remove
    }

    public class CartResponseDto
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public List<CartItemResponseDto> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public int TotalItems { get; set; }
    }

    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public string? SpecialInstructions { get; set; }
        public bool IsAvailable { get; set; }
    }
    #endregion

    #region Payment DTOs
    public class InitiatePaymentDto
    {
        [Required] public int OrderId { get; set; }
    }

    public class VerifyPaymentDto
    {
        [Required] public string RazorpayOrderId { get; set; } = string.Empty;
        [Required] public string RazorpayPaymentId { get; set; } = string.Empty;
        [Required] public string RazorpaySignature { get; set; } = string.Empty;
        [Required] public int OrderId { get; set; }
    }

    public class PaymentResponseDto
    {
        public bool Success { get; set; }
        public string? RazorpayOrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
        public string? RazorpayKeyId { get; set; }
        public string? OrderNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
    }
    #endregion

    #region Review DTOs
    public class CreateReviewDto
    {
        [Required] public int OrderId { get; set; }
        [Required, Range(1, 5)] public int Rating { get; set; }
        public string? Comment { get; set; }
        public int? FoodRating { get; set; }
        public int? DeliveryRating { get; set; }
    }

    public class AdminReplyDto
    {
        [Required] public string Reply { get; set; } = string.Empty;
    }

    public class ReviewResponseDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? CustomerImage { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int? FoodRating { get; set; }
        public int? DeliveryRating { get; set; }
        public bool IsApproved { get; set; }
        public string? AdminReply { get; set; }
        public DateTime? AdminRepliedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    #endregion

    #region Delivery DTOs
    public class UpdateLocationDto
    {
        [Required] public double Latitude { get; set; }
        [Required] public double Longitude { get; set; }
        public double? SpeedKmh { get; set; }
    }

    public class CompleteDeliveryDto
    {
        [Required] public int OrderId { get; set; }
        public bool IsCompleted { get; set; } = true;
        public string? FailureReason { get; set; }
    }

    public class DeliveryAssignmentDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string UniqueDeliveryCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public bool IsPrepaid { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string RestaurantAddress { get; set; } = string.Empty;
        public string? SpecialInstructions { get; set; }
    }

    public class DeliveryPartnerStatusDto
    {
        [Required] public bool IsAvailable { get; set; }
        [Required] public bool IsOnline { get; set; }
    }
    #endregion

    #region User DTOs
    public class UpdateProfileDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
    }

    public class ChangePasswordDto
    {
        [Required] public string CurrentPassword { get; set; } = string.Empty;
        [Required, MinLength(8)] public string NewPassword { get; set; } = string.Empty;
    }

    public class AdminCreateUserDto
    {
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string Username { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required, Phone] public string PhoneNumber { get; set; } = string.Empty;
        [Required, MinLength(8)] public string Password { get; set; } = string.Empty;
    }
    #endregion

    public class AdminCreateAdminDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Admin;
    }

    public class UpdateAdminDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; } // optional
    }

    public class RestaurantCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public int DisplayOrder { get; set; }
    }


    public class LocationResponseDto
    {
        public int DeliveryPartnerId { get; set; }
        public string DeliveryPartnerName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
