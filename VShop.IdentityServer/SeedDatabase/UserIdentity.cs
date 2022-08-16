using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data.ContextUser;

namespace VShop.IdentityServer.SeedDatabase;

public static class UserIdentity
{
    public static void CriarAdmin(UserManager<ApplicationUser> _userManager)
    {   
        var admin = new ApplicationUser
        {
            UserName = "admin1",
            NormalizedUserName = "ADMIN1",
            Email = "admin1@com.br",
            NormalizedEmail = "ADMIN1@COM.BR",
            EmailConfirmed = true,
            LockoutEnabled = false,
            PhoneNumber = "+55 (69) 12345-6789",
            FirstName = "Usuario",
            LastName = "Admin1",
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        if(_userManager.FindByEmailAsync("admin1@com.br").Result == null)
        {
            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "G5h4q1x8#").Result;

            if(resultAdmin.Succeeded)
            {
                _userManager.AddToRoleAsync(admin, IdentityConfig.Admin).Wait();

                var addminClaims = _userManager.AddClaimsAsync(admin, new Claim[]{
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfig.Admin)
                }).Result;
            }
        }
    }
    public static void CriarUser(UserManager<ApplicationUser> _userManager)
    {
        var client = new ApplicationUser
        {
            UserName = "client",
            NormalizedUserName = "CLIENT1",
            Email = "client1@com.br",
            NormalizedEmail = "CLIENT1@COM.BR",
            EmailConfirmed = true,
            LockoutEnabled = false,
            PhoneNumber = "+55 (69) 12345-6789",
            FirstName = "Usuario",
            LastName = "Client1",
            SecurityStamp = Guid.NewGuid().ToString()
        };
        
        if(_userManager.FindByEmailAsync("client1@com.br").Result == null)
        {
            IdentityResult resultClient = _userManager.CreateAsync(client, "G5h4q1x8#").Result;

            if(resultClient.Succeeded)
            {
                _userManager.AddToRoleAsync(client, IdentityConfig.Client).Wait();

                var addminClaims = _userManager.AddClaimsAsync(client, new Claim[]{
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfig.Client)
                }).Result;
            }
        }
    }

}