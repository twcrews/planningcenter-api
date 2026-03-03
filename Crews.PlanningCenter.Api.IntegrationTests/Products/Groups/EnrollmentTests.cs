using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Groups;

public class EnrollmentTests(GroupsFixture fixture) : GroupsTestBase(fixture)
{
	[Fact]
	public async Task Enrollment_GetAsync_ReturnsEnrollment()
	{
		var result = await Org.Groups.WithId(Fixture.GroupId!).Enrollment.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
