using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.Publishing.V2024_03_25;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products;

[Trait("Product", "Publishing")]
public class PublishingReadTests(PlanningCenterFixture fixture) : IntegrationTestBase(fixture)
{
	[Fact]
	public async Task Organization_GetAsync_ReturnsOrganization()
	{
		var client = new PublishingClient(HttpClient);
		var org = client.Latest;

		var result = await org.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
