using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class SpamEmailAddressTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task SpamEmailAddress_GetAsync_ReturnsSpamEmailAddress()
	{
		var spamEmailId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "people/v2/spam_email_addresses");
		Skip.If(spamEmailId is null, "No spam email address data available.");

		var result = await Org.SpamEmailAddresses.WithId(spamEmailId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
