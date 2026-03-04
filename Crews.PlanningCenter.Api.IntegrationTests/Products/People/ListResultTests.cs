using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ListResultTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task ListResult_GetAsync_ReturnsListResult()
	{
		Skip.If(Fixture.ListId is null, "No list data available.");

		var listResultId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/lists/{Fixture.ListId}/list_results");
		Skip.If(listResultId is null, "No list result data available.");

		var result = await Org.Lists.WithId(Fixture.ListId!).ListResults
			.WithId(listResultId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
