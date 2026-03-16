using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Webhooks;

public class AvailableEventTests(WebhooksFixture fixture) : WebhooksTestBase(fixture)
{
	[Fact]
	public async Task AvailableEvent_GetAsync_ReturnsAvailableEvents()
	{
		var result = await Org.AvailableEvents.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
