using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Enums.OrderEnums;
using Petalaka.Payment.Repository.Enums.PaymentEnums;

namespace Petalaka.Payment.Repository.Entities;

public class Order : BaseEntity
{
    public string OrderCode { get; set; }
    public string OrderDescription { get; set; }
    public long OrderExpiry { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public decimal OrderAmount { get; set; }
    public decimal TotalPrice { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.pending;
    public DateTimeOffset? PaymentDate { get; set; }
    public DateTimeOffset? CancelDate { get; set; }
    
    // Navigation Properties
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    
    public Guid PaymentGatewayId { get; set; }
    public virtual PaymentGateway? PaymentGateway { get; set; }
    
    public virtual ICollection<OrderDetail>? OrderDetail { get; set; }
}