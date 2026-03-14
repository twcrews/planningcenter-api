using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SongbookStatusTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task SongbookStatus_GetCollectionAsync_ReturnsCollection()
	{
		var response = await HttpClient.GetAsync("services/v2/songbook_statuses?per_page=1");
		Assert.True(response.IsSuccessStatusCode);
	}
}
