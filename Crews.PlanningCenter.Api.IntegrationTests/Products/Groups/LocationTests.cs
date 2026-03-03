using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class LocationTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Location_GetAsync_ReturnsLocation()
	{
		var result = await Org.Groups.WithId(Fixture.GroupId!).Location.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
