using Swadify_API.DTOs;

namespace Swadify_API.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> InitiatePaymentAsync(int userId, InitiatePaymentDto dto);
        Task<bool> VerifyPaymentAsync(VerifyPaymentDto dto);
        Task<bool> HandleWebhookAsync(string payload, string signature);
    }
}
