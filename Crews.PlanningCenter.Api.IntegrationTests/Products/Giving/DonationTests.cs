using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class DonationTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	[Fact]
	public async Task Donation_FullCrudLifecycle()
	{
		// FIXME: Can't seem to successfully POST a donation via PAT.

		string? donationId = await CollectionReadHelper.GetFirstIdAsync(Fixture.HttpClient, $"giving/v2/people/{Fixture.PersonId}/donations");

		var readResult = await Org.Donations.WithId(donationId!).GetAsync();
		Assert.NotNull(readResult.Data);
		Assert.Equal(donationId, readResult.Data.Id);

		// FIXME: Can't update or delete donations via API except those create via an external payment source.
	}
}
