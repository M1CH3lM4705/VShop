using Microsoft.AspNetCore.Identity;

namespace VShop.IdentityServer.Data.ContextUser;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }  = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
