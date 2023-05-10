namespace Tienda.Presentation.Extensions
{
    public interface IAppSettings
    {
        string DefaultApiUri { get; set; }
        string BaseVirtualPath { get; set; }
        string CaminhoWebDriver { get; set; }

    }

    public class AppSettings : IAppSettings
    {
        public string DefaultApiUri { get; set; }
        public string BaseVirtualPath { get; set; }
        public string CaminhoWebDriver { get; set; }
    }
}
