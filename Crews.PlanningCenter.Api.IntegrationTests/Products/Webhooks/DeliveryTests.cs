using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Webhooks;

public class DeliveryTests(WebhooksFixture fixture) : WebhooksTestBase(fixture)
{
	[Fact]
	public async Task Delivery_GetCollectionAsync_ReturnsDeliveries()
	{
		if (Fixture.EventId is null || Fixture.EventSubscriptionId is null)
			return;

		var result = await Org.Subscriptions
			.WithId(Fixture.EventSubscriptionId)
			.Events
			.WithId(Fixture.EventId)
			.Deliveries
			.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}

	[Fact]
	public async Task Delivery_GetAsync_ReturnsDelivery()
	{
		if (Fixture.DeliveryId is null || Fixture.EventId is null || Fixture.EventSubscriptionId is null)
			return;

		var result = await Org.Subscriptions
			.WithId(Fixture.EventSubscriptionId)
			.Events
			.WithId(Fixture.EventId)
			.Deliveries
			.WithId(Fixture.DeliveryId)
			.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.Equal(Fixture.DeliveryId, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
