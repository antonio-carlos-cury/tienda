using Tienda.Domain.Entities;

namespace Tienda.Domain.Interfaces
{
    public interface IVideoService
    {
        Task<bool> InsertAsync(VideoEntity entity);
        Task<(VideoFilter, IEnumerable<VideoEntity>)> DeleteAsync(string entityId);
        Task<(VideoFilter, IEnumerable<VideoEntity>)> SearchAsync(VideoFilter filter);
    }
}
