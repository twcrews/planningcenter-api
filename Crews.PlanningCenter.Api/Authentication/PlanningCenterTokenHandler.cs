using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.Authentication;

internal sealed class PlanningCenterTokenHandler(
    IHttpContextAccessor httpContextAccessor,
    IPlanningCenterTokenProvider? tokenProvider = null) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = null;

        if (httpContextAccessor.HttpContext is { } ctx)
        {
            // Resolve from the current request scope to avoid the captive dependency
            // problem: IHttpClientFactory creates handlers in a separate DI scope, so
            // the tokenProvider field may be a different instance than the one the
            // caller configured (e.g. a Blazor component that called SetToken).
            var scopedProvider = ctx.RequestServices.GetService<IPlanningCenterTokenProvider>();
            if (scopedProvider is not null)
            {
                token = await scopedProvider.GetAccessTokenAsync();
            }

            // Fall back to reading directly from the authentication cookie (covers the
            // interactive Blazor Server circuit, where the scoped provider may be fresh
            // and unpopulated but the WebSocket upgrade request carries the auth cookie).
            token ??= await ctx.GetTokenAsync("access_token");
        }
        else if (tokenProvider is not null)
        {
            // HttpContext is genuinely unavailable (e.g. background services).
            // Use the captured instance as a last resort.
            token = await tokenProvider.GetAccessTokenAsync();
        }

        if (token is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
