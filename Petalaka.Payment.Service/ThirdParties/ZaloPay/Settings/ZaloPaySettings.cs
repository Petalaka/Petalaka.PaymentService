﻿namespace Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;

public class ZaloPaySettings
{
    public string AppId { get; set; }
    public string Key1 { get; set; }
    public string Key2 { get; set; }
    public string CallbackUrl { get; set; }

    public bool IsValid()
    {
        if (String.IsNullOrWhiteSpace(AppId))
        {
            throw new ArgumentNullException("AppId is required");
        }
        if (String.IsNullOrWhiteSpace(Key1))
        {
            throw new ArgumentNullException("Key1 is required");
        }
        if (String.IsNullOrWhiteSpace(Key2))
        {
            throw new ArgumentNullException("Key2 is required");
        }
        if (String.IsNullOrWhiteSpace(CallbackUrl))
        {
            throw new ArgumentNullException("CallbackUrl is required");
        }

        return true;
    }
}