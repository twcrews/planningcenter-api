using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ResourceBookingTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task ResourceBooking_GetAsync_ReturnsResourceBooking()
	{
		var bookingId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "calendar/v2/resource_bookings");
		var result = await Org.ResourceBookings.WithId(bookingId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
