using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;

namespace Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;

public interface IZaloPayService
{
    Task<Dictionary<string, object>> CreateOrderUrl<TItem>(OrderCreationSettings<TItem> orderCreationSettings);
}