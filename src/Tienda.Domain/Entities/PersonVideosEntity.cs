using Tienda.Domain.Core;

namespace Tienda.Domain.Entities
{
    public class PersonVideosEntity : EntityCore
    {
        public Guid PersonId { get; set; }
        public Guid VideoEntityId { get; set; }
        public PersonEntity Person { get; set; }
        public VideoEntity Video { get; set; }

    }

    public partial class PersonVideosFilter : EntityFilter
    {
        public Guid PersonId { get; set; }
        public Guid ImageEntityId { get; set; }
    }


}
