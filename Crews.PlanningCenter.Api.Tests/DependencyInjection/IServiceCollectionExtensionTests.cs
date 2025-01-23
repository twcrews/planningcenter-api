using Crews.PlanningCenter.Api.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Crews.PlanningCenter.Api.Tests.DependencyInjection;

public class IServiceCollectionExtensionTests
{
	[Fact(DisplayName = "AddPlanningCenterApi adds HTTP client factory")]
	public void AddPlanningCenterApi_AddsHttpClientFactory()
	{
		IServiceCollection services = Substitute.For<IServiceCollection>();
		services.AddPlanningCenterApi(options => options.ApiBaseAddress = new("http://test.abc"));

		services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
			sd.ServiceType == typeof(HttpClient) && sd.ImplementationFactory != null));
	}

	[Fact(DisplayName = "AddPlanningCenterApi adds Planning Center API service")]
	public void AddPlanningCenterApi_AddsApiService()
	{
		IServiceCollection services = Substitute.For<IServiceCollection>();
		services.AddPlanningCenterApi(options => options.ApiBaseAddress = new("http://test.abc"));

		services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
			sd.ServiceType == typeof(IPlanningCenterApiService)));
	}

	[Fact(DisplayName = "AddPlanningCenterApi throws exception when not configured")]
	public void AddPlanningCenterApi_ThrowsWhenNotConfigured()
	{
		IServiceCollection services = Substitute.For<IServiceCollection>();
		Assert.Throws<InvalidOperationException>(() => services.AddPlanningCenterApi());
	}

	[Fact(DisplayName = "AddPlanningCenterApi gets configuration from providers")]
	public void AddPlanningCenterApi_AutoConfigures()
	{
		ServiceCollection services = new();
		IConfiguration configuration = Substitute.For<IConfiguration>();

		services.AddSingleton<IConfiguration>(configuration);
		services.AddPlanningCenterApi();

		configuration.Received(1).GetSection(PlanningCenterApiOptions.ConfigurationName);
	}

	[Theory(DisplayName = "AddPlanningCenterApi() correctly configures and adds service")]
	[InlineData(null, null)]
	[InlineData("http://test.abc", null)]
	[InlineData(null, "testClient")]
	[InlineData("http://test.abc", "testClient")]
	public void AddPlanningCenterApi_CorrectlyConfiguresService(string? baseAddressArg, string? httpClientNameArg)
	{

	}
}
