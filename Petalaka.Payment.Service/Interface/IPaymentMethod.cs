using Petalaka.PetStore.Service.Services;

namespace Petalaka.Payment.Service.Interface;

public interface IPaymentMethod
{
    Task<CreatePremiumPlanPaymentResponse> ProcessPayment(CreatePremiumPlanPaymentRequest premiumPlanPayment);
}