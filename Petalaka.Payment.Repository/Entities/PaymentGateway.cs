using System.ComponentModel.DataAnnotations;
using Petalaka.Payment.Core.Utils;
using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Base.Interface;

namespace Petalaka.Payment.Repository.Entities;

public class PaymentGateway : BaseEntity
{
    public string Name { get; set; }
    public string? ImageIcon { get; set; }
    public string? RegionCode { get; set;}
    public bool? IsSuspended { get; set; } = false;
    
    // Navigation Properties
    public virtual ICollection<Order>? Orders { get; set; }
}