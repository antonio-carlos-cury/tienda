using Tienda.BaseService;

namespace Tienda.WebApi.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, ConfigurationManager config)
        {
            Base.ConfigureServices(services, config);
            return services;
        }
    }
}