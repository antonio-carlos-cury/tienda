using Tienda.Domain.Entities;

namespace Tienda.Domain.Interfaces
{
    public interface IImageService
    {
        Task<(ImageFilter, IEnumerable<ImageEntity>)> GetCachedEntitiesAsync(ImageFilter filter);
        Task<bool> InsertMediaToCache(List<ImageEntity> entity);
        Task<(ImageFilter, IEnumerable<ImageEntity>)> GetImages(ImageFilter filter);
    }
}
