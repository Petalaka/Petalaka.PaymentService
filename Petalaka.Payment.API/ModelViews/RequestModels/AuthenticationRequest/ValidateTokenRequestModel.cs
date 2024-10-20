namespace Petalaka.Payment.API.ModelViews.RequestModels;

public class ValidateTokenRequestModel
{
    public string UserId { get; set; }
    public string DevideId { get; set; }
    public string TokenHashed { get; set; }
}