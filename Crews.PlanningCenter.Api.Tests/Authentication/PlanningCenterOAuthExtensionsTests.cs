using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.Tests.Authentication;

public class PlanningCenterOAuthExtensionsTests
{
    [Fact(DisplayName = "AddPlanningCenterOAuth with no parameters uses defaults")]
    public void AddPlanningCenterOAuth_WithNoParameters_UsesDefaults()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDataProtection();
        var authBuilder = new AuthenticationBuilder(services);

        // Act
        var result = authBuilder.AddPlanningCenterOAuth();

        // Assert
        Assert.Same(authBuilder, result);
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth with configure action works")]
    public void AddPlanningCenterOAuth_WithConfigureAction_Works()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDataProtection();
        var authBuilder = new AuthenticationBuilder(services);

        // Act
        var result = authBuilder.AddPlanningCenterOAuth(options =>
        {
            options.ClientId = "test-client-id";
        });

        // Assert
        Assert.Same(authBuilder, result);
        
        // Verify that the extension method worked by checking the services were registered
        Assert.Contains(services, s => s.ServiceType.Name.Contains("PlanningCenterOAuth"));
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth with custom scheme works")]
    public void AddPlanningCenterOAuth_WithCustomScheme_Works()
    {
        // Arrange
        var services = new ServiceCollection();
        var authBuilder = new AuthenticationBuilder(services);
        const string customScheme = "CustomPlanningCenter";

        // Act
        var result = authBuilder.AddPlanningCenterOAuth(customScheme, options =>
        {
            options.ClientId = "test-client-id";
        });

        // Assert
        Assert.Same(authBuilder, result);
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth with custom scheme and display name works")]
    public void AddPlanningCenterOAuth_WithCustomSchemeAndDisplayName_Works()
    {
        // Arrange
        var services = new ServiceCollection();
        var authBuilder = new AuthenticationBuilder(services);
        const string customScheme = "CustomPlanningCenter";
        const string customDisplayName = "Custom Planning Center";

        // Act
        var result = authBuilder.AddPlanningCenterOAuth(customScheme, customDisplayName, options =>
        {
            options.ClientId = "test-client-id";
        });

        // Assert
        Assert.Same(authBuilder, result);
    }
}