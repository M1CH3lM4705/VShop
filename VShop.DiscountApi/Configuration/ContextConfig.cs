using Microsoft.EntityFrameworkCore;
using VShop.DiscountApi.Context;

namespace VShop.DiscountApi.Configuration;

public static class ContextConfig 
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(option =>
                    option.UseMySql(mySqlConnection,
                        ServerVersion.AutoDetect(mySqlConnection)));
    }
}
