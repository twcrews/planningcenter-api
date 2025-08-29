using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Crews.PlanningCenter.Auth.Models;

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

        ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        ClaimActions.MapJsonKey(ClaimTypes.Name, "attributes.name");
        ClaimActions.MapJsonKey(ClaimTypes.DateOfBirth, "attributes.birthdate");
        ClaimActions.MapJsonKey(ClaimTypes.Gender, "attributes.gender");
        ClaimActions.MapJsonKey("urn:planningcenter:first_name", "attributes.first_name");
        ClaimActions.MapJsonKey("urn:planningcenter:last_name", "attributes.last_name");
        ClaimActions.MapJsonKey("urn:planningcenter:avatar", "attributes.avatar");
        ClaimActions.MapJsonKey("urn:planningcenter:status", "attributes.status");
        ClaimActions.MapJsonKey("urn:planningcenter:site_administrator", "attributes.site_administrator");
        ClaimActions.MapJsonKey("urn:planningcenter:accounting_administrator", "attributes.accounting_administrator");
        ClaimActions.MapJsonKey("urn:planningcenter:people_permissions", "attributes.people_permissions");
        ClaimActions.MapJsonKey("urn:planningcenter:membership", "attributes.membership");
        ClaimActions.MapJsonKey("urn:planningcenter:directory_status", "attributes.directory_status");
        ClaimActions.MapJsonKey("urn:planningcenter:passed_background_check", "attributes.passed_background_check");
        ClaimActions.MapJsonKey("urn:planningcenter:resource_permission_flags", "attributes.resource_permission_flags");
        ClaimActions.MapJsonKey("urn:planningcenter:login_identifier", "attributes.login_identifier");
        ClaimActions.MapJsonKey("urn:planningcenter:mfa_configured", "attributes.mfa_configured");
    }
}