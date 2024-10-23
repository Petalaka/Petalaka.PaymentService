using System.ComponentModel.DataAnnotations;
using Petalaka.Payment.Core.Utils;
using Petalaka.Payment.Repository.Base.Interface;

namespace Petalaka.Payment.Repository.Entities;

public class PaymentGateway : IBaseEntity
{
    [Key]
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

    public PaymentGateway()
    {
        CreatedTime = CoreHelper.SystemTimeNow;
        LastUpdatedTime = CreatedTime;
    }
}