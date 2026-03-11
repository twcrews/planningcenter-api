using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ListTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task List_GetAsync_ReturnsList()
	{
		var result = await Org.Lists.WithId(Fixture.ListId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.ListId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
