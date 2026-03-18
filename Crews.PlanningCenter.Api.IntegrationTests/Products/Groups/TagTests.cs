using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class TagTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Tag_GetAsync_ReturnsTag()
	{
		var tagId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"groups/v2/tag_groups/{Fixture.TagGroupId}/tags");
		Assert.NotNull(tagId);

		var result = await Org.TagGroups.WithId(Fixture.TagGroupId!).Tags.WithId(tagId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
