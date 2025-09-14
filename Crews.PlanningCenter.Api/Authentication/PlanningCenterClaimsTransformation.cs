using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Claims transformation service that can refresh Planning Center user data.
/// </summary>
class PlanningCenterClaimsTransformation : IClaimsTransformation
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PlanningCenterClaimsTransformation> _logger;
    private readonly PlanningCenterClaimsTransformationOptions _options;

    public PlanningCenterClaimsTransformation(
        IHttpClientFactory httpClientFactory,
        ILogger<PlanningCenterClaimsTransformation> logger,
        IOptions<PlanningCenterClaimsTransformationOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
		// Only transform if this is a Planning Center authentication (be more flexible about the auth scheme)
		if (principal.Identity is not ClaimsIdentity identity || !identity.IsAuthenticated)
			return principal;

		// Check if this looks like a Planning Center OAuth authentication by looking for access_token
		bool hasAccessToken = identity.FindFirst("access_token") != null;
        if (!hasAccessToken)
            return principal;

        // Check if we should refresh claims based on options
        if (!ShouldRefreshClaims(identity))
            return principal;

        try
        {
            await RefreshUserClaimsAsync(identity);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to refresh Planning Center user claims");
        }

        return principal;
    }

    private bool ShouldRefreshClaims(ClaimsIdentity identity)
    {
        if (!_options.EnableClaimsRefresh)
            return false;

		// Check if claims are older than the refresh interval
		Claim? lastRefreshClaim = identity.FindFirst("urn:planningcenter:last_refresh");
        if (lastRefreshClaim == null)
            return true;

        if (DateTime.TryParse(lastRefreshClaim.Value, out DateTime lastRefresh))
        {
            return DateTime.UtcNow - lastRefresh > _options.ClaimsRefreshInterval;
        }

        return true;
    }

    private async Task RefreshUserClaimsAsync(ClaimsIdentity identity)
    {
		Claim? accessTokenClaim = identity.FindFirst("access_token");
        if (accessTokenClaim == null)
        {
            _logger.LogWarning("No access token found in claims, cannot refresh user data");
            return;
        }

        using HttpClient httpClient = _httpClientFactory.CreateClient();
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, PlanningCenterOAuthDefaults.UserInformationEndpoint);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessTokenClaim.Value);

        using HttpResponseMessage response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Failed to refresh user information from Planning Center: {StatusCode}", response.StatusCode);
            return;
        }

        using JsonDocument payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
		JsonElement data = payload.RootElement.GetProperty("data");
		JsonElement attributes = data.GetProperty("attributes");

        // Remove old refreshable claims and add new ones
        RemoveRefreshableClaims(identity);
        AddClaimsFromUserData(identity, data, attributes);

        // Update last refresh time
        identity.AddClaim(new Claim("urn:planningcenter:last_refresh", DateTime.UtcNow.ToString("O"), ClaimValueTypes.DateTime));
    }

    private static void RemoveRefreshableClaims(ClaimsIdentity identity)
    {
		List<Claim> claimsToRemove = identity.Claims
            .Where(c => c.Type.StartsWith("urn:planningcenter:") && c.Type != "urn:planningcenter:last_refresh")
            .ToList();

        foreach (Claim? claim in claimsToRemove)
        {
            identity.RemoveClaim(claim);
        }
    }

    private static void AddClaimsFromUserData(ClaimsIdentity identity, JsonElement data, JsonElement attributes)
    {
        // Add refreshed user claims
        if (attributes.TryGetProperty("name", out JsonElement name) && !string.IsNullOrEmpty(name.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.Name, name.GetString()!));
        }

        if (attributes.TryGetProperty("first_name", out JsonElement firstName) && !string.IsNullOrEmpty(firstName.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:first_name", firstName.GetString()!));
        }

        if (attributes.TryGetProperty("last_name", out JsonElement lastName) && !string.IsNullOrEmpty(lastName.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:last_name", lastName.GetString()!));
        }

        if (attributes.TryGetProperty("avatar", out JsonElement avatar) && !string.IsNullOrEmpty(avatar.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:avatar", avatar.GetString()!));
        }

        if (attributes.TryGetProperty("status", out JsonElement status) && !string.IsNullOrEmpty(status.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:status", status.GetString()!));
        }

        if (attributes.TryGetProperty("site_administrator", out JsonElement siteAdmin))
        {
            identity.AddClaim(new Claim("urn:planningcenter:site_administrator", siteAdmin.GetBoolean().ToString().ToLowerInvariant()));
        }

        if (attributes.TryGetProperty("accounting_administrator", out JsonElement accountingAdmin))
        {
            identity.AddClaim(new Claim("urn:planningcenter:accounting_administrator", accountingAdmin.GetBoolean().ToString().ToLowerInvariant()));
        }

        if (attributes.TryGetProperty("birthdate", out JsonElement birthdate) && !string.IsNullOrEmpty(birthdate.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, birthdate.GetString()!));
        }

        if (attributes.TryGetProperty("people_permissions", out JsonElement peoplePermissions) && !string.IsNullOrEmpty(peoplePermissions.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:people_permissions", peoplePermissions.GetString()!));
        }

        if (attributes.TryGetProperty("gender", out JsonElement gender) && !string.IsNullOrEmpty(gender.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.Gender, gender.GetString()!));
        }

        if (attributes.TryGetProperty("membership", out JsonElement membership) && !string.IsNullOrEmpty(membership.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:membership", membership.GetString()!));
        }

        if (attributes.TryGetProperty("directory_status", out JsonElement directoryStatus) && !string.IsNullOrEmpty(directoryStatus.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:directory_status", directoryStatus.GetString()!));
        }

        if (attributes.TryGetProperty("passed_background_check", out JsonElement passedBackgroundCheck))
        {
            identity.AddClaim(new Claim("urn:planningcenter:passed_background_check", passedBackgroundCheck.GetBoolean().ToString().ToLowerInvariant()));
        }

        if (attributes.TryGetProperty("resource_permission_flags", out JsonElement resourcePermissionFlags) && !string.IsNullOrEmpty(resourcePermissionFlags.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:resource_permission_flags", resourcePermissionFlags.GetString()!));
        }

        if (attributes.TryGetProperty("login_identifier", out JsonElement loginIdentifier) && !string.IsNullOrEmpty(loginIdentifier.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:login_identifier", loginIdentifier.GetString()!));
        }

        if (attributes.TryGetProperty("mfa_configured", out JsonElement mfaConfigured))
        {
            identity.AddClaim(new Claim("urn:planningcenter:mfa_configured", mfaConfigured.GetBoolean().ToString().ToLowerInvariant()));
        }
    }
}

/// <summary>
/// Options for configuring Planning Center claims transformation.
/// </summary>
public class PlanningCenterClaimsTransformationOptions
{
    /// <summary>
    /// Gets or sets whether claims refresh is enabled. Default is true.
    /// </summary>
    public bool EnableClaimsRefresh { get; set; } = true;

    /// <summary>
    /// Gets or sets the interval for refreshing claims. Default is 1 hour.
    /// </summary>
    public TimeSpan ClaimsRefreshInterval { get; set; } = TimeSpan.FromHours(1);
}