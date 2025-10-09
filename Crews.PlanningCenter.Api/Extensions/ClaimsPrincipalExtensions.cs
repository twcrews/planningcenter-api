using System.Security.Claims;

namespace Crews.PlanningCenter.Api.Extensions;

/// <summary>
/// Extension methods for <see cref="ClaimsPrincipal"/> to access Planning Center OIDC claims.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Gets the subject (sub) claim value, representing the user ID.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The subject claim value, or null if not found.</returns>
    public static string? GetSubject(this ClaimsPrincipal principal)
        => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    /// <summary>
    /// Gets the user's name.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The user's name, or null if not found.</returns>
    public static string? GetName(this ClaimsPrincipal principal)
        => principal.Identity?.Name;

    /// <summary>
    /// Gets the user's email address.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The email claim value, or null if not found.</returns>
    public static string? GetEmail(this ClaimsPrincipal principal)
        => principal.FindFirst(ClaimTypes.Email)?.Value;

    /// <summary>
    /// Gets the organization ID.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The organization ID claim value, or null if not found.</returns>
    public static string? GetOrganizationId(this ClaimsPrincipal principal)
        => principal.FindFirst("organization_id")?.Value;

    /// <summary>
    /// Gets the organization name.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The organization name claim value, or null if not found.</returns>
    public static string? GetOrganizationName(this ClaimsPrincipal principal)
        => principal.FindFirst("organization_name")?.Value;
}
