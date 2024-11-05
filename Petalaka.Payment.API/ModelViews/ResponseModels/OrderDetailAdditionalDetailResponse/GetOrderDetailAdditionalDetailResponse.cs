namespace Petalaka.Payment.API.ModelViews.ResponseModels.OrderDetailAdditionalDetailResponse;

public class GetOrderDetailAdditionalDetailResponse
{
    public Guid Id { get; set; }
    public DateTimeOffset PremiumPlanStartDate { get; set; }
    public DateTimeOffset PremiumPlanEndDate { get; set; }
    public DateTimeOffset CheckInDate { get; set; }
    public DateTimeOffset CheckOutDate { get; set; }
}