using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class OwnerTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Owner_GetAsync_ReturnsOwner()
	{
		var noteId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"groups/v2/events/{Fixture.EventId}/notes");
		Assert.NotNull(noteId);

		var result = await Org.Events.WithId(Fixture.EventId!).Notes.WithId(noteId).Owner.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
