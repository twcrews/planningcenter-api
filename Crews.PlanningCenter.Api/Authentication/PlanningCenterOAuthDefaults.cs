namespace Crews.PlanningCenter.Api.Authentication;
internal static class PlanningCenterOAuthDefaults
{
    public const string AuthenticationScheme = "PlanningCenter";
    public const string DisplayName = "Planning Center";
    public const string Issuer = "Planning Center";
    public const string CallbackPath = "/signin-planningcenter";
    public const string AuthorizationEndpoint = "https://api.planningcenteronline.com/oauth/authorize";
    public const string TokenEndpoint = "https://api.planningcenteronline.com/oauth/token";
    public const string UserInformationEndpoint = "https://api.planningcenteronline.com/people/v2/me";
    public const string UserEmailsEndpoint = "https://api.planningcenteronline.com/people/v2/me/emails";
    public const string AuthorizationEndpointPath = "/oauth/authorize";
    public const string TokenEndpointPath = "/oauth/token";
    public const string UserInformationEndpointPath = "/people/v2/me";
    public const string UserEmailsEndpointPath = "/people/v2/me/emails";
}
