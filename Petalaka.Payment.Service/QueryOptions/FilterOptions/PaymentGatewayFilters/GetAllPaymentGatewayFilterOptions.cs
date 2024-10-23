using Petalaka.Payment.Service.QueryOptions.ExtensionOptions;

namespace Petalaka.Payment.Service.QueryOptions.FilterOptions.PaymentGatewayFilters;

public class GetAllPaymentGatewayFilterOptions
{
    public string? Name { get; set; }
    public string? RegionCode { get; set; }
    public bool? IsSuspended { get; set; }
    public TimeRange? CreatedTimeRange { get; set; }
}