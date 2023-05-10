using Tienda.Domain.Core;

namespace Tienda.Domain.Entities
{
    public class VideoImagesEntity : EntityCore
    {
        public Guid ImageEntityId { get; set; }
        public Guid VideoEntityId { get; set; }
        public ImageEntity Image { get; set; }
        public VideoEntity Video { get; set; }


    }
}
