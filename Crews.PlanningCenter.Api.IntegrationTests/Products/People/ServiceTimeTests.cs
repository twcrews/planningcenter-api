using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ServiceTimeTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task ServiceTime_FullCrudLifecycle()
	{
		string? serviceTimeId = null;

		try
		{
			var campusServiceTimes = Org.Campuses.WithId(Fixture.CampusId).ServiceTimes;

			// -- Create --
			var createResult = await campusServiceTimes.PostAsync(new ServiceTime
			{
				Day = "sunday",
				Description = $"IntTest-{UniqueId}",
				StartTime = 600
			});
			Assert.NotNull(createResult.Data);
			serviceTimeId = createResult.Data.Id;
			Assert.NotNull(serviceTimeId);
			Assert.Equal("sunday", createResult.Data.Attributes?.Day);

			// -- Read --
			var readResult = await Org.Campuses.WithId(Fixture.CampusId).ServiceTimes
				.WithId(serviceTimeId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(serviceTimeId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Campuses.WithId(Fixture.CampusId).ServiceTimes
				.WithId(serviceTimeId).PatchAsync(new ServiceTime { Day = "saturday" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Campuses.WithId(Fixture.CampusId).ServiceTimes
				.WithId(serviceTimeId).GetAsync();
			Assert.Equal("saturday", verifyResult.Data?.Attributes?.Day);

			// -- Delete --
			await Org.Campuses.WithId(Fixture.CampusId).ServiceTimes
				.WithId(serviceTimeId).DeleteAsync();
			serviceTimeId = null;
		}
		finally
		{
			if (serviceTimeId is not null)
			{
				try
				{
					await Org.Campuses.WithId(Fixture.CampusId).ServiceTimes
						.WithId(serviceTimeId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
