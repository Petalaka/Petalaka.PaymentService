using Microsoft.AspNetCore.Mvc;
using Petalaka.Payment.API.Base;
using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.Services;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;
using Petalaka.PetStore.Service.Services;

namespace Petalaka.Payment.API.Controllers;

public class PaymentsController : BaseController
{
    private readonly IZaloPayService _zaloPayService;
    private readonly IPaymentService _paymentService;
    private readonly PaymentManager.PaymentManagerBase _paymentManagerBase;
    public PaymentsController(IZaloPayService zaloPayService,
        IPaymentService paymentService,
        PaymentManager.PaymentManagerBase paymentManagerBase)
    {
        _zaloPayService = zaloPayService;
        _paymentService = paymentService;
        _paymentManagerBase = paymentManagerBase;
    }
    
    /*
    [HttpPost]
    [Route("v1/payments/zalopay/create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] PaymentBusinessModel<Service.Services.PaymentService.Item> orderCreationSettings)
    {
        var result = await _paymentService.CreateOrder(orderCreationSettings);
        return Ok(result);
    }*/

}