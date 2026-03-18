using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Crews.PlanningCenter.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring OIDC authentication.
/// </summary>
public static class AuthenticationBuilderExtensions
{
    /// <summary>
    /// Adds Planning Center OIDC authentication with configuration from appsettings.json.
    /// Reads from the "PlanningCenter" configuration section.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <returns>The authentication builder for chaining.</returns>
    /// <remarks>
    /// <para>Configuration is read from appsettings.json under the "PlanningCenter" section:</para>
    /// <code>
    /// {
    ///   "PlanningCenter": {
    ///     "Authority": "https://api.planningcenteronline.com",
    ///     "ClientId": "your-client-id",
    ///     "ClientSecret": "your-client-secret",
    ///     "Scopes": ["openid", "people"]  // Optional, defaults to ["openid", "people"]
    ///   }
    /// }
    /// </code>
    /// <para>ClientId and ClientSecret are required. If not configured, an exception will be thrown at startup.</para>
    /// <para>Note: You must also configure cookie authentication and set the DefaultSignInScheme in AddAuthentication().</para>
    /// </remarks>
    public static AuthenticationBuilder AddPlanningCenterAuthentication(
        this AuthenticationBuilder builder)
        => builder.AddPlanningCenterAuthentication(signInScheme: null, configureOptions: null);

    /// <summary>
    /// Adds Planning Center OIDC authentication with a custom sign-in scheme.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="signInScheme">The scheme to use for signing in. If null, uses the DefaultSignInScheme from AddAuthentication().</param>
    public static AuthenticationBuilder AddPlanningCenterAuthentication(
        this AuthenticationBuilder builder,
        string? signInScheme)
        => builder.AddPlanningCenterAuthentication(signInScheme, configureOptions: null);

    /// <summary>
    /// Adds Planning Center OIDC authentication with custom configuration options.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="configureOptions">Callback to customize OpenID Connect options.</param>
    public static AuthenticationBuilder AddPlanningCenterAuthentication(
        this AuthenticationBuilder builder,
        Action<OpenIdConnectOptions> configureOptions)
        => builder.AddPlanningCenterAuthentication(signInScheme: null, configureOptions);

    /// <summary>
    /// Adds Planning Center OIDC authentication with full customization options.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="signInScheme">The scheme to use for signing in. If null, uses the DefaultSignInScheme from AddAuthentication().</param>
    /// <param name="configureOptions">Optional callback to customize OpenID Connect options.</param>
    public static AuthenticationBuilder AddPlanningCenterAuthentication(
        this AuthenticationBuilder builder,
        string? signInScheme,
        Action<OpenIdConnectOptions>? configureOptions)
    {
        // Register the configuration options provider that will read from appsettings
        // Use TryAdd to prevent duplicate registrations if called multiple times
        builder.Services.TryAddSingleton<IConfigureOptions<OpenIdConnectOptions>,
            ConfigurePlanningCenterOpenIdConnectOptions>();

        // Register the OpenID Connect authentication scheme
        builder.AddOpenIdConnect(PlanningCenterAuthenticationDefaults.AuthenticationScheme, options =>
        {
            // Set sign-in scheme if explicitly provided
            // Otherwise, uses the DefaultSignInScheme from AddAuthentication()
            if (signInScheme is not null)
            {
                options.SignInScheme = signInScheme;
            }

            // Allow consumer to override any options
            // Note: This runs after ConfigurePlanningCenterOpenIdConnectOptions
            configureOptions?.Invoke(options);
        });

        return builder;
    }
}
