using Microsoft.AspNetCore.Authentication;

namespace VShop.Web.Configuration;

public static class AuthenticateConfig
{

    public static IServiceCollection AdicionarAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options => 
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
        })
            .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = configuration["ServicesUri:IdentityServer"];
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClientId = "vshop";
                options.ClientSecret = configuration["Client:Secret"];
                options.ResponseType = "code";
                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";
                options.Scope.Add("vshop");
                options.SaveTokens = true;
            });
        
        return services;
    }
}
