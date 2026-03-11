using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Extensions;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class ConfigurePlanningCenterOpenIdConnectOptionsTests
{
	private static IOptionsFactory<OpenIdConnectOptions> CreateFactory(
		string? clientId = "test-client-id",
		string? clientSecret = "test-client-secret",
		string? authority = null,
		string[]? scopes = null)
	{
		var services = new ServiceCollection();
		var config = new Dictionary<string, string?>();

		string section = PlanningCenterAuthenticationDefaults.ConfigurationSection;
		if (clientId is not null) config[$"{section}:ClientId"] = clientId;
		if (clientSecret is not null) config[$"{section}:ClientSecret"] = clientSecret;
		if (authority is not null) config[$"{section}:Authority"] = authority;
		if (scopes is not null)
		{
			for (int i = 0; i < scopes.Length; i++)
				config[$"{section}:Scopes:{i}"] = scopes[i];
		}

		services.AddSingleton<IConfiguration>(
			new ConfigurationBuilder().AddInMemoryCollection(config).Build());
		services.AddAuthentication().AddPlanningCenterAuthentication();

		return services.BuildServiceProvider().GetRequiredService<IOptionsFactory<OpenIdConnectOptions>>();
	}

	private static OpenIdConnectOptions Configure(
		string? clientId = "test-client-id",
		string? clientSecret = "test-client-secret",
		string? authority = null,
		string[]? scopes = null)
		=> CreateFactory(clientId, clientSecret, authority, scopes)
			.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme);

	[Fact(DisplayName = "Missing ClientId throws InvalidOperationException")]
	public void Configure_MissingClientId_ThrowsInvalidOperationException()
	{
		var factory = CreateFactory(clientId: null);

		Assert.Throws<InvalidOperationException>(() =>
			factory.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme));
	}

	[Fact(DisplayName = "Empty ClientId throws InvalidOperationException")]
	public void Configure_EmptyClientId_ThrowsInvalidOperationException()
	{
		var factory = CreateFactory(clientId: "");

		Assert.Throws<InvalidOperationException>(() =>
			factory.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme));
	}

	[Fact(DisplayName = "Missing ClientSecret throws InvalidOperationException")]
	public void Configure_MissingClientSecret_ThrowsInvalidOperationException()
	{
		var factory = CreateFactory(clientSecret: null);

		Assert.Throws<InvalidOperationException>(() =>
			factory.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme));
	}

	[Fact(DisplayName = "Empty ClientSecret throws InvalidOperationException")]
	public void Configure_EmptyClientSecret_ThrowsInvalidOperationException()
	{
		var factory = CreateFactory(clientSecret: "");

		Assert.Throws<InvalidOperationException>(() =>
			factory.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme));
	}

	[Fact(DisplayName = "ClientId is set from configuration")]
	public void Configure_SetsClientIdFromConfiguration()
	{
		var options = Configure(clientId: "my-client-id");

		Assert.Equal("my-client-id", options.ClientId);
	}

	[Fact(DisplayName = "ClientSecret is set from configuration")]
	public void Configure_SetsClientSecretFromConfiguration()
	{
		var options = Configure(clientSecret: "my-client-secret");

		Assert.Equal("my-client-secret", options.ClientSecret);
	}

	[Fact(DisplayName = "Authority defaults to PlanningCenter base URL when not configured")]
	public void Configure_UsesDefaultAuthorityWhenNotConfigured()
	{
		var options = Configure(authority: null);

		Assert.Equal(PlanningCenterAuthenticationDefaults.BaseUrl, options.Authority);
	}

	[Fact(DisplayName = "Authority is set from configuration when provided")]
	public void Configure_SetsAuthorityFromConfiguration()
	{
		const string authority = "https://custom.authority.example.com";

		var options = Configure(authority: authority);

		Assert.Equal(authority, options.Authority);
	}

	[Fact(DisplayName = "ResponseType is set to authorization code flow")]
	public void Configure_SetsResponseTypeToCode()
	{
		var options = Configure();

		Assert.Equal(OpenIdConnectResponseType.Code, options.ResponseType);
	}

	[Fact(DisplayName = "SaveTokens is enabled")]
	public void Configure_SetsSaveTokensToTrue()
	{
		var options = Configure();

		Assert.True(options.SaveTokens);
	}

	[Fact(DisplayName = "GetClaimsFromUserInfoEndpoint is enabled")]
	public void Configure_SetsGetClaimsFromUserInfoEndpointToTrue()
	{
		var options = Configure();

		Assert.True(options.GetClaimsFromUserInfoEndpoint);
	}

	[Fact(DisplayName = "Prompt is set to the recommended select_account value")]
	public void Configure_SetsPromptToSelectAccount()
	{
		var options = Configure();

		Assert.Equal(PlanningCenterAuthenticationDefaults.RecommendedPrompt, options.Prompt);
	}

	[Fact(DisplayName = "Default scopes (openid, people) are used when no scopes are configured")]
	public void Configure_UsesDefaultScopesWhenNotConfigured()
	{
		var options = Configure(scopes: null);

		Assert.Contains("openid", options.Scope);
		Assert.Contains("people", options.Scope);
	}

	[Fact(DisplayName = "Default scopes are used when an empty scopes array is configured")]
	public void Configure_UsesDefaultScopesWhenConfiguredScopesIsEmpty()
	{
		var options = Configure(scopes: []);

		Assert.Contains("openid", options.Scope);
		Assert.Contains("people", options.Scope);
	}

	[Fact(DisplayName = "Scopes from configuration are applied")]
	public void Configure_UsesConfiguredScopes()
	{
		var options = Configure(scopes: ["openid", "groups", "services"]);

		Assert.Contains("openid", options.Scope);
		Assert.Contains("groups", options.Scope);
		Assert.Contains("services", options.Scope);
	}

	[Fact(DisplayName = "Default scopes are cleared when custom scopes are configured")]
	public void Configure_ClearsDefaultScopesWhenCustomScopesConfigured()
	{
		var options = Configure(scopes: ["openid", "groups"]);

		Assert.DoesNotContain("people", options.Scope);
	}

	[Fact(DisplayName = "Blank entries in configured scopes are filtered out")]
	public void Configure_FiltersBlankScopesFromConfiguration()
	{
		var options = Configure(scopes: ["openid", "", "  ", "groups"]);

		Assert.DoesNotContain("", options.Scope);
		Assert.DoesNotContain("  ", options.Scope);
		Assert.Contains("groups", options.Scope);
	}

	[Fact(DisplayName = "Nameless Configure overload does not apply PlanningCenter configuration")]
	public void Configure_NamelessOverload_DoesNotApplyConfiguration()
	{
		IConfiguration configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string?>
			{
				[$"{PlanningCenterAuthenticationDefaults.ConfigurationSection}:ClientId"] = "test-client-id",
				[$"{PlanningCenterAuthenticationDefaults.ConfigurationSection}:ClientSecret"] = "test-client-secret",
			})
			.Build();

		var configurator = new ConfigurePlanningCenterOpenIdConnectOptions(configuration);
		var options = new OpenIdConnectOptions();
		configurator.Configure(options);

		Assert.Null(options.ClientId);
		Assert.Null(options.ClientSecret);
	}

	[Fact(DisplayName = "Configuration is not applied to schemes other than PlanningCenter")]
	public void Configure_IgnoresNonPlanningCenterScheme()
	{
		// Configure() calls Create() with PlanningCenterAuthenticationDefaults.AuthenticationScheme
		// Here we call Create() with a different scheme to ensure our configurator is a no-op for it
		var factory = CreateFactory();
		var options = factory.Create("SomeOtherScheme");

		Assert.Null(options.ClientId);
		Assert.Null(options.ClientSecret);
	}
}
