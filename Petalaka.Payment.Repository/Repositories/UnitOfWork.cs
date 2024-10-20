using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Base.Interface;
using Petalaka.Payment.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Repository.Repositories
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        private readonly PetalakaDbContext _dbContext;

        public UnitOfWork(PetalakaDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class, IBaseEntity
        {
            return new GenericRepository<T>(_dbContext);
        }
    }
}
