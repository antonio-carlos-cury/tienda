using Tienda.Domain.Core;

namespace Tienda.Domain.Entities
{
    public class PersonImagesEntity : EntityCore
    {
        public Guid PersonId { get; set; }
        public Guid ImageEntityId { get; set; }
        public PersonEntity Person { get; set; }
        public ImageEntity Image { get; set; }
    }

    public partial class PersonImagesFilter : EntityFilter
    {
        public Guid PersonId { get; set; }
        public Guid ImageEntityId { get; set; }
    }
}
