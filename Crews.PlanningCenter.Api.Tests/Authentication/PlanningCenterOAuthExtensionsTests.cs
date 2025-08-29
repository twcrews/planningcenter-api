using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

    [Fact(DisplayName = "AddPlanningCenterClaimsTransformation with no parameters uses defaults")]
    public void AddPlanningCenterClaimsTransformation_WithNoParameters_UsesDefaults()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddHttpClient(); // Required dependency
        services.AddLogging();

        // Act
        var result = services.AddPlanningCenterClaimsTransformation();

        // Assert
        Assert.Same(services, result);
        
        // Verify the transformation is registered
        var serviceProvider = services.BuildServiceProvider();
        var transformation = serviceProvider.GetService<IClaimsTransformation>();
        Assert.NotNull(transformation);
        Assert.IsType<PlanningCenterClaimsTransformation>(transformation);
    }

    [Fact(DisplayName = "AddPlanningCenterClaimsTransformation with configure action works")]
    public void AddPlanningCenterClaimsTransformation_WithConfigureAction_Works()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddHttpClient(); // Required for claims transformation
        var configureWasCalled = false;

        // Act
        var result = services.AddPlanningCenterClaimsTransformation(options =>
        {
            configureWasCalled = true;
            options.EnableClaimsRefresh = false;
            options.ClaimsRefreshInterval = TimeSpan.FromMinutes(15);
        });

        // Assert
        Assert.Same(services, result);
        
        // Build and verify configuration was applied
        var serviceProvider = services.BuildServiceProvider();
        var optionsMonitor = serviceProvider.GetService<IOptionsMonitor<PlanningCenterClaimsTransformationOptions>>();
        var options = optionsMonitor?.CurrentValue;
        
        Assert.True(configureWasCalled);
        Assert.NotNull(options);
        Assert.False(options.EnableClaimsRefresh);
        Assert.Equal(TimeSpan.FromMinutes(15), options.ClaimsRefreshInterval);
    }

    [Fact(DisplayName = "Claims transformation registers as transient")]
    public void ClaimsTransformation_RegistersAsTransient()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddHttpClient(); // Required for claims transformation

        // Act
        services.AddPlanningCenterClaimsTransformation();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var transformation1 = serviceProvider.GetService<IClaimsTransformation>();
        var transformation2 = serviceProvider.GetService<IClaimsTransformation>();
        
        Assert.NotNull(transformation1);
        Assert.NotNull(transformation2);
        // Verify they are different instances (transient)
        Assert.NotSame(transformation1, transformation2);
    }

    [Fact(DisplayName = "OAuth extensions work with full authentication setup")]
    public void OAuthExtensions_WorkWithFullAuthenticationSetup()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAuthentication()
               .AddPlanningCenterOAuth(options =>
               {
                   options.ClientId = "test-client-id";
                   options.ClientSecret = "test-client-secret";
               });

        services.AddHttpClient(); // Required for claims transformation
        services.AddPlanningCenterClaimsTransformation();

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        
        var authSchemeProvider = serviceProvider.GetService<IAuthenticationSchemeProvider>();
        var claimsTransformation = serviceProvider.GetService<IClaimsTransformation>();
        var oauthOptions = serviceProvider.GetService<IOptionsMonitor<PlanningCenterOAuthOptions>>();
        
        Assert.NotNull(authSchemeProvider);
        Assert.NotNull(claimsTransformation);
        Assert.NotNull(oauthOptions);
    }
}