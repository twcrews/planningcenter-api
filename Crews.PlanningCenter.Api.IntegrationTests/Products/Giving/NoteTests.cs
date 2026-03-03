using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class NoteTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Note_PostAndGetAsync_ReturnsNote()
	{
		// FIXME: Can't seem to successfully POST a donation via PAT.

		string? donationId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"giving/v2/people/{Fixture.PersonId}/donations");

		var noteClient = new PaginatedNoteClient(
			HttpClient,
			new Uri(new Uri(HttpClient.BaseAddress!, "giving/v2/"), $"donations/{donationId}/note/"));

		var postResult = await noteClient.PostAsync(
			new Note { Body = $"IntTest-Note-{UniqueId}" });

		Assert.NotNull(postResult.Data);
		Assert.Equal($"IntTest-Note-{UniqueId}", postResult.Data.Attributes?.Body);

		var getResult = await Org.Donations.WithId(donationId!).Note.GetAsync();
		Assert.NotNull(getResult.Data);
		Assert.Equal($"IntTest-Note-{UniqueId}", getResult.Data.Attributes?.Body);
	}
}
