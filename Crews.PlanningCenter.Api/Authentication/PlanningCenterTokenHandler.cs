using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Crews.PlanningCenter.Api.Authentication;

internal sealed class PlanningCenterTokenHandler(
    IHttpContextAccessor httpContextAccessor,
    IPlanningCenterTokenProvider? tokenProvider = null) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = tokenProvider is not null
            ? await tokenProvider.GetAccessTokenAsync()
            : httpContextAccessor.HttpContext is { } ctx
                ? await ctx.GetTokenAsync("access_token")
                : null;

        if (token is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
