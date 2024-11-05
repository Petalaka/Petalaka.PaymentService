using Petalaka.Payment.Repository.Base;

namespace Petalaka.Payment.Repository.Entities;

public class CartItemAdditionalDetail : BaseEntity
{
    public DateTime? PremiumPlanEndDate { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
}