using Amazon.Runtime.Internal.Transform;
using Newtonsoft.Json;
using Petalaka.Payment.Core.Utils;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Models;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Utils;

namespace Petalaka.Payment.Service.ThirdParties.ZaloPay.Services;

public class ZaloPayService : IZaloPayService
{
    private readonly ZaloPaySettings _zaloPaySettings;
    private const string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";
    public ZaloPayService(ZaloPaySettings zaloPaySettings)
    {
        _zaloPaySettings = zaloPaySettings;
    }
    
    public async Task<ZaloPayResponseData> CreateOrderUrl<TItem>(OrderCreationSettings<TItem> orderCreationSettings)
    {
        var param = new Dictionary<string, string>();
        /*
        var embeddata = JsonConvert.SerializeObject(orderCreationSettings.Items);
        */
        var embeddata = JsonConvert.SerializeObject(new
        {
            redirecturl = "https://petalaka.nodfeather.win"
        });
        var appTransId = $"{CoreHelper.SystemTimeNow:yyMMdd}_{orderCreationSettings.ApptransId}";
        param.Add("appid", _zaloPaySettings.AppId);
        param.Add("appuser", orderCreationSettings.AppUser);
        param.Add("apptime", orderCreationSettings.AppTime.ToString());
        param.Add("amount", orderCreationSettings.Amount.ToString());
        param.Add("apptransid", appTransId);
        param.Add("embeddata", embeddata);
        param.Add("item", JsonConvert.SerializeObject(orderCreationSettings.Items));
        param.Add("description", orderCreationSettings.Description);
        param.Add("bankcode", orderCreationSettings.BankCode);
        if (orderCreationSettings.UserPhone != null) param.Add("phone", orderCreationSettings.UserPhone);
        if (orderCreationSettings.UserEmail != null) param.Add("email", orderCreationSettings.UserEmail);

        var macData = _zaloPaySettings.AppId + "|" + param["apptransid"] + "|" + param["appuser"] + "|" +
                      param["amount"] + "|" + param["apptime"] + "|"
                      + param["embeddata"] + "|" + param["item"];
        param.Add("mac", ZaloPayHmacHelper.HmacHelper.Compute(ZaloPayHmacHelper.ZaloPayHMAC.HMACSHA256, _zaloPaySettings.Key1, macData));

        var result = await HttpHelper.PostFormAsync(createOrderUrl, param);
        
        return new ZaloPayResponseData()
        {
            OrderCode = appTransId,
            SubReturnCode = result.GetValueOrDefault("subreturncode", -1),
            SubReturnMessage = result.GetValueOrDefault("subreturnmessage", ""),
            OrderUrl = result.GetValueOrDefault("orderurl", ""),
            ZpTransToken = result.GetValueOrDefault("zptranstoken", ""),
            OrderToken = result.GetValueOrDefault("ordertoken", ""),
            QrCode = result.GetValueOrDefault("qrCode", ""),
            ReturnMessage = result.GetValueOrDefault("returnmessage", ""),
            ReturnCode = result.GetValueOrDefault("returncode", -1)
        };
    }
}