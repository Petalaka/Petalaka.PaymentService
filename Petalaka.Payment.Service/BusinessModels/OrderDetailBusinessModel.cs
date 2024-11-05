using Petalaka.Payment.Repository.Enums.ItemEnums;

namespace Petalaka.Payment.Service.BusinessModels;

public class OrderDetailBusinessModel : BaseBusinessModel
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    
    public Guid ItemId { get; set; }
    public decimal ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public ItemType ItemType { get; set; }
    // Navigation Properties
    
    public ICollection<OrderDetailAdditionalDetailBusinessModel> OrderDetailAdditionalDetails { get; set; }
}