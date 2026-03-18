using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class AuthenticationBuilderExtensionsTests
{
	private static ServiceCollection CreateServices(
		string? clientId = "test-id",
		string? clientSecret = "test-secret")
	{
		var services = new ServiceCollection();
		var config = new Dictionary<string, string?>();

		string section = PlanningCenterAuthenticationDefaults.ConfigurationSection;
		if (clientId is not null) config[$"{section}:ClientId"] = clientId;
		if (clientSecret is not null) config[$"{section}:ClientSecret"] = clientSecret;

		services.AddSingleton<IConfiguration>(
			new ConfigurationBuilder().AddInMemoryCollection(config).Build());

		return services;
	}

	[Fact(DisplayName = "AddPlanningCenterAuthentication() returns the builder for chaining")]
	public void AddPlanningCenterAuthentication_ReturnsBuilder()
	{
		var services = CreateServices();
		var builder = services.AddAuthentication();

		var result = builder.AddPlanningCenterAuthentication();

		Assert.Same(builder, result);
	}

	[Fact(DisplayName = "AddPlanningCenterAuthentication(signInScheme) returns the builder for chaining")]
	public void AddPlanningCenterAuthentication_WithSignInScheme_ReturnsBuilder()
	{
		var services = CreateServices();
		var builder = services.AddAuthentication();

		var result = builder.AddPlanningCenterAuthentication("TestScheme");

		Assert.Same(builder, result);
	}

	[Fact(DisplayName = "AddPlanningCenterAuthentication(configureOptions) returns the builder for chaining")]
	public void AddPlanningCenterAuthentication_WithConfigureOptions_ReturnsBuilder()
	{
		var services = CreateServices();
		var builder = services.AddAuthentication();

		var result = builder.AddPlanningCenterAuthentication(_ => { });

		Assert.Same(builder, result);
	}

	[Fact(DisplayName = "AddPlanningCenterAuthentication(signInScheme, configureOptions) returns the builder for chaining")]
	public void AddPlanningCenterAuthentication_WithSignInSchemeAndConfigureOptions_ReturnsBuilder()
	{
		var services = CreateServices();
		var builder = services.AddAuthentication();

		var result = builder.AddPlanningCenterAuthentication("TestScheme", _ => { });

		Assert.Same(builder, result);
	}

	[Fact(DisplayName = "Registers the PlanningCenter OpenIdConnect scheme")]
	public async Task AddPlanningCenterAuthentication_RegistersPlanningCenterScheme()
	{
		var services = CreateServices();
		services.AddAuthentication().AddPlanningCenterAuthentication();
		var provider = services.BuildServiceProvider();

		var schemeProvider = provider.GetRequiredService<IAuthenticationSchemeProvider>();
		var scheme = await schemeProvider.GetSchemeAsync(PlanningCenterAuthenticationDefaults.AuthenticationScheme);

		Assert.NotNull(scheme);
		Assert.Equal(PlanningCenterAuthenticationDefaults.AuthenticationScheme, scheme.Name);
	}

	[Fact(DisplayName = "Provided sign-in scheme is applied to the OIDC options")]
	public void AddPlanningCenterAuthentication_WithSignInScheme_SetsSignInScheme()
	{
		const string signInScheme = "TestCookieScheme";
		var services = CreateServices();
		services.AddAuthentication().AddPlanningCenterAuthentication(signInScheme);
		var provider = services.BuildServiceProvider();

		var factory = provider.GetRequiredService<IOptionsFactory<OpenIdConnectOptions>>();
		var options = factory.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme);

		Assert.Equal(signInScheme, options.SignInScheme);
	}

	[Fact(DisplayName = "Null sign-in scheme does not override the DefaultSignInScheme")]
	public void AddPlanningCenterAuthentication_WithNullSignInScheme_FallsBackToDefaultSignInScheme()
	{
		const string defaultSignInScheme = "DefaultCookieScheme";
		var services = CreateServices();
		services.AddAuthentication(o => o.DefaultSignInScheme = defaultSignInScheme)
			.AddPlanningCenterAuthentication(signInScheme: null);
		var provider = services.BuildServiceProvider();

		var factory = provider.GetRequiredService<IOptionsFactory<OpenIdConnectOptions>>();
		var options = factory.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme);

		Assert.Equal(defaultSignInScheme, options.SignInScheme);
	}

	[Fact(DisplayName = "The configureOptions callback is invoked when options are created")]
	public void AddPlanningCenterAuthentication_WithConfigureOptions_InvokesCallback()
	{
		bool callbackInvoked = false;
		var services = CreateServices();
		services.AddAuthentication().AddPlanningCenterAuthentication(_ => { callbackInvoked = true; });
		var provider = services.BuildServiceProvider();

		provider.GetRequiredService<IOptionsFactory<OpenIdConnectOptions>>()
			.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme);

		Assert.True(callbackInvoked);
	}

	[Fact(DisplayName = "Options set in the configureOptions callback are applied")]
	public void AddPlanningCenterAuthentication_WithConfigureOptions_AppliesOptionsFromCallback()
	{
		const string callbackPath = "/my-callback";
		var services = CreateServices();
		services.AddAuthentication().AddPlanningCenterAuthentication(o => o.CallbackPath = callbackPath);
		var provider = services.BuildServiceProvider();

		var options = provider.GetRequiredService<IOptionsFactory<OpenIdConnectOptions>>()
			.Create(PlanningCenterAuthenticationDefaults.AuthenticationScheme);

		Assert.Equal(callbackPath, options.CallbackPath);
	}

	[Fact(DisplayName = "Calling AddPlanningCenterAuthentication multiple times registers the options configurator only once")]
	public void AddPlanningCenterAuthentication_CalledMultipleTimes_OptionsConfiguratorRegisteredOnce()
	{
		var services = CreateServices();
		var builder = services.AddAuthentication();
		builder.AddPlanningCenterAuthentication();
		builder.AddPlanningCenterAuthentication();

		int count = services.Count(d =>
			d.ServiceType == typeof(IConfigureOptions<OpenIdConnectOptions>) &&
			d.ImplementationType?.FullName?.EndsWith("ConfigurePlanningCenterOpenIdConnectOptions") == true);

		Assert.Equal(1, count);
	}
}
