using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class SplitTeamRehearsalAssignmentTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task SplitTeamRehearsalAssignment_GetCollectionAsync_ReturnsCollection()
	{
		string? planTimeId = null;

		try
		{
			var startsAt = DateTime.UtcNow.AddDays(7);
			var endsAt = startsAt.AddHours(1);
			var planTimeResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).PlanTimes.PostAsync(new PlanTime
				{
					StartsAt = startsAt,
					EndsAt = endsAt,
					TimeType = "rehearsal"
				});
			Assert.NotNull(planTimeResult.Data);
			planTimeId = planTimeResult.Data.Id;
			Assert.NotNull(planTimeId);

			var result = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).PlanTimes.WithId(planTimeId)
				.SplitTeamRehearsalAssignments.GetAsync();
			Assert.NotNull(result);
			Assert.True(result.ResponseMessage?.IsSuccessStatusCode);
		}
		finally
		{
			if (planTimeId is not null)
			{
				try
				{
					await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
						.Plans.WithId(Fixture.PlanId).PlanTimes.WithId(planTimeId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
