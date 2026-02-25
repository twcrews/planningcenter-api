using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.CheckIns.V2025_05_28;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products;

[Trait("Product", "CheckIns")]
public class CheckInsReadTests(PlanningCenterFixture fixture) : IntegrationTestBase(fixture)
{
	[Fact]
	public async Task Organization_GetAsync_ReturnsOrganization()
	{
		var client = new CheckInsClient(HttpClient);
		var org = client.Latest;

		var result = await org.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
