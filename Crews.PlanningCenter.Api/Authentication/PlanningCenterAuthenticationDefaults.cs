namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Default values used by Planning Center OpenID Connect authentication.
/// </summary>
public static class PlanningCenterAuthenticationDefaults
{
    /// <summary>
    /// The default configuration section for authentication options.
    /// </summary>
    public const string ConfigurationSection = "Authentication:PlanningCenter";

    /// <summary>
    /// The default authentication scheme name for Planning Center OAuth.
    /// </summary>
    public const string AuthenticationScheme = "planningcenter";

    /// <summary>
    /// The Planning Center OIDC authority URL.
    /// </summary>
    public const string Authority = "https://api.planningcenteronline.com";
}
