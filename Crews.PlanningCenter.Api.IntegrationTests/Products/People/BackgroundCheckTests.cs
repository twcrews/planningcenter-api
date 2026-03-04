using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class BackgroundCheckTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task BackgroundCheck_FullCrudLifecycle()
	{
		string? checkId = null;

		try
		{
			var personChecks = Org.People.WithId(Fixture.PersonId).BackgroundChecks;

			// -- Create --
			var createResult = await personChecks.PostAsync(new BackgroundCheck
			{
				Note = $"IntTest-{UniqueId}"
			});
			Assert.NotNull(createResult.Data);
			checkId = createResult.Data.Id;
			Assert.NotNull(checkId);

			// -- Read --
			var readResult = await Org.BackgroundChecks.WithId(checkId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(checkId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.BackgroundChecks.WithId(checkId).PatchAsync(
				new BackgroundCheck { Note = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.BackgroundChecks.WithId(checkId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Note);

			// -- Delete --
			await Org.BackgroundChecks.WithId(checkId).DeleteAsync();
			checkId = null;
		}
		finally
		{
			if (checkId is not null)
			{
				try
				{
					await Org.BackgroundChecks.WithId(checkId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
