using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PeopleImportTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PeopleImport_GetAsync_ReturnsPeopleImport()
	{
		var importId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "people/v2/people_imports");
		Skip.If(importId is null, "No people import data available.");

		var result = await Org.PeopleImports.WithId(importId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
