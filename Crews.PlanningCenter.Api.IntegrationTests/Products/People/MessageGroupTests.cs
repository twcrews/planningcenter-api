using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class MessageGroupTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task MessageGroup_GetAsync_ReturnsMessageGroup()
	{
		var messageGroupId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/message_groups");
		Skip.If(messageGroupId is null, "No message group data available.");

		var result = await Org.MessageGroups.WithId(messageGroupId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
