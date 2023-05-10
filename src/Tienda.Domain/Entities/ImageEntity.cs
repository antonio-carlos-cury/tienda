using Tienda.Domain.Core;

namespace Tienda.Domain.Entities
{
    public partial class ImageEntity : EntityCore
    {
        public uint Key { get; set; }
        public string Base64Value { get; set; }
        public string Source { get; set; }

    }
    public partial class ImageFilter : EntityFilter
    {
        public List<uint> Keys { get; set; }
        public string Source { get; set; }
        public Guid PersonId { get; set; }
    }
}
