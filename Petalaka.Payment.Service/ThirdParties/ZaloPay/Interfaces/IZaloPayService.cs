using Petalaka.Payment.Service.ThirdParties.ZaloPay.Models;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;

namespace Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;

public interface IZaloPayService
{
    
    Task<ZaloPayResponseData> CreateOrderUrl<TItem>(OrderCreationSettings<TItem> orderCreationSettings);
    
}