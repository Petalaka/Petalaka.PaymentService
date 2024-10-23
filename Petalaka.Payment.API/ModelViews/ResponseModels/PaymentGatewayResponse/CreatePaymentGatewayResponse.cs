namespace Petalaka.Payment.API.ModelViews.ResponseModels.PaymentGatewayResponse;

public class CreatePaymentGatewayResponse
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? ImageIcon { get; set; }
    public string? RegionCode { get; set; }
}