using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class CampusTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Campus_FullCrudLifecycle()
	{
		string? campusId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Campuses.PostAsync(new Campus
			{
				Name = $"IntTest-Campus-{UniqueId}",
				Street = "456 Test Ave",
				City = "Tulsa",
				State = "OK",
				Zip = "74101",
				Country = "United States",
				Latitude = (decimal)36.154,
				Longitude = (decimal)-95.993
			});
			Assert.NotNull(createResult.Data);
			campusId = createResult.Data.Id;
			Assert.NotNull(campusId);
			Assert.Equal($"IntTest-Campus-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Campuses.WithId(campusId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(campusId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Campuses.WithId(campusId).PatchAsync(
				new Campus { Description = "Updated description" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Campuses.WithId(campusId).GetAsync();
			Assert.Equal("Updated description", verifyResult.Data?.Attributes?.Description);

			// -- Delete --
			await Org.Campuses.WithId(campusId).DeleteAsync();
			campusId = null;
		}
		finally
		{
			if (campusId is not null)
			{
				try
				{
					await Org.Campuses.WithId(campusId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
