using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class TeamPositionTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task TeamPosition_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
			.TeamPositions.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
