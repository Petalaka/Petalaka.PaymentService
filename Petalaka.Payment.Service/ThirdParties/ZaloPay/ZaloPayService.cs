using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;

namespace Petalaka.Payment.Service.ThirdParties.ZaloPay;

public class ZaloPayService
{
    private readonly ZaloPaySettings _zaloPaySettings;

    public ZaloPayService(ZaloPaySettings zaloPaySettings)
    {
        _zaloPaySettings = zaloPaySettings;
    }
    
    public string CreateOrderUrl<TItem>(OrderCreationSettings<TItem> orderCreationSettings)
    {
        
        return "Order created";
    }
}