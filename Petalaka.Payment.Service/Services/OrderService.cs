using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Petalaka.Payment.Core.CustomException;
using Petalaka.Payment.Core.Utils;
using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Repository.Enums.ItemEnums;
using Petalaka.Payment.Repository.Interface;
using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.BusinessModels.ContextModels;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.QueryOptions.FilterOptions.OrderFilters;
using Petalaka.Payment.Service.QueryOptions.RequestOptions;
using Petalaka.Payment.Service.QueryOptions.SortOptions.OrderSorts;
using Petalaka.PetStore.Service.Services;

namespace Petalaka.Payment.Service.Services;

public class OrderService : OrderManager.OrderManagerBase, IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public override async Task<CreatePremiumOrderResponseGrpc> CreatePremiumPlanOrder(
        CreatePremiumOrderRequestGrpc request, ServerCallContext context)
    {
        try
        {
            Order order = new Order()
            {
                OrderCode = request.OrderCode,
                OrderDescription = request.OrderDescription,
                OrderAmount = Convert.ToDecimal(request.OrderAmount),
                BranchId = new Guid(request.BranchId),
                UserId = new Guid(request.UserId),
                PaymentGatewayId = new Guid(request.PaymentGatewayId),
                TotalPrice = Convert.ToDecimal(request.OrderAmount),
            };
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);

            foreach (var orderDetailRequestGrpc in request.OrderDetail)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.Id,
                    OrderCode = request.OrderCode,
                    ItemType = (ItemType)orderDetailRequestGrpc.ItemType,
                    ItemId = new Guid(orderDetailRequestGrpc.ItemId),
                    ItemPrice = Convert.ToDecimal(orderDetailRequestGrpc.ItemPrice),
                    ItemQuantity = orderDetailRequestGrpc.ItemQuantity,
                };
                await _unitOfWork.GetRepository<OrderDetail>().InsertAsync(orderDetail);

                OrderDetailAdditionalDetail orderDetailAdditionalDetail = new OrderDetailAdditionalDetail()
                {
                    OrderDetailId = orderDetail.Id,
                    PremiumPlanStartDate = CoreHelper.SystemTimeNow,
                    PremiumPlanEndDate = CoreHelper.SystemTimeNow.AddMonths(orderDetailRequestGrpc.ItemQuantity),
                };
                await _unitOfWork.GetRepository<OrderDetailAdditionalDetail>().InsertAsync(orderDetailAdditionalDetail);
            }

            await _unitOfWork.SaveChangesAsync();

            return new CreatePremiumOrderResponseGrpc()
            {
                Message = "Create premium plan order successfully",
                IsSuccess = true,
                Code = 0,
            };
        }
        catch (RpcException e)
        {
            return new CreatePremiumOrderResponseGrpc()
            {
                Message = "Create premium plan order failed",
                IsSuccess = false,
                Code = (int)e.Status.StatusCode,
            };
        }
    }


    /*
    public async Task<OrderBusinessModel> GetOrders()
*/
    public async Task<PaginationResponse<OrderBusinessModel>>
        GetOrderByUser(RequestOptionsBase<GetOrderByUserFilterOptions, GetOrderByUserSortOptions> request,
            UserContext userContext)
    {
        var orderQuery = _unitOfWork.GetRepository<Order>().AsQueryable();

        orderQuery = orderQuery.Where(p =>
            p.UserId.ToString() == StringConverterHelper.CapitalizeString(userContext.UserId));

        if (request.FilterOptions != null)
        {
            if (request.FilterOptions.OrderStatus != null)
            {
                orderQuery = orderQuery.Where(p => p.OrderStatus == request.FilterOptions.OrderStatus);
            }

            if (request.FilterOptions.PaymentStatus != null)
            {
                orderQuery = orderQuery.Where(p => p.PaymentStatus == request.FilterOptions.PaymentStatus);
            }
        }

        switch (request.SortOptions)
        {
            case GetOrderByUserSortOptions.TotalPriceAscending:
                orderQuery = orderQuery.OrderBy(p => p.TotalPrice);
                break;
            case GetOrderByUserSortOptions.TotalPriceDescending:
                orderQuery = orderQuery.OrderByDescending(p => p.TotalPrice);
                break;
            default:
                orderQuery = orderQuery.OrderByDescending(p => p.CreatedTime);
                break;
        }

        var queryPaging = await _unitOfWork.GetRepository<Order>()
            .GetPagination(orderQuery, request.PageNumber, request.PageSize);

        var order = _mapper.Map<IList<OrderBusinessModel>>(queryPaging.Data);

        return new PaginationResponse<OrderBusinessModel>
        (order, queryPaging.PageNumber, queryPaging.PageSize, queryPaging.TotalRecords,
            queryPaging.CurrentPageRecords);
    }

    public async Task<OrderBusinessModel> GetOrderDetail(OrderBusinessModel request)
    {
        var order = await _unitOfWork.GetRepository<Order>().FindAsync(p => p.OrderCode == request.OrderCode);
        if (order == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Order not found");
        }
        return _mapper.Map<OrderBusinessModel>(order);
    }
}