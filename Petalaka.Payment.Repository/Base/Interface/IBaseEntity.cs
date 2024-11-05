using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Repository.Base.Interface
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        string? CreatedBy { get; set; }
        string? LastUpdatedBy { get; set; }
        string? DeletedBy { get; set; }
        DateTimeOffset CreatedTime { get; set; }
        DateTimeOffset LastUpdatedTime { get; set; }
        DateTimeOffset? DeletedTime { get; set; }
    }
}
