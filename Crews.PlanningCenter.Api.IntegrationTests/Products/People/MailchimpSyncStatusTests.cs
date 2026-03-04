using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class MailchimpSyncStatusTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task MailchimpSyncStatus_GetAsync_ReturnsMailchimpSyncStatus()
	{
		Skip.If(Fixture.ListId is null, "No list data available.");

		var syncStatusId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/lists/{Fixture.ListId}/mailchimp_sync_status");
		Skip.If(syncStatusId is null, "No Mailchimp sync status data available.");

		var result = await Org.Lists.WithId(Fixture.ListId!).MailchimpSyncStatus
			.WithId(syncStatusId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
