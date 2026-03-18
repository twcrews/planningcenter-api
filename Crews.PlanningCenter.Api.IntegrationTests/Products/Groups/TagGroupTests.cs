using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class TagGroupTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task TagGroup_GetAsync_ReturnsTagGroup()
	{
		var result = await Org.TagGroups.WithId(Fixture.TagGroupId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
