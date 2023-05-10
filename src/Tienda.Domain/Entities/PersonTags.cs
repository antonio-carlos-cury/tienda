using Tienda.Domain.Core;

namespace Tienda.Domain.Entities
{
    public class PersonTags : EntityCore
    {
        public Guid PersonId { get; set; }
        public Guid TagId { get; set; }

        public virtual PersonEntity Person { get; set; }

        public virtual TagsEntity Tag { get; set; }
    }
}
