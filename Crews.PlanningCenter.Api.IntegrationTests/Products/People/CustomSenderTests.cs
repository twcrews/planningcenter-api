using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class CustomSenderTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task CustomSender_GetAsync_ReturnsCustomSender()
	{
		var customSenderId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "people/v2/custom_senders");
		Skip.If(customSenderId is null, "No custom sender data available.");

		var client = new CustomSenderClient(
			HttpClient,
			new Uri(HttpClient.BaseAddress!, $"people/v2/custom_senders/{customSenderId}/"));

		var result = await client.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
