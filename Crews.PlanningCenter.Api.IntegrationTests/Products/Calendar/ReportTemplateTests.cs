using Crews.PlanningCenter.Api.Calendar.V2022_07_07;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.Calendar;

public class ReportTemplateTests(CalendarFixture fixture) : CalendarTestBase(fixture)
{
	[Fact]
	public async Task ReportTemplate_FullCrudLifecycle()
	{
		string? reportTemplateId = null;

		try
		{
			// -- Create --
			var createResult = await Org.ReportTemplates.PostAsync(
				new ReportTemplate { Title = $"IntTest-RT-{UniqueId}" });
			Assert.NotNull(createResult.Data);
			reportTemplateId = createResult.Data.Id;
			Assert.NotNull(reportTemplateId);
			Assert.Equal($"IntTest-RT-{UniqueId}", createResult.Data.Attributes?.Title);

			// -- Read --
			var readResult = await Org.ReportTemplates.WithId(reportTemplateId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(reportTemplateId, readResult.Data.Id);
			Assert.Equal($"IntTest-RT-{UniqueId}", readResult.Data.Attributes?.Title);

			// -- Update --
			var updateResult = await Org.ReportTemplates.WithId(reportTemplateId).PatchAsync(
				new ReportTemplate { Title = $"IntTest-RT-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.ReportTemplates.WithId(reportTemplateId).GetAsync();
			Assert.Equal($"IntTest-RT-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Title);

			// -- Delete --
			await Org.ReportTemplates.WithId(reportTemplateId).DeleteAsync();
			reportTemplateId = null;
		}
		finally
		{
			if (reportTemplateId is not null)
			{
				try
				{
					await Org.ReportTemplates.WithId(reportTemplateId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
