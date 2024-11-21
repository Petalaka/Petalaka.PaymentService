using Petalaka.Payment.Repository.Enums.OrderEnums;
using Petalaka.Payment.Repository.Enums.PaymentEnums;

namespace Petalaka.Payment.API.ModelViews.ResponseModels.OrderResponse;

public class GetOrdersResponse
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    public string OrderDescription { get; set; }
    public long OrderExpiry { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal OrderAmount { get; set; }
    public decimal TotalPrice { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTimeOffset? PaymentDate { get; set; }
    public DateTimeOffset? CancelDate { get; set; }
    public DateTimeOffset CreateTime { get; set; }

    // Navigation Properties
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
}