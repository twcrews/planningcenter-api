using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.Groups.V2023_07_10;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products;

[Trait("Product", "Groups")]
public class GroupsReadTests(PlanningCenterFixture fixture) : IntegrationTestBase(fixture)
{
	[Fact]
	public async Task Organization_GetAsync_ReturnsOrganization()
	{
		var client = new GroupsClient(HttpClient);
		var org = client.Latest;

		var result = await org.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
