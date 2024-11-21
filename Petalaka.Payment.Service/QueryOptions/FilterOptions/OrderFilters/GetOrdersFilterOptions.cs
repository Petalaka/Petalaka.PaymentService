using Petalaka.Payment.Repository.Enums.OrderEnums;
using Petalaka.Payment.Repository.Enums.PaymentEnums;

namespace Petalaka.Payment.Service.QueryOptions.FilterOptions.OrderFilters;

public class GetOrdersFilterOptions
{
    public string? OrderCode { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
}