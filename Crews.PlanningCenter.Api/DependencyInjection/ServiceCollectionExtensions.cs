using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Crews.PlanningCenter.Api.DependencyInjection;

/// <summary>
/// Extension methods useful for registering services with an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers all Planning Center product APIs with the given <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> with which to register the services.</param>
	/// <param name="configureOptions">Optionally configures the service with a lambda expression.</param>
	/// <returns>Returns the same <see cref="IServiceCollection"/>.</returns>
	public static IServiceCollection AddPlanningCenterClient(
		this IServiceCollection services, Action<PlanningCenterClientOptions>? configureOptions = null)
	{
		ServiceProvider provider;
		
		if (configureOptions == null)
		{
			provider = services.BuildServiceProvider();
			IConfiguration configuration = provider.GetService<IConfiguration>()
				?? throw new InvalidOperationException(
				"Unable to configure service: no application configuration exists in the service container, and no "
				+ $"configuration was provided via the {nameof(configureOptions)} argument.");

			services.Configure<PlanningCenterClientOptions>(
				configuration.GetSection(PlanningCenterClientOptions.ConfigurationName));
		}
		else
		{
			services.Configure(configureOptions);
		}

		provider = services.BuildServiceProvider();
		PlanningCenterClientOptions options = provider.GetRequiredService<IOptions<PlanningCenterClientOptions>>().Value;

		// Register HttpContextAccessor if not already registered
		if (!services.Any(x => x.ServiceType == typeof(IHttpContextAccessor)))
		{
			services.AddHttpContextAccessor();
		}

		// Register the authentication handler
		services.AddTransient<PlanningCenterAuthenticationHandler>();

		// Register HttpClient with the authentication handler
		services.AddHttpClient(options.HttpClientName)
			.AddHttpMessageHandler<PlanningCenterAuthenticationHandler>();

		return services.AddScoped<IPlanningCenterClient, PlanningCenterClient>();
	}
}
