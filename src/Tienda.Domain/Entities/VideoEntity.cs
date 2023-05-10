using Tienda.Domain.Core;

namespace Tienda.Domain.Entities
{
    public class VideoEntity : EntityCore
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<VideoImagesEntity>? VideoImages { get; set; }
        public string? SourceUrl { get; set; }
        public string? LocalFilePath { get; set; }
        public byte[]? Contents { get; set; }
        public ushort Height { get; set; }
        public ushort Width { get; set; }
        public long SizeBytes { get; set; }
        public ushort Duration { get; set; }
    }


    public class VideoFilter : EntityFilter
    {
        public string FileName { get; set; } = string.Empty;
        public static VideoFilter Default => new();
    }
}