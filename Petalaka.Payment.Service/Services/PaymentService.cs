using Grpc.Core;
using Petalaka.Payment.Core.Utils;
using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;
using Petalaka.PetStore.Service.Services;

namespace Petalaka.Payment.Service.Services;

public class PaymentService : PaymentManager.PaymentManagerBase, IPaymentService
{
    private readonly PaymentMethodFactory _paymentMethodFactory;

    public PaymentService(PaymentMethodFactory paymentMethodFactory)
    {
        _paymentMethodFactory = paymentMethodFactory;
    }

    /*public async Task<Dictionary<string, object>> CreateOrder(PaymentBusinessModel<Item> paymentBusinessModel)
    {
        var orderCreationSettings = new OrderCreationSettings<Item>()
        {
            AppUser = paymentBusinessModel.UserId,
            ApptransId = paymentBusinessModel.TransactionId,
            Amount = paymentBusinessModel.Amount,
            Description = paymentBusinessModel.Description,
            Items = paymentBusinessModel.Items,
            BankCode = "",
            UserPhone = paymentBusinessModel.UserPhone,
            UserEmail = paymentBusinessModel.UserEmail,
            UserAddress = paymentBusinessModel.UserAddress
        };
        return await _zaloPayService.CreateOrderUrl(orderCreationSettings);
    }*/
    public override async Task<CreatePremiumPlanPaymentResponse> CreatePremiumPlanPayment(
        CreatePremiumPlanPaymentRequest request, ServerCallContext context)
    {
        var paymentMethod = await _paymentMethodFactory.GetPaymentMethod(request.PaymentMethod);
        var response = await paymentMethod.ProcessPayment(request);
        if (response.Code != 1)
        {
            return (new CreatePremiumPlanPaymentResponse()
            {
                Message = response.Message,
                Code = (int)response.Code!,
                Success = false
            });
        }

        return (new CreatePremiumPlanPaymentResponse()
        {
            Message = "Create premium plan payment successfully",
            PaymentUrl = response.PaymentUrl,
            ZaloPayToken = response.ZaloPayToken,
            Code = (int)response.Code!,
            Success = true,
            OrderCode = response.OrderCode
        });
    }
}