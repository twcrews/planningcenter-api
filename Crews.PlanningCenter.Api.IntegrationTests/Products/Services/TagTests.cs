using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class TagTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Tag_GetCollectionAsync_ReturnsCollection()
	{
		var tagGroupId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "services/v2/tag_groups");
		Assert.NotNull(tagGroupId);

		var result = await Org.TagGroups.WithId(tagGroupId).Tags.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
