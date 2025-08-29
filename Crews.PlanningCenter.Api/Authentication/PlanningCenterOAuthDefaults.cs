using Crews.PlanningCenter.Auth.Models;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Default values used by the Planning Center OAuth authentication handler.
/// </summary>
public static class PlanningCenterOAuthDefaults
{
    /// <summary>
    /// The default authentication scheme name for Planning Center OAuth.
    /// </summary>
    public const string AuthenticationScheme = "PlanningCenter";

    /// <summary>
    /// The default display name for Planning Center OAuth authentication.
    /// </summary>
    public const string DisplayName = "Planning Center";

    /// <summary>
    /// The default issuer name for Planning Center OAuth claims.
    /// </summary>
    public const string Issuer = "Planning Center";

    /// <summary>
    /// The default callback path where Planning Center will redirect after authorization.
    /// </summary>
    public const string CallbackPath = "/signin-planningcenter";

    /// <summary>
    /// The Planning Center OAuth authorization endpoint URL.
    /// </summary>
    public const string AuthorizationEndpoint = "https://api.planningcenteronline.com/oauth/authorize";

    /// <summary>
    /// The Planning Center OAuth token endpoint URL.
    /// </summary>
    public const string TokenEndpoint = "https://api.planningcenteronline.com/oauth/token";

    /// <summary>
    /// The Planning Center API endpoint for retrieving user information.
    /// </summary>
    public const string UserInformationEndpoint = "https://api.planningcenteronline.com/people/v2/me";

    /// <summary>
    /// The Planning Center API endpoint for retrieving user email addresses.
    /// </summary>
    public const string UserEmailsEndpoint = "https://api.planningcenteronline.com/people/v2/me/emails";

    /// <summary>
    /// The path portion of the Planning Center OAuth authorization endpoint.
    /// </summary>
    public const string AuthorizationEndpointPath = "/oauth/authorize";

    /// <summary>
    /// The path portion of the Planning Center OAuth token endpoint.
    /// </summary>
    public const string TokenEndpointPath = "/oauth/token";

    /// <summary>
    /// The path portion of the Planning Center user information endpoint.
    /// </summary>
    public const string UserInformationEndpointPath = "/people/v2/me";

    /// <summary>
    /// The path portion of the Planning Center user emails endpoint.
    /// </summary>
    public const string UserEmailsEndpointPath = "/people/v2/me/emails";

    /// <summary>
    /// The default OAuth scope for Planning Center authentication (People API access).
    /// </summary>
    public static readonly string Scope = PlanningCenterOAuthScope.People;
}
