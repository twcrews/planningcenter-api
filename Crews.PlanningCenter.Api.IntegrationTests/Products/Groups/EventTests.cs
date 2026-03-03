using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class EventTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Event_GetAsync_ReturnsEvent()
	{
		var result = await Org.Events.WithId(Fixture.EventId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
