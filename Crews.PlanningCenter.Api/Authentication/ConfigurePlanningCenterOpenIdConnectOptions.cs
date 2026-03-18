using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Configures Planning Center OpenID Connect options from configuration.
/// </summary>
internal sealed class ConfigurePlanningCenterOpenIdConnectOptions(IConfiguration configuration) : IConfigureNamedOptions<OpenIdConnectOptions>
{
    private readonly IConfiguration _configuration = configuration;

    public void Configure(string? name, OpenIdConnectOptions options)
    {
        // Only configure our specific scheme
        if (name != PlanningCenterAuthenticationDefaults.AuthenticationScheme)
            return;

        IConfigurationSection config = _configuration
            .GetSection(PlanningCenterAuthenticationDefaults.ConfigurationSection);

        // Bind configuration values
        options.Authority = config["Authority"] ?? PlanningCenterAuthenticationDefaults.BaseUrl;
        options.ClientId = config["ClientId"];
        options.ClientSecret = config["ClientSecret"];

        // Validate required configuration
        if (string.IsNullOrEmpty(options.ClientId))
        {
            throw new InvalidOperationException(
                $"ClientId is required. Configure it in appsettings.json under " +
                $"'{PlanningCenterAuthenticationDefaults.ConfigurationSection}:ClientId' or " +
                $"set it manually via the configuration callback.");
        }

        if (string.IsNullOrEmpty(options.ClientSecret))
        {
            throw new InvalidOperationException(
                $"ClientSecret is required. Configure it in appsettings.json under " +
                $"'{PlanningCenterAuthenticationDefaults.ConfigurationSection}:ClientSecret' or " +
                $"set it manually via the configuration callback.");
        }

        // Set defaults
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
        options.Prompt = PlanningCenterAuthenticationDefaults.RecommendedPrompt;

        // Configure scopes
        options.Scope.Clear();

        // Read scopes from configuration, or use defaults
        string[]? configuredScopes = config.GetSection("Scopes").Get<string[]>();
        if (configuredScopes is { Length: > 0 })
        {
            foreach (string scope in configuredScopes)
            {
                if (!string.IsNullOrWhiteSpace(scope))
                {
                    options.Scope.Add(scope);
                }
            }
        }
        else
        {
            // Default scopes
            options.Scope.Add("openid");
            options.Scope.Add("people");
        }
    }

    public void Configure(OpenIdConnectOptions options) => Configure(string.Empty, options);
}
