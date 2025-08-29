using Crews.PlanningCenter.Api.Authentication;
using System.Security.Claims;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOAuthOptionsTests
{
    [Fact(DisplayName = "Constructor sets correct default values")]
    public void Constructor_SetsCorrectDefaults()
    {
        // Act
        var options = new PlanningCenterOAuthOptions();

        // Assert
        Assert.Equal(PlanningCenterOAuthDefaults.CallbackPath, options.CallbackPath);
        Assert.Equal(PlanningCenterOAuthDefaults.AuthorizationEndpoint, options.AuthorizationEndpoint);
        Assert.Equal(PlanningCenterOAuthDefaults.TokenEndpoint, options.TokenEndpoint);
        Assert.Equal(PlanningCenterOAuthDefaults.UserInformationEndpoint, options.UserInformationEndpoint);
        Assert.Equal(PlanningCenterOAuthDefaults.Issuer, options.ClaimsIssuer);
        Assert.Contains("people", options.Scope);
    }

    [Fact(DisplayName = "Constructor configures claim actions for standard claims")]
    public void Constructor_ConfiguresStandardClaimActions()
    {
        // Act
        var options = new PlanningCenterOAuthOptions();

        // Assert - Verify claim actions are configured
        Assert.NotEmpty(options.ClaimActions);
        
        // Check that key claim types are mapped
        var claimActions = options.ClaimActions.ToList();
        Assert.Contains(claimActions, ca => ca.ClaimType == ClaimTypes.NameIdentifier);
        Assert.Contains(claimActions, ca => ca.ClaimType == ClaimTypes.Name);
        Assert.Contains(claimActions, ca => ca.ClaimType == ClaimTypes.DateOfBirth);
        Assert.Contains(claimActions, ca => ca.ClaimType == ClaimTypes.Gender);
    }

    [Fact(DisplayName = "Constructor configures claim actions for Planning Center specific claims")]
    public void Constructor_ConfiguresPlanningCenterSpecificClaimActions()
    {
        // Act
        var options = new PlanningCenterOAuthOptions();

        // Assert
        var claimActions = options.ClaimActions.ToList();
        
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:first_name");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:last_name");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:avatar");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:status");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:site_administrator");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:accounting_administrator");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:people_permissions");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:membership");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:directory_status");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:passed_background_check");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:resource_permission_flags");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:login_identifier");
        Assert.Contains(claimActions, ca => ca.ClaimType == "urn:planningcenter:mfa_configured");
    }

    [Fact(DisplayName = "Options inherit from OAuthOptions correctly")]
    public void Options_InheritFromOAuthOptionsCorrectly()
    {
        // Act
        var options = new PlanningCenterOAuthOptions();

        // Assert
        Assert.NotNull(options.Scope);
        Assert.NotNull(options.ClaimActions);
        Assert.NotNull(options.Events);
    }
}