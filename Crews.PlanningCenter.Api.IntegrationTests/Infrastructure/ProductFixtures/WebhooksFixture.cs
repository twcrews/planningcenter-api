using Crews.PlanningCenter.Api.Webhooks.V2022_10_20;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for Webhooks product integration tests.
/// Pre-creates a WebhookSubscription and discovers existing event/delivery IDs for child tests.
/// </summary>
public class WebhooksFixture : PlanningCenterFixture
{
	string _fixtureId = null!;

	/// <summary>ID of a pre-created WebhookSubscription for Event collection tests.</summary>
	public string SubscriptionId { get; private set; } = null!;

	/// <summary>
	/// ID of the subscription from which <see cref="EventId"/> was discovered,
	/// or null if no events exist.
	/// </summary>
	public string? EventSubscriptionId { get; private set; }

	/// <summary>ID of an existing Event for child resource tests, or null if none exist.</summary>
	public string? EventId { get; private set; }

	/// <summary>ID of an existing Delivery for tests, or null if none exist.</summary>
	public string? DeliveryId { get; private set; }

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();
		_fixtureId = TestData.NewId();

		// Discover existing events before creating the fixture subscription so that
		// the fixture subscription doesn't pollute the first-item search.
		var existingSubId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "webhooks/v2/subscriptions");
		if (existingSubId is not null)
		{
			EventId = await CollectionReadHelper.GetFirstIdAsync(
				HttpClient, $"webhooks/v2/subscriptions/{existingSubId}/events");

			if (EventId is not null)
			{
				EventSubscriptionId = existingSubId;
				DeliveryId = await CollectionReadHelper.GetFirstIdAsync(
					HttpClient, $"webhooks/v2/subscriptions/{existingSubId}/events/{EventId}/deliveries");
			}
		}

		var org = new WebhooksClient(HttpClient).Latest;

		const string subscriptionName = "people.v2.events.address.created";

		// Subscription names are fixed, so one left by a crashed run must be removed before creating another.
		await TestDataSweeper.DeleteWhereAsync(HttpClient, "webhooks/v2/subscriptions", subscription =>
			subscription["attributes"]?["name"]?.GetValue<string>() == subscriptionName
			&& subscription["attributes"]?["url"]?.GetValue<string>() == TestDataSweeper.WebhookUrl);

		var result = await org.Subscriptions.PostAsync(new WebhookSubscription
		{
			Name = subscriptionName,
			Url = TestDataSweeper.WebhookUrl
		});
		SubscriptionId = result.Data!.Id!;
	}

	public override async Task DisposeAsync()
	{
		var org = new WebhooksClient(HttpClient).Latest;

		try { await org.Subscriptions.WithId(SubscriptionId).DeleteAsync(); } catch { }

		await base.DisposeAsync();
	}
}
