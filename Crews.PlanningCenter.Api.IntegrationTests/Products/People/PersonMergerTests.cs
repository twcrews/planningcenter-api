using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class PersonMergerTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task PersonMerger_GetAsync_ReturnsPersonMerger()
	{
		var personMergerId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, "people/v2/person_mergers");
		Skip.If(personMergerId is null, "No person merger data available.");

		var result = await Org.PersonMergers.WithId(personMergerId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
