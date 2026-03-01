using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class EventResourceAnswerTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task EventResourceAnswer_GetAsync_ReturnsEventResourceAnswer()
	{
		var answerId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"calendar/v2/event_resource_requests/{Fixture.EventResourceRequestId}/answers");
		if (answerId is null)
			return;

		var result = await Org.EventResourceRequests.WithId(Fixture.EventResourceRequestId).Answers.WithId(answerId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
