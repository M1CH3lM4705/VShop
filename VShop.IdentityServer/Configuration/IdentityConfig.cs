using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShop.IdentityServer.Configuration
{
    public class IdentityConfig
    {
        public const string Admin = "Admin";
        public const string Client = "Cliente";


        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("vshop", "VShop Server"),
                new ApiScope(name:"read", "Read data."),
                new ApiScope(name:"write", "Write data."),
                new ApiScope(name:"delete", "Delete data.")
            };
        
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("abacadabra#simsalabim".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profiles"}
                },
                new Client
                {
                    ClientId = "vshop",
                    ClientSecrets = { new Secret("abacadabra#simsalabim".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:7218/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:7218/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "vshop"
                    }
                }
            };
    }
}