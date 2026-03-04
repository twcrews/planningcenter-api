using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class RuleTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Rule_GetAsync_ReturnsRule()
	{
		Skip.If(Fixture.ListId is null, "No list data available.");

		var ruleId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/lists/{Fixture.ListId}/rules");
		Skip.If(ruleId is null, "No rule data available.");

		var result = await Org.Lists.WithId(Fixture.ListId!).Rules
			.WithId(ruleId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
