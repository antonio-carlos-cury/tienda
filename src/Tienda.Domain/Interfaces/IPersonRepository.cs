using Tienda.Domain.Entities;

namespace Tienda.Domain.Interfaces
{
    public interface IPersonRepository : ICoreRepository<PersonFilter, PersonEntity>, IAggregateRoot
    {
    }

    public interface IPersonImagesRepository : ICoreRepository<PersonImagesFilter, PersonImagesEntity>
    {
    }

    public interface IPersonVideosRepository : ICoreRepository<PersonVideosFilter, PersonVideosEntity>
    {
    }

}
