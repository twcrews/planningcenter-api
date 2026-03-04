using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class MessageTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Message_GetAsync_ReturnsMessage()
	{
		var messageId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/messages");

		var result = await Org.Messages.WithId(messageId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
