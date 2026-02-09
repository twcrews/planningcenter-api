namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Default values for Planning Center authentication.
/// </summary>
public static class PlanningCenterAuthenticationDefaults
{
	/// <summary>
	/// The base URL for the Planning Center API.
	/// </summary>
	public const string BaseUrl = "https://api.planningcenteronline.com";

	/// <summary>
	/// The OAuth 2.0 authorization endpoint.
	/// </summary>
	public const string AuthorizationEndpoint = "https://api.planningcenteronline.com/oauth/authorize";

	/// <summary>
	/// The OAuth 2.0 token endpoint.
	/// </summary>
	public const string TokenEndpoint = "https://api.planningcenteronline.com/oauth/token";

	/// <summary>
	/// The OIDC userinfo endpoint.
	/// </summary>
	public const string UserInfoEndpoint = "https://api.planningcenteronline.com/oauth/userinfo";

	/// <summary>
	/// The OIDC discovery endpoint (OpenID configuration).
	/// </summary>
	public const string DiscoveryEndpoint = "https://api.planningcenteronline.com/.well-known/openid-configuration";

	/// <summary>
	/// The default authentication scheme name for Planning Center OIDC.
	/// </summary>
	public const string AuthenticationScheme = "PlanningCenter";

	/// <summary>
	/// The display name for the Planning Center authentication scheme.
	/// </summary>
	public const string DisplayName = "Planning Center";

	/// <summary>
	/// The recommended prompt value for OIDC authorization requests.
	/// Allows users to verify which account they will be signing in as and switch accounts.
	/// </summary>
	public const string RecommendedPrompt = "select_account";

	/// <summary>
	/// Alternative prompt value that forces users to log in again.
	/// </summary>
	public const string LoginPrompt = "login";

	/// <summary>
	/// The access token lifetime in seconds (2 hours).
	/// </summary>
	public const int AccessTokenLifetimeSeconds = 7200;

	/// <summary>
	/// The ID token lifetime in seconds (1 hour).
	/// </summary>
	public const int IdTokenLifetimeSeconds = 3600;

	/// <summary>
	/// The refresh token lifetime in days (90 days).
	/// </summary>
	public const int RefreshTokenLifetimeDays = 90;
}
