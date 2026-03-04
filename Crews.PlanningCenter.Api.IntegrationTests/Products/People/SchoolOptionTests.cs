using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class SchoolOptionTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task SchoolOption_FullCrudLifecycle()
	{
		string? schoolOptionId = null;

		try
		{
			// -- Create --
			var createResult = await Org.SchoolOptions.PostAsync(new SchoolOption
			{
				Value = $"IntTest-{UniqueId}",
				BeginningGrade = "K",
				EndingGrade = "5"
			});
			Assert.NotNull(createResult.Data);
			schoolOptionId = createResult.Data.Id;
			Assert.NotNull(schoolOptionId);

			// -- Read --
			var readResult = await Org.SchoolOptions.WithId(schoolOptionId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(schoolOptionId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.SchoolOptions.WithId(schoolOptionId).PatchAsync(
				new SchoolOption { Value = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.SchoolOptions.WithId(schoolOptionId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Value);

			// -- Delete --
			await Org.SchoolOptions.WithId(schoolOptionId).DeleteAsync();
			schoolOptionId = null;
		}
		finally
		{
			if (schoolOptionId is not null)
			{
				try
				{
					await Org.SchoolOptions.WithId(schoolOptionId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
