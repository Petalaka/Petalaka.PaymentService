using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Payment.API.Base;
using Petalaka.Payment.API.ModelViews.RequestModels.PaymentGatewayRequest;
using Petalaka.Payment.API.ModelViews.ResponseModels.PaymentGatewayResponse;
using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.QueryOptions.FilterOptions.PaymentGatewayFilters;
using Petalaka.Payment.Service.QueryOptions.RequestOptions;
using Petalaka.Payment.Service.QueryOptions.SortOptions.PaymentGatewaySorts;

namespace Petalaka.Payment.API.Controllers;

public class PaymentGatewaysController : BaseController
{
    private readonly IPaymentGatewayService _paymentGatewayService;
    private readonly IMapper _mapper;
    public PaymentGatewaysController(IPaymentGatewayService paymentGatewayService, IMapper mapper)
    {
        _paymentGatewayService = paymentGatewayService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get all payment gateways
    /// </summary>
    /// <remarks>
    /// SortOptions:
    /// - NameAlphabeticalAz (A -> Z) = 1,
    /// - NameAlphabeticalZa (Z -> A) = 2,
    /// - CreatedTimeAscending = 3,
    /// - CreatedTimeDescending = 4
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("v1/payment-gateways")]
    public async Task<ActionResult<BaseResponsePagination<GetPaymentGatewayResponse>>> 
        GetPaymentGateways(RequestOptionsBase<GetAllPaymentGatewayFilterOptions, GetAllPaymentGatewaySortOptions> request)
    {
        var paymentGateways = await _paymentGatewayService.GetPaymentGateways(request);
        
        var basePaginationResponse = _mapper.Map<BaseResponsePagination<GetPaymentGatewayResponse>>(paymentGateways);
        basePaginationResponse.StatusCode = StatusCodes.Status200OK;
        basePaginationResponse.Message = "Get all payment gateways successfully";
        
        return Ok(paymentGateways);
    }
    
    /// <summary>
    /// Create new payment gateway
    /// </summary>
    /// <remarks>
    /// Require authentication as admin role
    ///
    /// - Name (REQUIRED): Full name should only contain letters and spaces, no special characters or numbers
    /// - RegionCode (REQUIRED): No validation
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("v1/payment-gateway")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<BaseResponse<CreatePaymentGatewayResponse>>> CreatePaymentGateway([FromForm] CreatePaymentGatewayRequest request)
    {
        var model = _mapper.Map<PaymentGatewayBusinessModel>(request);
        var result = await _paymentGatewayService.CreatePaymentGateway(model);
        var response = _mapper.Map<CreatePaymentGatewayResponse>(result);
        return Created(String.Empty, new BaseResponse(StatusCodes.Status201Created, "Create payment gateway successfully", response));
    }
}