using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PeopleImportConflictTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PeopleImportConflict_GetAsync_ReturnsPeopleImportConflict()
	{
		var importId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "people/v2/people_imports");
		Skip.If(importId is null, "No people import data available.");

		var conflictId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/people_imports/{importId}/conflicts");
		Skip.If(conflictId is null, "No people import conflict data available.");

		var result = await Org.PeopleImports.WithId(importId!).Conflicts
			.WithId(conflictId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
