using Petalaka.Payment.Core.Utils;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Repository.Interface;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;
using Petalaka.PetStore.Service.Services;

namespace Petalaka.Payment.Service.Services;

public class ZaloPayPayment : IPaymentMethod
{
    private readonly IZaloPayService _zaloPayService;
    private readonly IUnitOfWork _unitOfWork;

    public ZaloPayPayment(IZaloPayService zaloPayService, IUnitOfWork unitOfWork)
    {
        _zaloPayService = zaloPayService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreatePremiumPlanPaymentResponse> ProcessPayment(CreatePremiumPlanPaymentRequest request)
    {
        var orderCreationSettings = new OrderCreationSettings<PurchasePremiumPlanPayment>()
        {
            AppUser = request.ProviderId,
            ApptransId = request.PremiumPlan.Id + "_" + TimeStampHelper.GenerateUnixTimeStampToMilisecond(),
            Amount = (long)request.Amount,
            Description = $"Purchase premium plan {request.PremiumPlan.Name}",
            Items = [request.PremiumPlan],
            BankCode = "",
            UserPhone = request.PremiumPlan.UserPhone,
            UserEmail = request.PremiumPlan.UserEmail,
            
        };

        var response = await _zaloPayService.CreateOrderUrl(orderCreationSettings);
        if (response.ReturnCode != 1)
        {
            return (new CreatePremiumPlanPaymentResponse()
            {
                Message = response.ReturnMessage,
                Code = (int)response.ReturnCode!,
                Success = false
            });
        }

        return (new CreatePremiumPlanPaymentResponse()
        {
            Message = "Create premium plan payment successfully",
            PaymentUrl = response.OrderUrl,
            ZaloPayToken = response.ZpTransToken,
            Code = (int)response.ReturnCode!,
            Success = true
        });
    }
}