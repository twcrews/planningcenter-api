using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class TagGroupTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task TagGroup_GetCollectionAsync_ReturnsCollection()
	{
		var result = await Org.TagGroups.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}

	[Fact]
	public async Task TagGroup_GetAsync_ReturnsTagGroup()
	{
		var tagGroupId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "services/v2/tag_groups");
		Assert.NotNull(tagGroupId);

		var result = await Org.TagGroups.WithId(tagGroupId).GetAsync();
		Assert.NotNull(result.Data);
		Assert.Equal(tagGroupId, result.Data.Id);
	}
}
