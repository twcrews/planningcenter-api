using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PlatformNotificationTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PlatformNotification_GetAsync_ReturnsPlatformNotification()
	{
		var notificationId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/people/{Fixture.PersonId}/platform_notifications");
		Skip.If(notificationId is null, "No platform notification data available.");

		var result = await Org.People.WithId(Fixture.PersonId).PlatformNotifications
			.WithId(notificationId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
