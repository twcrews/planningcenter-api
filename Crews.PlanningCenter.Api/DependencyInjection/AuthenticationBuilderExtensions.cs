using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.DependencyInjection;

/// <summary>
/// Extension methods useful for registering services with an <see cref="AuthenticationBuilder"/>.
/// </summary>
public static class AuthenticationBuilderExtensions
{
	/// <summary>
	/// Adds Planning Center OAuth authentication to the application.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <returns>A reference to this instance after the operation has completed.</returns>
	public static AuthenticationBuilder AddPlanningCenterOAuth(this AuthenticationBuilder builder)
		=> builder.AddPlanningCenterOAuth(PlanningCenterOAuthDefaults.AuthenticationScheme, _ => { });

	/// <summary>
	/// Adds Planning Center OAuth authentication to the application.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <param name="configureOptions">The action to configure the Planning Center OAuth options.</param>
	/// <returns>A reference to this instance after the operation has completed.</returns>
	public static AuthenticationBuilder AddPlanningCenterOAuth(
		this AuthenticationBuilder builder, Action<PlanningCenterOAuthOptions> configureOptions)
		=> builder.AddPlanningCenterOAuth(PlanningCenterOAuthDefaults.AuthenticationScheme, configureOptions);

	/// <summary>
	/// Adds Planning Center OAuth authentication to the application.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <param name="authenticationScheme">The authentication scheme.</param>
	/// <param name="configureOptions">The action to configure the Planning Center OAuth options.</param>
	/// <returns>A reference to this instance after the operation has completed.</returns>
	public static AuthenticationBuilder AddPlanningCenterOAuth(
		this AuthenticationBuilder builder, string authenticationScheme, Action<PlanningCenterOAuthOptions> configureOptions)
		=> builder.AddPlanningCenterOAuth(authenticationScheme, PlanningCenterOAuthDefaults.DisplayName, configureOptions);

	/// <summary>
	/// Adds Planning Center OAuth authentication to the application.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <param name="authenticationScheme">The authentication scheme.</param>
	/// <param name="displayName">The display name for the authentication scheme.</param>
	/// <param name="configureOptions">The action to configure the Planning Center OAuth options.</param>
	/// <returns>A reference to this instance after the operation has completed.</returns>
	public static AuthenticationBuilder AddPlanningCenterOAuth(
		this AuthenticationBuilder builder,
		string authenticationScheme,
		string displayName,
		Action<PlanningCenterOAuthOptions> configureOptions)
	{
		ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
		IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

		// Register claims transformation services
		builder.Services.Configure<PlanningCenterClaimsTransformationOptions>(
			configuration.GetSection("Authentication:PlanningCenter:ClaimsTransformation"));
		builder.Services.AddTransient<IClaimsTransformation, PlanningCenterClaimsTransformation>();

		return builder.AddOAuth<PlanningCenterOAuthOptions, PlanningCenterOAuthHandler>(
			authenticationScheme, displayName, options =>
		{
			configuration.GetSection("Authentication:PlanningCenter").Bind(options);
			configureOptions(options);
		});
	}
}
