using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Crews.PlanningCenter.Api.DependencyInjection;

/// <summary>
/// Extension methods useful for registering services with an <see cref="AuthenticationBuilder"/>.
/// </summary>
public static class AuthenticationBuilderExtensions
{
	/// <summary>
	/// Adds Planning Center OpenID Connect authentication to the application.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <returns>A reference to this instance after the operation has completed.</returns>
	public static AuthenticationBuilder AddPlanningCenter(this AuthenticationBuilder builder)
		=> builder.AddPlanningCenter(PlanningCenterAuthenticationDefaults.AuthenticationScheme, _ => { });

	/// <summary>
	/// Adds Planning Center OpenID Connect authentication to the application.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <param name="configureOptions">The action to configure the Planning Center OpenID Connect options.</param>
	/// <returns>A reference to this instance after the operation has completed.</returns>
	public static AuthenticationBuilder AddPlanningCenter(
		this AuthenticationBuilder builder, Action<OpenIdConnectOptions> configureOptions)
		=> builder.AddPlanningCenter(PlanningCenterAuthenticationDefaults.AuthenticationScheme, configureOptions);

	/// <summary>
	/// Adds Planning Center OpenID Connect authentication to the application.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <param name="authenticationScheme">The authentication scheme.</param>
	/// <param name="configureOptions">The action to configure the Planning Center OpenID Connect options.</param>
	/// <returns>A reference to this instance after the operation has completed.</returns>
	public static AuthenticationBuilder AddPlanningCenter(
		this AuthenticationBuilder builder, string authenticationScheme, Action<OpenIdConnectOptions> configureOptions)
		=> builder.AddPlanningCenter(
			authenticationScheme, PlanningCenterAuthenticationDefaults.AuthenticationScheme, configureOptions);

	/// <summary>
	/// Adds Planning Center OpenID Connect authentication to the application.
	/// </summary>
	/// <param name="builder">The authentication builder.</param>
	/// <param name="authenticationScheme">The authentication scheme.</param>
	/// <param name="displayName">The display name for the authentication scheme.</param>
	/// <param name="configureOptions">The action to configure the Planning Center OpenID Connect options.</param>
	/// <returns>A reference to this instance after the operation has completed.</returns>
	public static AuthenticationBuilder AddPlanningCenter(
		this AuthenticationBuilder builder,
		string authenticationScheme,
		string displayName,
		Action<OpenIdConnectOptions> configureOptions)
	{
		ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
		IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

		// Register claims transformation to map Planning Center claims to .NET conventions
		builder.Services.AddSingleton<IClaimsTransformation, PlanningCenterClaimsTransformation>();

		return builder.AddOpenIdConnect(authenticationScheme, displayName, options =>
		{
			options.Authority = PlanningCenterAuthenticationDefaults.Authority;

			options.Scope.Clear();
			options.Scope.Add(PlanningCenterOAuthScope.OpenId);

			options.Prompt = OpenIdConnectPrompt.SelectAccount;
			options.ResponseType = OpenIdConnectResponseType.Code;

			// Save tokens so they can be retrieved later for API calls
			options.SaveTokens = true;

			// Bind configuration and apply user customizations
			configuration.GetSection(PlanningCenterAuthenticationDefaults.ConfigurationSection).Bind(options);
			configureOptions(options);
		});
	}
}
