using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class GradeTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Grade_GetAsync_ReturnsGrade()
	{
		var result = await Org.Grades.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
