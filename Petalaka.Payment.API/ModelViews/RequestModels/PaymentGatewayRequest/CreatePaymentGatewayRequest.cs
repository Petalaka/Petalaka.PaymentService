using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Petalaka.Payment.API.ModelViews.RequestModels.PaymentGatewayRequest;

public class CreatePaymentGatewayRequest
{
    [FromForm(Name = "name")]
    [Required]
    [RegularExpression(@"^[\p{L}[\s]+$", ErrorMessage = "Full name should only contain letters and spaces, no special characters or numbers")]
    public string Name { get; set; }
    [FromForm(Name = "regionCode")]
    public string RegionCode { get; set; }
    [FromForm(Name = "imageIcon")]
    public IFormFile? ImageFile { get; set; }
}