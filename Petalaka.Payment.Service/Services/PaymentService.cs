using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;

namespace Petalaka.Payment.Service.Services;

public class PaymentService : IPaymentService
{
    private readonly IZaloPayService _zaloPayService;
    public PaymentService(IZaloPayService zaloPayService)
    {
        _zaloPayService = zaloPayService;
    }

    public class Item
    {
        public int Id = 2;
        public string Name = "Item 1";
        public long Amount = 100000;
    }
    public async Task<Dictionary<string, object>> CreateOrder(PaymentBusinessModel<Item> paymentBusinessModel)
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
    }
}