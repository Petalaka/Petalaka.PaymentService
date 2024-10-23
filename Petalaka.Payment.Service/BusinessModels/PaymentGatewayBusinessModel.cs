using Microsoft.AspNetCore.Http;

namespace Petalaka.Payment.Service.BusinessModels;

public class PaymentGatewayBusinessModel : BaseBusinessModel
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? ImageIcon { get; set; }
    public string? RegionCode { get; set; }
    public bool? IsSuspended { get; set; } = false;
}