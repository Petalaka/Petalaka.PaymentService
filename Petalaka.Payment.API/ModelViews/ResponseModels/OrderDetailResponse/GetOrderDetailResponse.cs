using Petalaka.Payment.API.ModelViews.ResponseModels.OrderDetailAdditionalDetailResponse;
using Petalaka.Payment.Repository.Enums.ItemEnums;
using Petalaka.Payment.Service.BusinessModels;

namespace Petalaka.Payment.API.ModelViews.ResponseModels.OrderDetailResponse;

public class GetOrderDetailResponse
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    
    public Guid ItemId { get; set; }
    public decimal ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public ItemType ItemType { get; set; }
    // Navigation Properties
    
    public IList<GetOrderDetailAdditionalDetailResponse> OrderDetailAdditionalDetails { get; set; }
}