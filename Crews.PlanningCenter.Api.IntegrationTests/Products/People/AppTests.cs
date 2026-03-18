using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class AppTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task App_GetAsync_ReturnsApp()
	{
		var appId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/apps");

		var result = await Org.Apps.WithId(appId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
