using Crews.PlanningCenter.Api.Authentication;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOAuthOptionsTests
{
    [Fact(DisplayName = "Constructor sets correct default values")]
    public void Constructor_SetsCorrectDefaults()
    {
		// Act
		PlanningCenterOAuthOptions options = new();

        // Assert
        Assert.Equal(PlanningCenterOAuthDefaults.CallbackPath, options.CallbackPath);
        Assert.Equal(PlanningCenterOAuthDefaults.AuthorizationEndpoint, options.AuthorizationEndpoint);
        Assert.Equal(PlanningCenterOAuthDefaults.TokenEndpoint, options.TokenEndpoint);
        Assert.Equal(PlanningCenterOAuthDefaults.UserInformationEndpoint, options.UserInformationEndpoint);
        Assert.Equal(PlanningCenterOAuthDefaults.Issuer, options.ClaimsIssuer);
        Assert.Contains("people", options.Scope);
    }

    [Fact(DisplayName = "Options inherit from OAuthOptions correctly")]
    public void Options_InheritFromOAuthOptionsCorrectly()
    {
		// Act
		PlanningCenterOAuthOptions options = new();

        // Assert
        Assert.NotNull(options.Scope);
        Assert.NotNull(options.ClaimActions);
        Assert.NotNull(options.Events);
    }
}