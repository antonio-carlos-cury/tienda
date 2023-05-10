using Tienda.Domain.Core;

namespace Tienda.Domain.Entities
{
    public class TagsEntity : EntityCore
    {
        public string Name { get; set; }
        
        public string Slug { get; set; }

        public string Description { get; set; }
    }
}
