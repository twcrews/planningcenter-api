using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Clients;
using Crews.PlanningCenter.Api.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

/// <summary>
/// Base class for integration tests providing configuration and DI setup.
/// </summary>
public abstract class IntegrationTestBase : IDisposable
{
	protected ServiceProvider ServiceProvider { get; }
	protected IConfiguration Configuration { get; }
	protected IPlanningCenterClient PlanningCenterClient { get; }

	protected IntegrationTestBase()
	{
		// Build configuration from user secrets and environment variables
		Configuration = new ConfigurationBuilder()
			.AddUserSecrets<IntegrationTestBase>()
			.AddEnvironmentVariables(prefix: "PLANNINGCENTER_")
			.Build();

		// Build service collection with Planning Center client
		var services = new ServiceCollection();
		ConfigureServices(services);

		ServiceProvider = services.BuildServiceProvider();
		PlanningCenterClient = ServiceProvider.GetRequiredService<IPlanningCenterClient>();
	}

	/// <summary>
	/// Configures services for the test. Override to add custom services.
	/// </summary>
	protected virtual void ConfigureServices(IServiceCollection services)
	{
		services.AddSingleton(Configuration);

		// Add Planning Center client with configuration
		services.AddPlanningCenterClient(options =>
		{
			// Load from configuration if available
			var appId = Configuration["PlanningCenterClient:PersonalAccessToken:AppId"];
			var secret = Configuration["PlanningCenterClient:PersonalAccessToken:Secret"];

			if (!string.IsNullOrEmpty(appId) && !string.IsNullOrEmpty(secret))
			{
				options.PersonalAccessToken = new PlanningCenterPersonalAccessToken
				{
					AppId = appId,
					Secret = secret
				};
			}

			// Allow override from environment variables
			var envAppId = Configuration["AppId"];
			var envSecret = Configuration["Secret"];
			if (!string.IsNullOrEmpty(envAppId) && !string.IsNullOrEmpty(envSecret))
			{
				options.PersonalAccessToken = new PlanningCenterPersonalAccessToken
				{
					AppId = envAppId,
					Secret = envSecret
				};
			}

			// Set user agent
			options.UserAgent = "Crews.PlanningCenter.Api Integration Tests";
		});
	}

	/// <summary>
	/// Checks if credentials are configured.
	/// </summary>
	protected bool HasCredentials()
	{
		var appId = Configuration["PlanningCenterClient:PersonalAccessToken:AppId"]
		            ?? Configuration["AppId"];
		var secret = Configuration["PlanningCenterClient:PersonalAccessToken:Secret"]
		             ?? Configuration["Secret"];

		return !string.IsNullOrEmpty(appId) && !string.IsNullOrEmpty(secret);
	}

	public virtual void Dispose()
	{
		ServiceProvider?.Dispose();
		GC.SuppressFinalize(this);
	}
}
