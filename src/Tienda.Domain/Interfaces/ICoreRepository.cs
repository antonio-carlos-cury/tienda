using System.Linq.Expressions;
using Tienda.Domain.Core;

namespace Tienda.Domain.Interfaces
{
    public interface ICoreRepository<TPagination, TEntity> where TEntity : EntityCore where TPagination : EntityFilter
    {
        Task<(TPagination, IEnumerable<TEntity>)> SearchAsync(Expression<Func<TEntity, bool>> predicate, TPagination filters);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<bool> InsertAsync(TEntity entity);
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(TEntity entity);

        (TPagination, IEnumerable<TEntity>) DefaultValues();
    }
}
