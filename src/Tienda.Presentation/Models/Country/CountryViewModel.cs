namespace Tienda.Presentation.Models
{
    public class CountryViewModel : EntityCoreModel
    {
        public string Name { get; set; } = string.Empty; //Ex: Brazil
        public string Acronym { get; set; } = string.Empty; // Ex: BR
        public string Language { get; set; } = string.Empty; // Ex: pt-br
        public string ImgFlag { get; set; } = string.Empty; //Base64 image of country flag.
    }

    public class CountryFilters : EntityCoreFilter { }
}
