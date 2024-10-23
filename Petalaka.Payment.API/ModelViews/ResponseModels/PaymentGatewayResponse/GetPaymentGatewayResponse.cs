namespace Petalaka.Payment.API.ModelViews.ResponseModels.PaymentGatewayResponse;

public class GetPaymentGatewayResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageIcon { get; set; }
    public string? RegionCode { get; set;}
    public bool? IsSuspended { get; set; } = false;
    
    public string? CreatedBy { get; set; }
    public string? LastUpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastUpdatedTime { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
}