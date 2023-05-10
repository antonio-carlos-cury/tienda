using Tienda.Domain.Core;

namespace Tienda.Domain.Entities
{
    public partial class PersonEntity : EntityCore
    {
        public string Name { get; set; } = string.Empty;

        public ushort SinceYear { get; set; }

        public string Country { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;

        public List<PersonVideosEntity>? Videos { get; set; }

        public List<PersonImagesEntity>? Images { get; set; }

        public List<PersonTags> Tags { get; set; }

    }

    public partial class PersonFilter : EntityFilter
    {
        public string Name { get; set; } = string.Empty;
        public ushort SinceYearMin { get; set; }
        public ushort SinceYearMax { get; set; }
        public bool OnlyHasVideos { get; set; }
        public bool OnlyHasImages { get; set; }
        public string TagName { get; set; } = string.Empty;

        public static PersonFilter Default => new() { PageIndex = 1, PageSize = 20, SinceYearMin = Convert.ToUInt16(DateTime.Now.Year-80), SinceYearMax = Convert.ToUInt16(DateTime.Now.Year - 18) };

    }

    #region Interfaces

    public interface IPersonFilter : IEntityFilter
    {
        public string Name { get; set; }
        public ushort SinceYearMin { get; set; }
        public ushort SinceYearMax { get; set; }
        public bool OnlyHasVideos { get; set; }
        public bool OnlyHasImages { get; set; }
    }


    public interface IPersonEntity : IEntityCore
    {
        public string Name { get; set; }

        public ushort SinceYear { get; set; }

        public string Country { get; set; }
    }
    #endregion
}
