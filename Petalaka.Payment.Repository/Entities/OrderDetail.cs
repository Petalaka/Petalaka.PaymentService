using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Enums.ItemEnums;

namespace Petalaka.Payment.Repository.Entities;

public class OrderDetail : BaseEntity
{
    public string OrderCode { get; set; }
    
    public Guid ItemId { get; set; }
    public decimal ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public ItemType ItemType { get; set; }
    // Navigation Properties

    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
    
    public virtual ICollection<OrderDetailAdditionalDetail>? OrderDetailAdditionalDetails { get; set; }
}