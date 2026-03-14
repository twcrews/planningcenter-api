using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class PlanTimeTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task PlanTime_FullCrudLifecycle()
	{
		string? planTimeId = null;

		try
		{
			var startsAt = DateTime.UtcNow.AddDays(7);

			// -- Create --
			var createResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).PlanTimes.PostAsync(new PlanTime
				{
					StartsAt = startsAt,
					TimeType = "service"
				});
			Assert.NotNull(createResult.Data);
			planTimeId = createResult.Data.Id;
			Assert.NotNull(planTimeId);

			// -- Read --
			var readResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).PlanTimes.WithId(planTimeId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(planTimeId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).PlanTimes.WithId(planTimeId).PatchAsync(new PlanTime
				{
					Name = $"IntTest-PlanTime-{UniqueId}"
				});
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).PlanTimes.WithId(planTimeId).GetAsync();
			Assert.Equal($"IntTest-PlanTime-{UniqueId}",
				verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(Fixture.PlanId).PlanTimes.WithId(planTimeId).DeleteAsync();
			planTimeId = null;
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
