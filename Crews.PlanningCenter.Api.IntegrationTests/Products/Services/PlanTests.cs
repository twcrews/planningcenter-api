using Crews.PlanningCenter.Api.Services.V2018_11_01;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Services;

public class PlanTests(ServicesFixture fixture) : ServicesTestBase(fixture)
{
	[Fact]
	public async Task Plan_FullCrudLifecycle()
	{
		string? planId = null;

		try
		{
			// -- Create --
			var createResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId).Plans.PostAsync(
				new Plan { Title = $"IntTest-Plan-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			planId = createResult.Data.Id;
			Assert.NotNull(planId);
			Assert.Equal($"IntTest-Plan-{UniqueId}", createResult.Data.Attributes?.Title);

			// -- Read --
			var readResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(planId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(planId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(planId).PatchAsync(
					new Plan { Title = $"IntTest-Plan-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
				.Plans.WithId(planId).GetAsync();
			Assert.Equal($"IntTest-Plan-Updated-{UniqueId}",
				verifyResult.Data?.Attributes?.Title);

			// -- Delete --
			await Org.ServiceTypes.WithId(Fixture.ServiceTypeId).Plans.WithId(planId).DeleteAsync();
			planId = null;
		}
		finally
		{
			if (planId is not null)
			{
				try
				{
					await Org.ServiceTypes.WithId(Fixture.ServiceTypeId)
						.Plans.WithId(planId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
