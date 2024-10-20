namespace Petalaka.Payment.Service.BusinessModels.ContextModels;

public class UserContext
{
    public string UserId { get; set; }
    public IList<string> UserRole { get; set; }
}