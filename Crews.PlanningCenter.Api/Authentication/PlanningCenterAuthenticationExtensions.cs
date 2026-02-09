using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Extension methods for adding Planning Center authentication to an ASP.NET Core application.
/// </summary>
public static class PlanningCenterAuthenticationExtensions
{
	/// <summary>
	/// Adds Planning Center OIDC authentication to the service collection.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="configure">A delegate to configure the <see cref="PlanningCenterOidcOptions"/>.</param>
	/// <returns>The service collection for method chaining.</returns>
	public static IServiceCollection AddPlanningCenterAuthentication(
		this IServiceCollection services,
		Action<PlanningCenterOidcOptions> configure)
	{
		ArgumentNullException.ThrowIfNull(services);
		ArgumentNullException.ThrowIfNull(configure);

		var options = new PlanningCenterOidcOptions
		{
			ClientId = string.Empty,
			ClientSecret = string.Empty
		};
		configure(options);

		// Register claims transformation
		services.TryAddEnumerable(
			ServiceDescriptor.Transient<IClaimsTransformation, PlanningCenterClaimsTransformation>());

		// Add authentication with cookie and OIDC schemes
		services.AddAuthentication(authOptions =>
			{
				authOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				authOptions.DefaultChallengeScheme = PlanningCenterAuthenticationDefaults.AuthenticationScheme;
			})
			.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddPlanningCenterOidc(options);

		return services;
	}

	/// <summary>
	/// Adds Planning Center OIDC authentication to an existing authentication builder.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <param name="configure">A delegate to configure the <see cref="PlanningCenterOidcOptions"/>.</param>
	/// <returns>The authentication builder for method chaining.</returns>
	public static AuthenticationBuilder AddPlanningCenterOidc(
		this AuthenticationBuilder builder,
		Action<PlanningCenterOidcOptions> configure)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(configure);

		var options = new PlanningCenterOidcOptions
		{
			ClientId = string.Empty,
			ClientSecret = string.Empty
		};
		configure(options);

		return builder.AddPlanningCenterOidc(options);
	}

	/// <summary>
	/// Adds Planning Center OIDC authentication to an existing authentication builder.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <param name="options">The Planning Center OIDC options.</param>
	/// <returns>The authentication builder for method chaining.</returns>
	public static AuthenticationBuilder AddPlanningCenterOidc(
		this AuthenticationBuilder builder,
		PlanningCenterOidcOptions options)
	{
		ArgumentNullException.ThrowIfNull(builder);
		ArgumentNullException.ThrowIfNull(options);

		// Register claims transformation
		builder.Services.TryAddEnumerable(
			ServiceDescriptor.Transient<IClaimsTransformation, PlanningCenterClaimsTransformation>());

		return builder.AddOpenIdConnect(
			PlanningCenterAuthenticationDefaults.AuthenticationScheme,
			PlanningCenterAuthenticationDefaults.DisplayName,
			oidcOptions => options.ApplyTo(oidcOptions));
	}

}
