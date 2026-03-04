using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ConditionTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Condition_GetAsync_ReturnsCondition()
	{
		var ruleId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/lists/{Fixture.ListId}/rules");

		var conditionId = await CollectionReadHelper.GetFirstIdAsync(
			HttpClient, $"people/v2/lists/{Fixture.ListId}/rules/{ruleId}/conditions");

		var result = await Org.Lists.WithId(Fixture.ListId!).Rules.WithId(ruleId!).Conditions
			.WithId(conditionId!).GetAsync();

		Assert.NotNull(result);
		Assert.NotNull(result.Data);
		Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
	}
}
