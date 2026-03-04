using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class NoteCategorySubscriptionTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task NoteCategorySubscription_GetAsync_ReturnsNoteCategorySubscription()
	{
		var subscriptionId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "people/v2/note_category_subscriptions");
		Skip.If(subscriptionId is null, "No note category subscription data available.");

		var result = await Org.NoteCategorySubscriptions.WithId(subscriptionId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
