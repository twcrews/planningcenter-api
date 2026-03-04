using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class ReportTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task Report_FullCrudLifecycle()
	{
		string? reportId = null;

		try
		{
			// -- Create --
			var createResult = await Org.Reports.PostAsync(new Report
			{
				Name = $"IntTest-{UniqueId}",
				Body = "{{ person.name }}"
			});
			Assert.NotNull(createResult.Data);
			reportId = createResult.Data.Id;
			Assert.NotNull(reportId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.Reports.WithId(reportId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(reportId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.Reports.WithId(reportId).PatchAsync(
				new Report { Name = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.Reports.WithId(reportId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.Reports.WithId(reportId).DeleteAsync();
			reportId = null;
		}
		finally
		{
			if (reportId is not null)
			{
				try
				{
					await Org.Reports.WithId(reportId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
