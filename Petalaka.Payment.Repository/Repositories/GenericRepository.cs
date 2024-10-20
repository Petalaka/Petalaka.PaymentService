using Microsoft.EntityFrameworkCore;
using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Base.Interface;
using Petalaka.Payment.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Petalaka.Payment.Core.Utils;

namespace Petalaka.Payment.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
    {
        private readonly PetalakaDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(PetalakaDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .Where(p => p.DeletedTime == null)
                .FirstOrDefaultAsync(predicate);
        }
        public async Task<T> FindAsyncDeleted(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .Where(p => p.DeletedTime != null)
                .FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> AsQueryable() => _dbSet.AsQueryable();

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate)
                .ToListAsync();

        public async Task<IEnumerable<T>> FindAllPredicate(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate)
                .ToListAsync();

        public void Insert(T entity) => _dbSet.Add(entity);
        public async Task InsertAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task InsertRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);
        public void SaveChanges() => _dbContext.SaveChanges();
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        public void Update(T entity) => _dbContext.Entry(entity).State = EntityState.Modified;
        public void UpdateRange(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            entity.DeletedTime = CoreHelper.SystemTimeNow;
        }
        public void DeletePermanent(T entity) => _dbSet.Remove(entity);

        public async Task<PaginationResponse<T>> GetPagination(IQueryable<T> queryable, int pageIndex, int pageSize)
        {
            int totalRecords = await queryable.CountAsync();
            queryable = queryable.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .AsQueryable();
            var data = await queryable.ToListAsync();
            var currentPageRecords = await queryable.CountAsync();
            return new PaginationResponse<T>(data, pageIndex, pageSize, totalRecords, currentPageRecords);
        }
    }
}
