using Petalaka.Payment.Repository.Enums.OrderEnums;
using Petalaka.Payment.Repository.Enums.PaymentEnums;
using Petalaka.Payment.Service.QueryOptions.ExtensionOptions;

namespace Petalaka.Payment.Service.QueryOptions.FilterOptions.OrderFilters;

public class GetOrderByUserFilterOptions
{
    public OrderStatus? OrderStatus { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
}