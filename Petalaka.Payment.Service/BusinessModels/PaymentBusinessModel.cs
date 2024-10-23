namespace Petalaka.Payment.Service.BusinessModels;

public class PaymentBusinessModel<TItem>
{
    public string? UserId { get; set; }
    public string? TransactionId { get; set; }
    public long Amount { get; set; }
    public IList<TItem>? Items { get; set; }
    public string? Description { get; set; }
    public string? UserPhone { get; set; }
    public string? UserEmail { get; set; }
    public string? UserAddress { get; set; }
}