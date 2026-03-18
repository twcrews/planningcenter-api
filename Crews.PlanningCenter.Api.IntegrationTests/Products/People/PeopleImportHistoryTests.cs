using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PeopleImportHistoryTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PeopleImportHistory_GetAsync_ReturnsPeopleImportHistory()
	{
		var importId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "people/v2/people_imports");

		var historyId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/people_imports/{importId}/histories");

		var result = await Org.PeopleImports.WithId(importId!).Histories
			.WithId(historyId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
