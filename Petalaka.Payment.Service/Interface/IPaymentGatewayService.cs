using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.QueryOptions.FilterOptions.PaymentGatewayFilters;
using Petalaka.Payment.Service.QueryOptions.RequestOptions;
using Petalaka.Payment.Service.QueryOptions.SortOptions.PaymentGatewaySorts;

namespace Petalaka.Payment.Service.Interface;

public interface IPaymentGatewayService
{
    Task<PaginationResponse<PaymentGatewayBusinessModel>>
        GetPaymentGateways(
            RequestOptionsBase<GetAllPaymentGatewayFilterOptions, GetAllPaymentGatewaySortOptions> request);
    Task<PaymentGatewayBusinessModel> CreatePaymentGateway(PaymentGatewayBusinessModel paymentGatewayBusinessModel);
}