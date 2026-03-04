using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class OrganizationStatisticsTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task OrganizationStatistics_GetAsync_ReturnsStatistics()
	{
		var result = await Org.Stats.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
