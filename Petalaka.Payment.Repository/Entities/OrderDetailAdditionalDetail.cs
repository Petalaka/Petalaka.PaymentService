using Petalaka.Payment.Repository.Base;

namespace Petalaka.Payment.Repository.Entities;

public class OrderDetailAdditionalDetail : BaseEntity
{
    public DateTimeOffset? PremiumPlanStartDate { get; set; }
    public DateTimeOffset? PremiumPlanEndDate { get; set; }
    public DateTimeOffset? CheckInDate { get; set; }
    public DateTimeOffset? CheckOutDate { get; set; }
    
    // Navigation Properties
    public Guid OrderDetailId { get; set; }
    public virtual OrderDetail OrderDetail { get; set; }
}