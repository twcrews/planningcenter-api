using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class InactiveReasonTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task InactiveReason_FullCrudLifecycle()
	{
		string? inactiveReasonId = null;

		try
		{
			// -- Create --
			var createResult = await Org.InactiveReasons.PostAsync(
				new InactiveReason { Value = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			inactiveReasonId = createResult.Data.Id;
			Assert.NotNull(inactiveReasonId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Value);

			// -- Read --
			var readResult = await Org.InactiveReasons.WithId(inactiveReasonId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(inactiveReasonId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.InactiveReasons.WithId(inactiveReasonId).PatchAsync(
				new InactiveReason { Value = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.InactiveReasons.WithId(inactiveReasonId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Value);

			// -- Delete --
			await Org.InactiveReasons.WithId(inactiveReasonId).DeleteAsync();
			inactiveReasonId = null;
		}
		finally
		{
			if (inactiveReasonId is not null)
			{
				try
				{
					await Org.InactiveReasons.WithId(inactiveReasonId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
