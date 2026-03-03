using Crews.PlanningCenter.Api.Groups.V2023_07_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class RsvpTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Rsvp_GetAsync_ReturnsRsvp()
	{
		var eventId = await CollectionReadHelper.GetLastIdAsync(HttpClient, "groups/v2/events");
		var rsvpId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"groups/v2/events/{eventId}/rsvps");
		Assert.NotNull(rsvpId);

		var result = await Org.Events.WithId(eventId!).Rsvps.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
