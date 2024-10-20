using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Repository.Base.Interface
{
    public interface IBaseUnitOfWork
    {
        Task SaveChangesAsync();
        void SaveChanges();
        void Dispose();
    }
}
