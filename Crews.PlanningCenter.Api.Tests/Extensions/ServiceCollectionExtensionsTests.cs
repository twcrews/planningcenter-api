using Crews.PlanningCenter.Api.Authentication;
using Crews.PlanningCenter.Api.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
	private static ServiceProvider BuildWithNamedClient(string name)
	{
		var services = new ServiceCollection();
		services.AddHttpClient(name, client =>
		{
			client.BaseAddress = new Uri(PlanningCenterAuthenticationDefaults.BaseUrl);
		});
		services.AddPlanningCenterApi(name);
		return services.BuildServiceProvider();
	}

	[Fact(DisplayName = "AddPlanningCenterApi() returns the same service collection for chaining")]
	public void AddPlanningCenterApi_ReturnsServiceCollection()
	{
		var services = new ServiceCollection();
		services.AddHttpContextAccessor();

		var result = services.AddPlanningCenterApi();

		Assert.Same(services, result);
	}

	[Fact(DisplayName = "AddPlanningCenterApi(httpClientName) returns the same service collection for chaining")]
	public void AddPlanningCenterApi_WithHttpClientName_ReturnsServiceCollection()
	{
		var services = new ServiceCollection();
		services.AddHttpClient("test");

		var result = services.AddPlanningCenterApi("test");

		Assert.Same(services, result);
	}

	[Theory(DisplayName = "AddPlanningCenterApi(httpClientName) registers all product clients as scoped")]
	[InlineData(typeof(CalendarClient))]
	[InlineData(typeof(CheckInsClient))]
	[InlineData(typeof(GivingClient))]
	[InlineData(typeof(GroupsClient))]
	[InlineData(typeof(PeopleClient))]
	[InlineData(typeof(PublishingClient))]
	[InlineData(typeof(RegistrationsClient))]
	[InlineData(typeof(ServicesClient))]
	public void AddPlanningCenterApi_WithHttpClientName_RegistersAllProductClients(Type clientType)
	{
		var services = new ServiceCollection();
		services.AddHttpClient("test");
		services.AddPlanningCenterApi("test");

		var descriptor = services.FirstOrDefault(d => d.ServiceType == clientType);

		Assert.NotNull(descriptor);
		Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime);
	}

	[Theory(DisplayName = "AddPlanningCenterApi(httpClientName) resolves all product clients from DI")]
	[InlineData(typeof(CalendarClient))]
	[InlineData(typeof(CheckInsClient))]
	[InlineData(typeof(GivingClient))]
	[InlineData(typeof(GroupsClient))]
	[InlineData(typeof(PeopleClient))]
	[InlineData(typeof(PublishingClient))]
	[InlineData(typeof(RegistrationsClient))]
	[InlineData(typeof(ServicesClient))]
	public void AddPlanningCenterApi_WithHttpClientName_ResolvesAllProductClients(Type clientType)
	{
		using var provider = BuildWithNamedClient("test");
		using var scope = provider.CreateScope();

		var client = scope.ServiceProvider.GetRequiredService(clientType);

		Assert.NotNull(client);
	}

	[Theory(DisplayName = "AddPlanningCenterApi() registers all product clients as scoped")]
	[InlineData(typeof(CalendarClient))]
	[InlineData(typeof(CheckInsClient))]
	[InlineData(typeof(GivingClient))]
	[InlineData(typeof(GroupsClient))]
	[InlineData(typeof(PeopleClient))]
	[InlineData(typeof(PublishingClient))]
	[InlineData(typeof(RegistrationsClient))]
	[InlineData(typeof(ServicesClient))]
	public void AddPlanningCenterApi_RegistersAllProductClients(Type clientType)
	{
		var services = new ServiceCollection();
		services.AddPlanningCenterApi();

		var descriptor = services.FirstOrDefault(d => d.ServiceType == clientType);

		Assert.NotNull(descriptor);
		Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime);
	}

	[Fact(DisplayName = "AddPlanningCenterApi() registers PlanningCenterTokenHandler as transient")]
	public void AddPlanningCenterApi_RegistersTokenHandlerAsTransient()
	{
		var services = new ServiceCollection();
		services.AddPlanningCenterApi();

		var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(PlanningCenterTokenHandler));

		Assert.NotNull(descriptor);
		Assert.Equal(ServiceLifetime.Transient, descriptor.Lifetime);
	}

	[Fact(DisplayName = "AddPlanningCenterApi() registers a named HttpClient")]
	public void AddPlanningCenterApi_RegistersNamedHttpClient()
	{
		var services = new ServiceCollection();
		services.AddPlanningCenterApi();

		Assert.Contains(services, d => d.ServiceType == typeof(IHttpClientFactory));
	}
}
