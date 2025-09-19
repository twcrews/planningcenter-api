using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Crews.PlanningCenter.Api.DependencyInjection;

/// <summary>
/// Extension methods useful for registering services with an <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionExtensions
{
	/// <summary>
	/// Registers all Planning Center product APIs with the given <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> with which to register the services.</param>
	/// <param name="configureOptions">Optionally configures the service with a lambda expression.</param>
	/// <returns>Returns the same <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection AddPlanningCenterApi(
		this IServiceCollection services, Action<PlanningCenterApiOptions>? configureOptions = null)
	{
		ServiceProvider provider;
		
		if (configureOptions == null)
		{
			provider = services.BuildServiceProvider();
			IConfiguration configuration = provider.GetService<IConfiguration>()
				?? throw new InvalidOperationException(
				"Unable to configure service: no application configuration exists in the service container, and no "
				+ $"configuration was provided via the {nameof(configureOptions)} argument.");

			services.Configure<PlanningCenterApiOptions>(
				configuration.GetSection(PlanningCenterApiOptions.ConfigurationName));
		}
		else
		{
			services.Configure(configureOptions);
		}

		provider = services.BuildServiceProvider();
		PlanningCenterApiOptions options = provider.GetRequiredService<IOptions<PlanningCenterApiOptions>>().Value;

		services.AddHttpClient(options.HttpClientName, (httpClient) =>
		{
			httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(options.UserAgent);
		});
		return services.AddScoped<IPlanningCenterApiService, PlanningCenterApiService>();
	}
}
