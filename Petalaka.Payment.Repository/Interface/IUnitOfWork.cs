using Petalaka.Payment.Repository.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Repository.Interface
{
    public interface IUnitOfWork : IDisposable, IBaseUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>() where T : class, IBaseEntity;
    }
}
