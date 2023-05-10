using Tienda.Presentation.Extensions;

namespace Tienda.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddMemoryCache();
            builder.Services.AddRouting().AddResponseCaching().AddResponseCompression();
            builder.Services.Configure<AppSettings>(builder.Configuration);

            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllers();
            app.UseRouting().UseResponseCaching().UseResponseCompression();
            app.MapDefaultControllerRoute();
            app.UseCors();
            app.UseHttpsRedirection();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }
            app.Run();
        }
    }
}