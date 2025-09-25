using Microsoft.AspNetCore.Authentication.OAuth;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Configuration options for Planning Center OAuth authentication.
/// </summary>
public class PlanningCenterOAuthOptions : OAuthOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlanningCenterOAuthOptions"/> class with default settings.
    /// </summary>
    public PlanningCenterOAuthOptions()
    {
        CallbackPath = PlanningCenterOAuthDefaults.CallbackPath;
        AuthorizationEndpoint = PlanningCenterOAuthDefaults.AuthorizationEndpoint;
        TokenEndpoint = PlanningCenterOAuthDefaults.TokenEndpoint;
        UserInformationEndpoint = PlanningCenterOAuthDefaults.UserInformationEndpoint;

        ClaimsIssuer = PlanningCenterOAuthDefaults.Issuer;

        Scope.Add(PlanningCenterOAuthDefaults.Scope);
    }
}