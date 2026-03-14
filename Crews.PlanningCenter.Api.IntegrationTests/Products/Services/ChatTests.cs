using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class ChatTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Chat_GetAsync_ReturnsChat()
	{
		var result = await Org.Chat.GetAsync();
		Assert.NotNull(result);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
