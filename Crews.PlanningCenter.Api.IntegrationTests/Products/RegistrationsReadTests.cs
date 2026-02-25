using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.Registrations.V2025_05_01;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products;

[Trait("Product", "Registrations")]
public class RegistrationsReadTests(PlanningCenterFixture fixture) : IntegrationTestBase(fixture)
{
	[Fact]
	public async Task Organization_GetAsync_ReturnsOrganization()
	{
		var client = new RegistrationsClient(HttpClient);
		var org = client.Latest;

		var result = await org.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
