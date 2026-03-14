using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class FolderPathTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task FolderPath_GetAsync_ReturnsResponse()
	{
		var folderId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "services/v2/folders");
		Assert.NotNull(folderId);

		await Org.Folders.WithId(folderId).GetAsync();
	}
}
