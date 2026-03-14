using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ServiceTypePathTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task ServiceTypePath_GetAsync_ReturnsResponse()
	{
		var response = await HttpClient.GetAsync(
			$"services/v2/service_types/{Fixture.ServiceTypeId}/service_type_path");
		Assert.True(response.IsSuccessStatusCode);
	}
}
