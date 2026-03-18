using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class AttachmentTypeTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task AttachmentType_GetAsync_ReturnsAttachmentType()
	{
		var id = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "services/v2/attachment_types");
		Assert.NotNull(id);

		var result = await Org.AttachmentTypes.WithId(id).GetAsync();
		Assert.NotNull(result.Data);
		Assert.Equal(id, result.Data.Id);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
