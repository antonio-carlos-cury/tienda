using Tienda.Domain.Entities;

namespace Tienda.Domain.Interfaces
{
    public interface IPersonService
    {
        Task<(PersonFilter, IEnumerable<PersonEntity>)> SearchAsync(PersonFilter personPagination);
        Task<bool> InsertAsync(PersonEntity entity);
        Task<(PersonFilter, IEnumerable<PersonEntity>)> UpdateAsync(PersonEntity entity);
    }
}
