using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
		AuthenticationBuilder result = authBuilder.AddPlanningCenterOAuth();

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
		AuthenticationBuilder result = authBuilder.AddPlanningCenterOAuth(options =>
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
		ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
		AuthenticationBuilder authBuilder = new(services);
        const string customScheme = "CustomPlanningCenter";

		// Act
		AuthenticationBuilder result = authBuilder.AddPlanningCenterOAuth(customScheme, options =>
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
		AuthenticationBuilder result = authBuilder.AddPlanningCenterOAuth(customScheme, customDisplayName, options =>
        {
            options.ClientId = "test-client-id";
        });

        // Assert
        Assert.Same(authBuilder, result);
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth uses configuration values by default")]
    public void AddPlanningCenterOAuth_UsesConfigurationValuesByDefault()
    {
        // Arrange
        var configurationData = new Dictionary<string, string?>
        {
            ["Authentication:PlanningCenter:ClientId"] = "config-client-id",
            ["Authentication:PlanningCenter:ClientSecret"] = "config-client-secret"
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configurationData)
            .Build();

        ServiceCollection services = new();
        services.AddLogging();
        services.AddDataProtection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton(TimeProvider.System);
        AuthenticationBuilder authBuilder = new(services);

        PlanningCenterOAuthOptions? capturedOptions = null;

        // Act
        authBuilder.AddPlanningCenterOAuth(options =>
        {
            capturedOptions = options;
        });

        var serviceProvider = services.BuildServiceProvider();

        // Trigger options resolution to execute the callback
        var optionsMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<PlanningCenterOAuthOptions>>();
        _ = optionsMonitor.Get(PlanningCenterOAuthDefaults.AuthenticationScheme);

        // Assert
        Assert.NotNull(capturedOptions);
        Assert.Equal("config-client-id", capturedOptions.ClientId);
        Assert.Equal("config-client-secret", capturedOptions.ClientSecret);
    }

    [Fact(DisplayName = "AddPlanningCenterOAuth allows user values to override configuration")]
    public void AddPlanningCenterOAuth_AllowsUserValuesToOverrideConfiguration()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Services.AddLogging();
        builder.Services.AddDataProtection();

        PlanningCenterOAuthOptions? capturedOptions = null;

        // Act
        builder.Services.AddAuthentication().AddPlanningCenterOAuth(options =>
        {
            capturedOptions = options;
            options.ClientId = "user-override-client-id";
            options.ClientSecret = "user-override-client-secret";
        });

        var serviceProvider = builder.Services.BuildServiceProvider();

        // Trigger options resolution to execute the callback
        var optionsMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<PlanningCenterOAuthOptions>>();
        _ = optionsMonitor.Get(PlanningCenterOAuthDefaults.AuthenticationScheme);

        // Assert
        Assert.NotNull(capturedOptions);
        Assert.Equal("user-override-client-id", capturedOptions.ClientId);
        Assert.Equal("user-override-client-secret", capturedOptions.ClientSecret);
    }
}