using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

/// <summary>
/// Shared fixture for Planning Center client across multiple test classes.
/// This reduces the number of client instances created and improves test performance.
/// </summary>
public class PlanningCenterClientFixture : IDisposable
{
	public IPlanningCenterClient Client { get; }
	public IConfiguration Configuration { get; }
	public ServiceProvider ServiceProvider { get; }

	public PlanningCenterClientFixture()
	{
		// Build configuration from user secrets and environment variables
		Configuration = new ConfigurationBuilder()
			.AddUserSecrets<IntegrationTestBase>()
			.AddEnvironmentVariables(prefix: "PLANNINGCENTER_")
			.Build();

		// Build service collection
		var services = new ServiceCollection();
		services.AddSingleton(Configuration);

		// Add Planning Center client
		services.AddPlanningCenterClient(options =>
		{
			// Load from configuration
			var appId = Configuration["PlanningCenterClient:PersonalAccessToken:AppId"]
			            ?? Configuration["AppId"];
			var secret = Configuration["PlanningCenterClient:PersonalAccessToken:Secret"]
			             ?? Configuration["Secret"];

			if (!string.IsNullOrEmpty(appId) && !string.IsNullOrEmpty(secret))
			{
				options.PersonalAccessToken = new PlanningCenterPersonalAccessToken
				{
					AppId = appId,
					Secret = secret
				};
			}

			options.UserAgent = "Crews.PlanningCenter.Api Integration Tests";
		});

		ServiceProvider = services.BuildServiceProvider();
		Client = ServiceProvider.GetRequiredService<IPlanningCenterClient>();
	}

	/// <summary>
	/// Checks if credentials are configured.
	/// </summary>
	public bool HasCredentials()
	{
		var appId = Configuration["PlanningCenterClient:PersonalAccessToken:AppId"]
		            ?? Configuration["AppId"];
		var secret = Configuration["PlanningCenterClient:PersonalAccessToken:Secret"]
		             ?? Configuration["Secret"];

		return !string.IsNullOrEmpty(appId) && !string.IsNullOrEmpty(secret);
	}

	public void Dispose()
	{
		ServiceProvider?.Dispose();
		GC.SuppressFinalize(this);
	}
}
