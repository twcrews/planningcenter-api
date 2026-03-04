using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ConditionTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Condition_GetAsync_ReturnsCondition()
	{
		Skip.If(Fixture.ListId is null, "No list data available for condition tests.");

		var ruleId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/lists/{Fixture.ListId}/rules");
		Skip.If(ruleId is null, "No rule data available for condition tests.");

		var conditionId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/lists/{Fixture.ListId}/rules/{ruleId}/conditions");
		Skip.If(conditionId is null, "No condition data available.");

		var result = await Org.Lists.WithId(Fixture.ListId!).Rules.WithId(ruleId!).Conditions
			.WithId(conditionId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
