using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class FieldDefinitionTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task FieldDefinition_FullCrudLifecycle()
	{
		string? fieldDefId = null;

		try
		{
			var tabFieldDefs = Org.Tabs.WithId(Fixture.TabId).FieldDefinitions;

			// -- Create --
			var createResult = await tabFieldDefs.PostAsync(new FieldDefinition
			{
				Name = $"IntTest-FieldDef-{UniqueId}",
				DataType = "text"
			});
			Assert.NotNull(createResult.Data);
			fieldDefId = createResult.Data.Id;
			Assert.NotNull(fieldDefId);
			Assert.Equal($"IntTest-FieldDef-{UniqueId}", createResult.Data.Attributes?.Name);

			// -- Read --
			var readResult = await Org.FieldDefinitions.WithId(fieldDefId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(fieldDefId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.FieldDefinitions.WithId(fieldDefId).PatchAsync(
				new FieldDefinition { Name = $"IntTest-FieldDef-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.FieldDefinitions.WithId(fieldDefId).GetAsync();
			Assert.Equal($"IntTest-FieldDef-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Name);

			// -- Delete --
			await Org.FieldDefinitions.WithId(fieldDefId).DeleteAsync();
			fieldDefId = null;
		}
		finally
		{
			if (fieldDefId is not null)
			{
				try
				{
					await Org.FieldDefinitions.WithId(fieldDefId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
