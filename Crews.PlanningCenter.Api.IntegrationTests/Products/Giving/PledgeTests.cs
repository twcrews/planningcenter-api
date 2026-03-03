using Crews.PlanningCenter.Api.Giving.V2019_10_18;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Giving;

public class PledgeTests(GivingFixture fixture) : GivingTestBase(fixture)
{
	PaginatedPledgeClient Pledges =>
		new(HttpClient, new Uri(HttpClient.BaseAddress!, $"giving/v2/people/{Fixture.PersonId}/pledges/"));

	[Fact]
	public async Task Pledge_FullCrudLifecycle()
	{
		string? pledgeId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, $"giving/v2/people/{Fixture.PersonId}/pledges");

		// FIXME: All attempts to POST to the Pledges endpoint result in a 403. This may be because only registered
		// OAuth apps with specific permissions can create pledges, as it seems personal access tokens correspond
		// with a separate user ID from that of their owner, and this ID does not have permission to create pledges.

		Assert.NotNull(pledgeId);

		var readResult = await Pledges.WithId(pledgeId).GetAsync();
		Assert.NotNull(readResult.Data);
		Assert.Equal(pledgeId, readResult.Data.Id);

		var originalCents = readResult.Data.Attributes?.AmountCents;
		var updateResult = await Pledges.WithId(pledgeId).PatchAsync(
			new Pledge { AmountCents = originalCents + 1 });
		Assert.NotNull(updateResult.Data);
		Assert.Equal(originalCents + 1, updateResult.Data.Attributes?.AmountCents);

		var verifyResult = await Pledges.WithId(pledgeId).GetAsync();
		Assert.Equal(originalCents + 1, verifyResult.Data?.Attributes?.AmountCents);
	}
}
