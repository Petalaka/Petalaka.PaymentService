namespace Petalaka.Payment.Service.BusinessModels;

public class OrderDetailAdditionalDetailBusinessModel : BaseBusinessModel
{
    public Guid Id { get; set; }
    public DateTimeOffset PremiumPlanStartDate { get; set; }
    public DateTimeOffset PremiumPlanEndDate { get; set; }
    public DateTimeOffset CheckInDate { get; set; }
    public DateTimeOffset CheckoutDate { get; set; }
}