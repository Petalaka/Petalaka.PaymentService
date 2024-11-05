using Petalaka.Payment.Repository.Base;

namespace Petalaka.Payment.Repository.Entities;

public class CartItem : BaseEntity
{
    public string ItemName { get; set; }
    public decimal ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public bool? IsInStock { get; set; } = true;
    // Navigation properties
    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; }
    
    public Guid CartItemAdditionalDetailId { get; set; }
    public virtual CartItemAdditionalDetail? CartItemAdditionalDetail { get; set; }
}