using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data.ContextUser;

namespace VShop.IdentityServer.SeedDatabase;

public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeSeedRoles()
    {
        if(!_roleManager.RoleExistsAsync(IdentityConfig.Admin).Result)
        {
            IdentityRole roleAdmin = new();
            roleAdmin.Name = IdentityConfig.Admin;
            roleAdmin.NormalizedName = IdentityConfig.Admin.ToUpper();
            _roleManager.CreateAsync(roleAdmin).Wait();
        }
        if(!_roleManager.RoleExistsAsync(IdentityConfig.Client).Result)
        {
            IdentityRole roleClient = new();
            roleClient.Name = IdentityConfig.Client;
            roleClient.NormalizedName = IdentityConfig.Client.ToUpper();
            _roleManager.CreateAsync(roleClient).Wait();
        }
    }

    public void InitializeSeedUsers()
    {
        UserIdentity.CriarAdmin(_userManager);
        UserIdentity.CriarUser(_userManager);
    }
}