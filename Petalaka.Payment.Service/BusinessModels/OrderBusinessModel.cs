using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Repository.Enums.OrderEnums;
using Petalaka.Payment.Repository.Enums.PaymentEnums;

namespace Petalaka.Payment.Service.BusinessModels;

public class OrderBusinessModel : BaseBusinessModel
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
    
    // Navigation Properties
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }
    
    public Guid PaymentGatewayId { get; set; }
    public PaymentGatewayBusinessModel PaymentGateway { get; set; }
    
    public ICollection<OrderDetailBusinessModel> OrderDetail { get; set; }
}