using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class AttachmentTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task Attachment_GetAsync_ReturnsAttachment()
	{
		var attachmentId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/attachments");
		var result = await Org.Attachments.WithId(attachmentId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
