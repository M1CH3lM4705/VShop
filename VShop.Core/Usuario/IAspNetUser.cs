using Microsoft.AspNetCore.Http;

namespace VShop.Core.Usuario;

public interface IAspNetUser
{
    Guid ObterUserId();
    bool EstaAutenticado();
    HttpContext ObterHttpContext();
    bool PossuiRole(string role);
}
