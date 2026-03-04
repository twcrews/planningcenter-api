using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class GradeTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Grade_GetAsync_ReturnsGrade()
	{
		var gradeId = await CollectionReadHelper.GetFirstIdAsync(HttpClient, "people/v2/grades");

		var client = new GradeClient(
			HttpClient,
			new Uri(HttpClient.BaseAddress!, $"people/v2/grades/{gradeId}/"));

		var result = await client.GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
