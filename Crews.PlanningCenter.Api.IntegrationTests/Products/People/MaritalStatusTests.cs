using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class MaritalStatusTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task MaritalStatus_FullCrudLifecycle()
	{
		string? maritalStatusId = null;

		try
		{
			// -- Create --
			var createResult = await Org.MaritalStatuses.PostAsync(
				new MaritalStatus { Value = $"IntTest-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			maritalStatusId = createResult.Data.Id;
			Assert.NotNull(maritalStatusId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Value);

			// -- Read --
			var readResult = await Org.MaritalStatuses.WithId(maritalStatusId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(maritalStatusId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.MaritalStatuses.WithId(maritalStatusId).PatchAsync(
				new MaritalStatus { Value = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.MaritalStatuses.WithId(maritalStatusId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Value);

			// -- Delete --
			await Org.MaritalStatuses.WithId(maritalStatusId).DeleteAsync();
			maritalStatusId = null;
		}
		finally
		{
			if (maritalStatusId is not null)
			{
				try
				{
					await Org.MaritalStatuses.WithId(maritalStatusId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
