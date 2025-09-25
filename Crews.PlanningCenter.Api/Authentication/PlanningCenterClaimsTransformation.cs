using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Claims transformation service that can refresh Planning Center user data.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PlanningCenterClaimsTransformation"/> class.
/// </remarks>
/// <param name="httpClientFactory">The HTTP client factory for making requests to Planning Center.</param>
/// <param name="logger">The logger for logging transformation activities.</param>
/// <param name="options">The options for configuring claims transformation behavior.</param>
public class PlanningCenterClaimsTransformation(
    IHttpClientFactory httpClientFactory,
    ILogger<PlanningCenterClaimsTransformation> logger,
    IOptions<PlanningCenterClaimsTransformationOptions> options) : IClaimsTransformation
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ILogger<PlanningCenterClaimsTransformation> _logger = logger;
    private readonly PlanningCenterClaimsTransformationOptions _options = options.Value;

    /// <summary>
    /// Transforms the claims principal by refreshing user data from Planning Center if needed.
    /// </summary>
    /// <param name="principal">The claims principal to transform.</param>
    /// <returns>The transformed claims principal with refreshed user data.</returns>
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

    /// <summary>
    /// Determines whether the claims should be refreshed based on the configured options and last refresh time.
    /// </summary>
    /// <param name="identity">The claims identity to check.</param>
    /// <returns>True if claims should be refreshed; otherwise, false.</returns>
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

    /// <summary>
    /// Refreshes user claims by fetching updated user data from Planning Center.
    /// </summary>
    /// <param name="identity">The claims identity to refresh.</param>
    /// <returns>A task representing the asynchronous refresh operation.</returns>
    private async Task RefreshUserClaimsAsync(ClaimsIdentity identity)
    {
        Claim? accessTokenClaim = identity.FindFirst("access_token");
        if (accessTokenClaim == null)
        {
            _logger.LogWarning("No access token found in claims, cannot refresh user data");
            return;
        }

        using HttpClient httpClient = _httpClientFactory.CreateClient();
        using HttpRequestMessage request = new(HttpMethod.Get, PlanningCenterOAuthDefaults.UserInformationEndpoint);
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
        identity.AddClaim(
            new Claim("urn:planningcenter:last_refresh", DateTime.UtcNow.ToString("O"), ClaimValueTypes.DateTime));
    }

    /// <summary>
    /// Removes refreshable user claims from the identity, preserving system claims like access_token and last refresh timestamp.
    /// </summary>
    /// <param name="identity">The claims identity to modify.</param>
    private static void RemoveRefreshableClaims(ClaimsIdentity identity)
    {
        // Remove user-related claims that will be refreshed, but preserve system claims
        List<Claim> claimsToRemove = identity.Claims
            .Where(c => c.Type != "access_token" && c.Type != "urn:planningcenter:last_refresh")
            .ToList();

        foreach (Claim claim in claimsToRemove)
            identity.RemoveClaim(claim);
    }

    /// <summary>
    /// Adds claims to the identity based on user data returned from Planning Center.
    /// </summary>
    /// <param name="identity">The claims identity to add claims to.</param>
    /// <param name="data">The data element from the Planning Center API response.</param>
    /// <param name="attributes">The attributes element containing user information.</param>
    private static void AddClaimsFromUserData(ClaimsIdentity identity, JsonElement data, JsonElement attributes)
    {
        // Add refreshed user claims using standardized claim types
        if (data.TryGetProperty("id", out JsonElement id) && !string.IsNullOrEmpty(id.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id.GetString()!));
        }

        if (attributes.TryGetProperty("name", out JsonElement name) && !string.IsNullOrEmpty(name.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.Name, name.GetString()!));
        }

        if (attributes.TryGetProperty("first_name", out JsonElement firstName) && !string.IsNullOrEmpty(firstName.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName.GetString()!));
        }

        if (attributes.TryGetProperty("last_name", out JsonElement lastName) && !string.IsNullOrEmpty(lastName.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.Surname, lastName.GetString()!));
        }

        if (attributes.TryGetProperty("middle_name", out JsonElement middleName) && !string.IsNullOrEmpty(middleName.GetString()))
        {
            identity.AddClaim(new Claim("middle_name", middleName.GetString()!));
        }

        if (attributes.TryGetProperty("nickname", out JsonElement nickname) && !string.IsNullOrEmpty(nickname.GetString()))
        {
            identity.AddClaim(new Claim("nickname", nickname.GetString()!));
        }

        if (attributes.TryGetProperty("avatar", out JsonElement avatar) && !string.IsNullOrEmpty(avatar.GetString()))
        {
            identity.AddClaim(new Claim("picture", avatar.GetString()!));
        }

        if (attributes.TryGetProperty("birthdate", out JsonElement birthdate) && !string.IsNullOrEmpty(birthdate.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, birthdate.GetString()!));
        }

        if (attributes.TryGetProperty("gender", out JsonElement gender) && !string.IsNullOrEmpty(gender.GetString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.Gender, gender.GetString()!));
        }

        if (attributes.TryGetProperty("status", out JsonElement status) && !string.IsNullOrEmpty(status.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:status", status.GetString()!));
        }

        if (attributes.TryGetProperty("child", out JsonElement child))
        {
            identity.AddClaim(new Claim("urn:planningcenter:child", child.GetBoolean().ToString().ToLowerInvariant()));
        }

        if (attributes.TryGetProperty("grade", out JsonElement grade) && grade.ValueKind != JsonValueKind.Null)
        {
            identity.AddClaim(new Claim("urn:planningcenter:grade", grade.ToString()));
        }

        if (attributes.TryGetProperty("graduation_year", out JsonElement graduationYear) && graduationYear.ValueKind != JsonValueKind.Null)
        {
            identity.AddClaim(new Claim("urn:planningcenter:graduation_year", graduationYear.ToString()));
        }

        if (attributes.TryGetProperty("membership", out JsonElement membership) && !string.IsNullOrEmpty(membership.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:membership", membership.GetString()!));
        }

        if (attributes.TryGetProperty("school_type", out JsonElement schoolType) && !string.IsNullOrEmpty(schoolType.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:school_type", schoolType.GetString()!));
        }

        if (attributes.TryGetProperty("anniversary", out JsonElement anniversary) && !string.IsNullOrEmpty(anniversary.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:anniversary", anniversary.GetString()!));
        }

        if (attributes.TryGetProperty("people_permissions", out JsonElement peoplePermissions) && !string.IsNullOrEmpty(peoplePermissions.GetString()))
        {
            identity.AddClaim(new Claim("urn:planningcenter:people_permissions", peoplePermissions.GetString()!));
        }

        if (attributes.TryGetProperty("accounting_administrator", out JsonElement accountingAdmin))
        {
            identity.AddClaim(new Claim("urn:planningcenter:accounting_admin", accountingAdmin.GetBoolean().ToString().ToLowerInvariant()));
        }

        if (attributes.TryGetProperty("site_administrator", out JsonElement siteAdmin))
        {
            identity.AddClaim(new Claim("urn:planningcenter:site_admin", siteAdmin.GetBoolean().ToString().ToLowerInvariant()));
        }

        if (attributes.TryGetProperty("can_create_forms", out JsonElement canCreateForms))
        {
            identity.AddClaim(new Claim("urn:planningcenter:can_create_forms", canCreateForms.GetBoolean().ToString().ToLowerInvariant()));
        }

        if (attributes.TryGetProperty("can_email_lists", out JsonElement canEmailLists))
        {
            identity.AddClaim(new Claim("urn:planningcenter:can_email_lists", canEmailLists.GetBoolean().ToString().ToLowerInvariant()));
        }

        if (attributes.TryGetProperty("passed_background_check", out JsonElement passedBackgroundCheck))
        {
            identity.AddClaim(new Claim("urn:planningcenter:background_check", passedBackgroundCheck.GetBoolean().ToString().ToLowerInvariant()));
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