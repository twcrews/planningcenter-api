using Crews.PlanningCenter.Api.Groups.V2023_07_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class AttendanceTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Attendance_GetAsync_ReturnsAttendance()
	{
		var result = await Org.Events.WithId(Fixture.EventId!).Attendances.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
