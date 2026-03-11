using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PersonAppTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PersonApp_GetAsync_ReturnsPersonApps()
	{
		var personId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/people");

		var personAppId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/people/{personId}/person_apps");

		var result = await Org.People.WithId(personId!).PersonApps
			.WithId(personAppId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
