

using VShop.Web.Services;
using VShop.Web.Services.Interfaces;

namespace VShop.Web;

public class Startup : IStartUp
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddHttpClient("ProductApi", c =>
        {
            c.BaseAddress = new Uri(Configuration["ServicesUri:ProductApi"]);
        });

        services.AddScoped<IProductService, ProductService>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }

}
public interface IStartUp
{
    IConfiguration Configuration { get; }
    void Configure(WebApplication app, IWebHostEnvironment env);
    void ConfigureServices(IServiceCollection services);
}

public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webAppBuilder) where TStartup : IStartUp
    {
        var startup = Activator.CreateInstance(typeof(TStartup), webAppBuilder.Configuration) as IStartUp;
        if(startup == null) throw new ArgumentException("Classe Startup.cs inválida");

        startup.ConfigureServices(webAppBuilder.Services);

        var app = webAppBuilder.Build();

        startup.Configure(app, app.Environment);

        app.Run();

        return webAppBuilder;
    }
}