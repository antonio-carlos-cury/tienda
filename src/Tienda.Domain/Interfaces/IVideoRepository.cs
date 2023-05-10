using Tienda.Domain.Entities;

namespace Tienda.Domain.Interfaces
{
    public interface IVideoRepository : ICoreRepository<VideoFilter, VideoEntity>
    {
    }
}
