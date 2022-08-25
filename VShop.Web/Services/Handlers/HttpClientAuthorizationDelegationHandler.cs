using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace VShop.Web.Services.Handlers;

public class HttpClientAuthorizationDelegationHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _accessor;

    public HttpClientAuthorizationDelegationHandler(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _accessor.HttpContext.GetTokenAsync("access_token").Result;

        if (token != null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return base.SendAsync(request, cancellationToken);
    }
}
