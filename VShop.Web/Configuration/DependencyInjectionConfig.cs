
using VShop.Web.Services;
using VShop.Web.Services.Handlers;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<HttpClientAuthorizationDelegationHandler>();

        services.AddHttpClient<IProductService, ProductService>("ProductApi", c =>
        {
            c.BaseAddress = new Uri(configuration["ServicesUri:ProductApi"]);
            c.DefaultRequestHeaders.Add("Connection", "Keep Alive");
            c.DefaultRequestHeaders.Add("Keep-Alive", "3600");
            c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-ProductApi");
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegationHandler>();

        services.AddHttpClient<ICartService, CartService>("CartApi",
            c => c.BaseAddress = new Uri(configuration["ServicesUri:CartApi"]))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegationHandler>();;

        services.AddScoped<ICategoryService, CategoryService>();
        // services.AddScoped<IProductService, ProductService>();
        // services.AddScoped<ICartService, CartService>();

        return services;
    }
}
