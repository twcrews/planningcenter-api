using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class AttachmentTypeGroupTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task AttachmentTypeGroup_GetCollectionAsync_ReturnsSuccessfully()
	{
		var response = await HttpClient.GetAsync("services/v2/attachment_type_groups?per_page=1");
		Assert.True(response.IsSuccessStatusCode);
	}
}
