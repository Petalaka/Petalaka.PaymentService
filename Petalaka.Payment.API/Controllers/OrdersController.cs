using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petalaka.Payment.API.Base;
using Petalaka.Payment.API.ModelViews.ResponseModels.OrderResponse;
using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.BusinessModels.ContextModels;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.QueryOptions.FilterOptions.OrderFilters;
using Petalaka.Payment.Service.QueryOptions.RequestOptions;
using Petalaka.Payment.Service.QueryOptions.SortOptions.OrderSorts;

namespace Petalaka.Payment.API.Controllers;

public class OrdersController : BaseController
{
    private IOrderService _orderService;
    private IMapper _mapper;
    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get Order of user when user login
    /// </summary>
    /// <remarks>
    /// Require authentication to perform this function
    /// 
    /// OrderStatus:
    /// - Pending = 1,
    /// - OverDue = 2,
    /// - Completed = 3,
    /// - Cancelled = 4
    ///
    /// SortOptions:
    /// - TotalPriceAscending = 1,
    /// - TotalPriceDescending = 2,
    /// </remarks>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("v1/user-orders")]
    [Authorize]
    public async Task<ActionResult<BaseResponsePagination<GetUserOrderResponse>>> GetUserOrders(
        RequestOptionsBase<GetOrderByUserFilterOptions, GetOrderByUserSortOptions> request)
    {
        var userContext = new UserContext()
        {
            UserId = User.Claims.FirstOrDefault(p => p.Type == "UserId").Value,
        };
        var paginationResponse = await _orderService.GetOrderByUser(request, userContext);
        
        var baseResponsePagination = _mapper.Map<BaseResponsePagination<GetUserOrderResponse>>(paginationResponse);
        baseResponsePagination.StatusCode = StatusCodes.Status200OK;
        baseResponsePagination.Message = "Get User Orders successfully";
        
        return Ok(baseResponsePagination);
    }
    
    [HttpGet]
    [Route("v1/order-detail")]
    [Authorize]
    public async Task<ActionResult<BaseResponse<GetOrderWithDetailResponse>>>
        GetOrderDetail([FromQuery] string orderCode)
    {
        var request = new OrderBusinessModel()
        {
            OrderCode = orderCode
        };
        var order = await _orderService.GetOrderDetail(request);
        var baseResponse = _mapper.Map<GetOrderWithDetailResponse>(order);
        
        return Ok(new BaseResponse(StatusCodes.Status200OK, "Get Order Detail successfully", baseResponse));
    }
}