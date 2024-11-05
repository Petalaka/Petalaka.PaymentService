namespace Petalaka.Payment.Service.BusinessModels;

public class BaseBusinessModel
{
    public string? CreateBy { get; set; }
    public string? LastUpdateBy { get; set; }
    public string? DeleteBy { get; set; }
    public DateTimeOffset CreateTime { get; set; }
    public DateTimeOffset LastUpdateTime { get; set; }
    public DateTimeOffset? DeleteTime { get; set; }
}