using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.BusinessModels.ContextModels;
using Petalaka.Payment.Service.QueryOptions.FilterOptions.OrderFilters;
using Petalaka.Payment.Service.QueryOptions.RequestOptions;
using Petalaka.Payment.Service.QueryOptions.SortOptions.OrderSorts;

namespace Petalaka.Payment.Service.Interface;

public interface IOrderService
{
    Task<PaginationResponse<OrderBusinessModel>>
        GetOrderByUser(RequestOptionsBase<GetOrderByUserFilterOptions, GetOrderByUserSortOptions> request,
            UserContext userContext);

    Task<OrderBusinessModel> GetOrderDetail(OrderBusinessModel request);

    Task<PaginationResponse<OrderBusinessModel>>
        GetOrders(RequestOptionsBase<GetOrdersFilterOptions, GetOrdersSortOptions> request);
}