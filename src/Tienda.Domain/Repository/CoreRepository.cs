using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tienda.Domain.Context;
using Tienda.Domain.Core;
using Tienda.Domain.Interfaces;

namespace Tienda.Domain.Repository
{
    
    public abstract class CoreRepository<TPagination, TEntity> : ICoreRepository<TPagination, TEntity> where TPagination : EntityFilter where TEntity : EntityCore
    {
        protected readonly TiendaDBContext Db;
        protected readonly DbSet<TEntity> DbSet;
        protected CoreRepository() { Db = new TiendaDBContext(); DbSet = Db.Set<TEntity>(); }
        protected CoreRepository(TiendaDBContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).CountAsync();
        }

        public async virtual Task<bool> InsertAsync(TEntity entity)
        {
            DbSet.Add(entity);
            return await SaveChangesAsync() > 0;
        }
        public async Task<(TPagination, IEnumerable<TEntity>)> SearchAsync(Expression<Func<TEntity, bool>> predicate, TPagination filters)
        {
            var total = await CountAsync(predicate);
            filters.Total = Convert.ToInt32(total);
            filters.PageCount = filters.Total > filters.PageSize ? Convert.ToInt32(Math.Ceiling((double)filters.Total / filters.PageSize)) : 1;

            if (filters.OrderByValue.IsNull())
            {
                var results = await DbSet.Where(predicate).Skip((filters.PageIndex - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
                return (filters, results);
            }
            else
            {
                var results = await PredicateBuilder.OrderBy(DbSet.Where(predicate), filters.OrderByValue).Skip((filters.PageIndex - 1) * filters.PageSize).Take(filters.PageSize).ToListAsync();
                return (filters, results);
            }
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            Db.ChangeTracker.Clear();
            DbSet.Update(entity);
            return await Db.SaveChangesAsync() > 0;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Db?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            DbSet.Remove(await GetByIdAsync(id));
            return await Db.SaveChangesAsync() > 0;
        }

        public (TPagination, IEnumerable<TEntity>) DefaultValues() {
            return (default, default);        
        }
    }
}
