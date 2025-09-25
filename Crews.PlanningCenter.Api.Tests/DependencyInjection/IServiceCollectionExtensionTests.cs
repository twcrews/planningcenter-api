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
		ServiceCollection services = new();
		services.AddPlanningCenterApi(options => options.ApiBaseAddress = new("http://test.abc"));

		ServiceDescriptor? httpClientDescriptor = services.FirstOrDefault(sd =>
			sd.ServiceType == typeof(HttpClient) && sd.ImplementationFactory != null);

		Assert.NotNull(httpClientDescriptor);
	}

	[Fact(DisplayName = "AddPlanningCenterApi adds Planning Center API service")]
	public void AddPlanningCenterApi_AddsApiService()
	{
		ServiceCollection services = new();
		services.AddPlanningCenterApi(options => options.ApiBaseAddress = new("http://test.abc"));

		ServiceDescriptor? apiServiceDescriptor = services.FirstOrDefault(sd =>
			sd.ServiceType == typeof(IPlanningCenterApiService));

		Assert.NotNull(apiServiceDescriptor);
	}

	[Fact(DisplayName = "AddPlanningCenterApi throws exception when not configured")]
	public void AddPlanningCenterApi_ThrowsWhenNotConfigured()
	{
		ServiceCollection services = new();
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
}
