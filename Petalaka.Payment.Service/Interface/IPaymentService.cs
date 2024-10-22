using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.Services;

namespace Petalaka.Payment.Service.Interface;

public interface IPaymentService
{
    Task<Dictionary<string, object>> CreateOrder(PaymentBusinessModel<PaymentService.Item> paymentBusinessModel);
}