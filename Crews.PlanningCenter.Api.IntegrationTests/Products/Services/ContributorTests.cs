using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ContributorTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Contributor_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
			.Plans.WithId(Fixture.PlanId).Contributors.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
