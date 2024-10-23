using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Petalaka.Payment.Repository.Base;

namespace Petalaka.Payment.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
T Find(Expression<Func<T, bool>> predicate);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindAsyncDeleted(Expression<Func<T, bool>> predicate);
        IQueryable<T> AsQueryable();
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllPredicate(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        void SaveChanges();
        Task SaveChangesAsync();
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeletePermanent(T entity);
        Task<PaginationResponse<T>> GetPagination(IQueryable<T> queryable, int pageIndex, int pageSize);
    }
}
