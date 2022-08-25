using Microsoft.AspNetCore.Http;

namespace VShop.Core.Usuario;

public class AspNetUser : IAspNetUser
{
    private readonly IHttpContextAccessor _accessor;
    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public bool EstaAutenticado()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public HttpContext ObterHttpContext()
    {
        return _accessor.HttpContext;
    }

    public Guid ObterUserId()
    {
        return EstaAutenticado() ? Guid.Parse(_accessor.HttpContext.User.GetUserID()) : Guid.Empty;
    }

    public bool PossuiRole(string role)
    {
        return _accessor.HttpContext.User.IsInRole(role);
    }
}
