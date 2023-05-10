using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tienda.Domain.Context;
using Tienda.Domain.Interfaces;
using Tienda.Domain.Repository;
using Tienda.Service;

namespace Tienda.BaseService
{
    public static class Base
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<TiendaDBContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Tienda.Domain"));
                //options.LogTo(Console.WriteLine);
            });

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonImagesRepository, PersonImagesRepository>();
            services.AddScoped<IPersonService, PersonService>();
            return services;
        }
    }
}