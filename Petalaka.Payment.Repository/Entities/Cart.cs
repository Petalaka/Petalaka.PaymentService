using Petalaka.Payment.Repository.Base;

namespace Petalaka.Payment.Repository.Entities;

public class Cart : BaseEntity
{
    public decimal TotalPrice { get; set; }

    // Navigation properties
    public Guid UserId { get; set; }
    public virtual ICollection<CartItem>? CartItems { get; set; }
}