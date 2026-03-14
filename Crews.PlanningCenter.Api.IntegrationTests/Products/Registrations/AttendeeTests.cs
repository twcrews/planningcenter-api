using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Registrations;

public class AttendeeTests(RegistrationsFixture fixture) : RegistrationsTestBase(fixture)
{
	[Fact]
	public async Task Attendee_GetAsync_ReturnsAttendee()
	{
		var result = await Org.Signups.WithId(Fixture.SignupId).Attendees.WithId(Fixture.AttendeeId).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
