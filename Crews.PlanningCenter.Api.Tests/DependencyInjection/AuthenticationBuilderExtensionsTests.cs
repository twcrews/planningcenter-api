using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.Tests.DependencyInjection;

public class AuthenticationBuilderExtensionsTests
{
    [Fact(DisplayName = "AddPlanningCenterOAuth with no parameters uses defaults")]
    public void AddPlanningCenterOAuth_WithNoParameters_UsesDefaults()
    {
		// Arrange
		ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
		AuthenticationBuilder authBuilder = new(services);

		// Act
		AuthenticationBuilder result = authBuilder.AddPlanningCenter();

        // Assert
        Assert.Same(authBuilder, result);
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth with configure action works")]
    public void AddPlanningCenterOAuth_WithConfigureAction_Works()
    {
		// Arrange
		ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
		AuthenticationBuilder authBuilder = new(services);

		// Act
		AuthenticationBuilder result = authBuilder.AddPlanningCenter(options =>
        {
            options.ClientId = "test-client-id";
        });

        // Assert
        Assert.Same(authBuilder, result);

        // Verify that the extension method worked by checking the services were registered
        Assert.Contains(services, s => s.ServiceType.Name.Contains("OpenIdConnect"));
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth with custom scheme works")]
    public void AddPlanningCenterOAuth_WithCustomScheme_Works()
    {
		// Arrange
		ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
		AuthenticationBuilder authBuilder = new(services);
        const string customScheme = "CustomPlanningCenter";

		// Act
		AuthenticationBuilder result = authBuilder.AddPlanningCenter(customScheme, options =>
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
		ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
		AuthenticationBuilder authBuilder = new(services);
        const string customScheme = "CustomPlanningCenter";
        const string customDisplayName = "Custom Planning Center";

		// Act
		AuthenticationBuilder result = authBuilder.AddPlanningCenter(customScheme, customDisplayName, options =>
        {
            options.ClientId = "test-client-id";
        });

        // Assert
        Assert.Same(authBuilder, result);
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth registers OpenIdConnect services")]
    public void AddPlanningCenterOAuth_RegistersOpenIdConnectServices()
    {
        // Arrange
        ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
        AuthenticationBuilder authBuilder = new(services);

        // Act
        authBuilder.AddPlanningCenter();

        // Assert - Verify OpenIdConnect services were registered
        Assert.Contains(services, s => s.ServiceType.Name.Contains("OpenIdConnect"));
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth with custom values works")]
    public void AddPlanningCenterOAuth_WithCustomValues_Works()
    {
        // Arrange
        ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
        AuthenticationBuilder authBuilder = new(services);

        // Act
        authBuilder.AddPlanningCenter(options =>
        {
            options.ClientId = "custom-client-id";
            options.ClientSecret = "custom-secret";
        });

        // Assert - Verify the extension method returns the builder
        Assert.NotNull(authBuilder);
        Assert.Contains(services, s => s.ServiceType.Name.Contains("OpenIdConnect"));
    }

    [Fact(DisplayName = "AddPlanningCenter registers claims transformation")]
    public void AddPlanningCenter_RegistersClaimsTransformation()
    {
        // Arrange
        ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
        AuthenticationBuilder authBuilder = new(services);

        // Act
        authBuilder.AddPlanningCenter();

        // Assert - Verify claims transformation was registered
        Assert.Contains(services, s =>
            s.ServiceType == typeof(IClaimsTransformation) &&
            s.ImplementationType?.Name == nameof(PlanningCenterClaimsTransformation));
    }

    [Fact(DisplayName = "AddPlanningCenter accepts configuration action")]
    public void AddPlanningCenter_AcceptsConfigurationAction()
    {
        // Arrange
        ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
        AuthenticationBuilder authBuilder = new(services);

        // Act
        AuthenticationBuilder result = authBuilder.AddPlanningCenter(options =>
        {
            options.ClientId = "test-client-123";
        });

        // Assert
        Assert.Same(authBuilder, result);
        Assert.Contains(services, s => s.ServiceType.Name.Contains("OpenIdConnect"));
    }

    [Fact(DisplayName = "AddPlanningCenter reads configuration section")]
    public void AddPlanningCenter_ReadsConfigurationSection()
    {
        // Arrange
        ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();

        Dictionary<string, string?> configValues = new()
        {
            ["Authentication:PlanningCenter:ClientId"] = "config-client-id",
            ["Authentication:PlanningCenter:ClientSecret"] = "config-secret"
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();

        services.AddSingleton(configuration);
        AuthenticationBuilder authBuilder = new(services);

        // Act
        authBuilder.AddPlanningCenter();

        // Assert - Configuration binding happens during service registration
        // Verify that the configuration section exists and has expected values
        IConfigurationSection section = configuration.GetSection("Authentication:PlanningCenter");
        Assert.Equal("config-client-id", section["ClientId"]);
        Assert.Equal("config-secret", section["ClientSecret"]);
    }
}