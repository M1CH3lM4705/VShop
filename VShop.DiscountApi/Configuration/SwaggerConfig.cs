using Microsoft.OpenApi.Models;

namespace VShop.DiscountApi.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwagger(this IServiceCollection services){
        services.AddSwaggerGen(c =>{
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "VShop Api Discount", Description = "Teste", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"Digite 'Bearer' [espaço] seu token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "ouath2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
        return services;
    }
}