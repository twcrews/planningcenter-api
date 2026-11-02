using Crews.PlanningCenter.Api.Webhooks.V2022_10_20;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Webhooks;

public class WebhookSubscriptionTests(WebhooksFixture fixture) : WebhooksTestBase(fixture)
{
	[Fact]
	public async Task WebhookSubscription_FullCrudLifecycle()
	{
		string? subscriptionId = null;

		try
		{
			// Subscription names are fixed, so one left by a crashed run must be removed before creating another.
			await TestDataSweeper.DeleteWhereAsync(HttpClient, "webhooks/v2/subscriptions", subscription =>
				subscription["attributes"]?["name"]?.GetValue<string>() == "people.v2.events.email.created"
				&& subscription["attributes"]?["url"]?.GetValue<string>() == TestDataSweeper.WebhookUrl);

			// -- Create --
			var createResult = await Org.Subscriptions.PostAsync(new WebhookSubscription
			{
				Name = $"people.v2.events.email.created",
				Url = TestDataSweeper.WebhookUrl,
				Active = true
			});
			Assert.NotNull(createResult.Data);
			subscriptionId = createResult.Data.Id;
			Assert.NotNull(subscriptionId);
			Assert.Equal($"people.v2.events.email.created", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Subscriptions.WithId(subscriptionId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(subscriptionId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Subscriptions.WithId(subscriptionId).PatchAsync(
				new WebhookSubscription { Active = false });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Subscriptions.WithId(subscriptionId).GetAsync();
			Assert.False(verifyResult.Data?.Attributes?.Active);

			// -- Delete --
			await Org.Subscriptions.WithId(subscriptionId).DeleteAsync();
			subscriptionId = null;
		}
		finally
		{
			if (subscriptionId is not null)
			{
				try
				{
					await Org.Subscriptions.WithId(subscriptionId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
