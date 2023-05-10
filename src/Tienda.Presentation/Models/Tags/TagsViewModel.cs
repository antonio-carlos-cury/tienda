namespace Tienda.Presentation.Models
{
    public class TagsViewModel : EntityCoreModel
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public uint Crc32 { get; set; } = 0;
    }

    public class TagsFilters : EntityCoreFilter
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

}
