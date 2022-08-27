using System;
using System.Security.Claims;

namespace VShop.Core.Usuario
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserID(this ClaimsPrincipal principal)
        {
            if(principal is null) throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("sub");
            return claim?.Value;
        }
    }
}