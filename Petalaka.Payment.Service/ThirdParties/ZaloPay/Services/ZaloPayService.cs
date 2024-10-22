using Newtonsoft.Json;
using Petalaka.Payment.Core.Utils;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Utils;

namespace Petalaka.Payment.Service.ThirdParties.ZaloPay;

public class ZaloPayService : IZaloPayService
{
    private readonly ZaloPaySettings _zaloPaySettings;
    private const string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";
    public ZaloPayService(ZaloPaySettings zaloPaySettings)
    {
        _zaloPaySettings = zaloPaySettings;
    }
    
    public async Task<Dictionary<string, object>> CreateOrderUrl<TItem>(OrderCreationSettings<TItem> orderCreationSettings)
    {
        var param = new Dictionary<string, string>();
        var embeddata = JsonConvert.SerializeObject(orderCreationSettings.Items);
        
        var ZaloPayHmac = ZaloPayHmacHelper.HmacHelper.Compute(ZaloPayHmacHelper.ZaloPayHMAC.HMACSHA256, _zaloPaySettings.Key1, embeddata);
        param.Add("appid", _zaloPaySettings.AppId);
        param.Add("appuser", orderCreationSettings.AppUser);
        param.Add("apptime", orderCreationSettings.AppTime.ToString());
        param.Add("amount", orderCreationSettings.Amount.ToString());
        param.Add("apptransid", $"{CoreHelper.SystemTimeNow:yyMMdd}_{orderCreationSettings.ApptransId}");
        param.Add("embeddata", embeddata);
        param.Add("item", JsonConvert.SerializeObject(orderCreationSettings.Items));
        param.Add("description", orderCreationSettings.Description);
        param.Add("bankcode", orderCreationSettings.BankCode);

        var macData = _zaloPaySettings.AppId + "|" + param["apptransid"] + "|" + param["appuser"] + "|" +
                      param["amount"] + "|" + param["apptime"] + "|"
                      + param["embeddata"] + "|" + param["item"];
        param.Add("mac", ZaloPayHmacHelper.HmacHelper.Compute(ZaloPayHmacHelper.ZaloPayHMAC.HMACSHA256, _zaloPaySettings.Key1, macData));

        var result = await HttpHelper.PostFormAsync(createOrderUrl, param);
        return result;
    }
}