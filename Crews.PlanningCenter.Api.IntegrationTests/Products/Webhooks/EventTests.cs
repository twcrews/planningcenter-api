using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Webhooks;

public class EventTests(WebhooksFixture fixture) : WebhooksTestBase(fixture)
{
	[Fact]
	public async Task Event_GetCollectionAsync_ReturnsEvents()
	{
		var result = await Org.Subscriptions.WithId(Fixture.SubscriptionId).Events.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}

	[Fact]
	public async Task Event_GetAsync_ReturnsEvent()
	{
		if (Fixture.EventId is null || Fixture.EventSubscriptionId is null)
			return;

		var result = await Org.Subscriptions
			.WithId(Fixture.EventSubscriptionId)
			.Events
			.WithId(Fixture.EventId)
			.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.EventId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
