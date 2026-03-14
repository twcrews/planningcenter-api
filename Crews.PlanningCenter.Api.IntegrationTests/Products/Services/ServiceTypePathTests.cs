using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ServiceTypePathTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task ServiceTypePath_GetAsync_ReturnsResponse()
	{
		// FIXME: I cannot find an endpoint for this resource. It's likely an attribute of some other resource.
	}
}
