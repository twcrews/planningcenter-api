namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Provides a Planning Center API access token for use in environments where
/// <see cref="Microsoft.AspNetCore.Http.IHttpContextAccessor"/> cannot supply one (e.g. Blazor
/// Interactive Server after the SSR prerender pass).
/// </summary>
public interface IPlanningCenterTokenProvider
{
    /// <summary>Returns the current access token, or <see langword="null"/> if unavailable.</summary>
    Task<string?> GetAccessTokenAsync();
}
