using System.IdentityModel.Tokens.Jwt;
using Crews.PlanningCenter.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Crews.PlanningCenter.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring OIDC authentication.
/// </summary>
public static class AuthenticationBuilderExtensions
{
    /// <summary>
    /// Adds Planning Center OIDC authentication with default configuration.
    /// Uses cookies as the sign-in scheme by default.
    /// </summary>
    public static AuthenticationBuilder AddPlanningCenterAuthentication(this AuthenticationBuilder builder)
        => builder.AddPlanningCenterAuthentication(signInScheme: null, configureOptions: null);

    /// <summary>
    /// Adds Planning Center OIDC authentication with a custom sign-in scheme.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="signInScheme">The scheme to use for signing in (defaults to cookies if null).</param>
    public static AuthenticationBuilder AddPlanningCenterAuthentication(
        this AuthenticationBuilder builder,
        string? signInScheme)
        => builder.AddPlanningCenterAuthentication(signInScheme, configureOptions: null);

    /// <summary>
    /// Adds Planning Center OIDC authentication with full customization options.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="signInScheme">The scheme to use for signing in (defaults to cookies if null).</param>
    /// <param name="configureOptions">Optional callback to customize OpenID Connect options.</param>
    public static AuthenticationBuilder AddPlanningCenterAuthentication(
        this AuthenticationBuilder builder,
        string? signInScheme,
        Action<Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectOptions>? configureOptions)
        => builder.AddOpenIdConnect(PlanningCenterAuthenticationDefaults.AuthenticationScheme, options =>
        {
            ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

            IConfigurationSection oidcConfig = configuration
                .GetSection(PlanningCenterAuthenticationDefaults.ConfigurationSection);

            // Configure defaults
            options.Authority = oidcConfig["Authority"] ?? PlanningCenterAuthenticationDefaults.BaseUrl;
            options.ClientId = oidcConfig["ClientId"];
            options.ClientSecret = oidcConfig["ClientSecret"];

            options.SignInScheme = signInScheme
                ?? Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            options.ResponseType = OpenIdConnectResponseType.Code;

            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;

            options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
            options.Prompt = PlanningCenterAuthenticationDefaults.RecommendedPrompt;

            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("people");

            // Allow consumer to override any options
            configureOptions?.Invoke(options);
        });
}
