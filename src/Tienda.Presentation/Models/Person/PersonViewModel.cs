namespace Tienda.Presentation.Models
{

    public enum PersonType
    {
        All,
        Customer,
        Admin,
        Actor,
        User
    }

    public partial class PersonViewModel : EntityCoreModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<TagsViewModel> Tags { get; set; } = Array.Empty<TagsViewModel>();
        public PersonType PersonType { get; set;} = PersonType.Customer;
        public DateTime BirthDate { get; set; } = DateTime.UtcNow.AddYears(-18);
        public CountryViewModel BirthCountry { get; set; } = new();

    }

    public partial class PersonFilters : EntityCoreFilter
    {
        public IEnumerable<PersonType> PersonType { get; set; } = Array.Empty<PersonType>();
        public DateTime BirthDateMin { get; set; } = DateTime.UtcNow.AddYears(-18);
        public DateTime BirthDateMax { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAtMin { get; set; } = DateTime.UtcNow.AddDays(-18);
        public DateTime CreatedAtMax { get; set; } = DateTime.UtcNow;

        public static PersonFilters Default => new() { PageIndex = 1, PageSize = 75};
    }

}
