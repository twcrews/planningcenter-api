using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class FieldOptionTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task FieldOption_FullCrudLifecycle()
	{
		string? selectFieldDefId = null;
		string? fieldOptionId = null;

		try
		{
			// Create a 'select' type field definition to host options
			var fieldDefResult = await Org.Tabs.WithId(Fixture.TabId).FieldDefinitions.PostAsync(
				new FieldDefinition
				{
					Name = $"IntTest-SelectField-{UniqueId}",
					DataType = "select"
				});
			Assert.NotNull(fieldDefResult.Data);
			selectFieldDefId = fieldDefResult.Data.Id;
			Assert.NotNull(selectFieldDefId);

			var fieldOptions = Org.FieldDefinitions.WithId(selectFieldDefId).FieldOptions;

			// -- Create --
			var createResult = await fieldOptions.PostAsync(new FieldOption
			{
				Value = $"Option-{UniqueId}",
				Sequence = 1
			});
			Assert.NotNull(createResult.Data);
			fieldOptionId = createResult.Data.Id;
			Assert.NotNull(fieldOptionId);
			Assert.Equal($"Option-{UniqueId}", createResult.Data.Attributes?.Value);

			// -- Read --
			var readResult = await fieldOptions.WithId(fieldOptionId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(fieldOptionId, readResult.Data.Id);

			// -- Update --
			var updateResult = await fieldOptions.WithId(fieldOptionId).PatchAsync(
				new FieldOption { Value = $"Option-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await fieldOptions.WithId(fieldOptionId).GetAsync();
			Assert.Equal($"Option-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Value);

			// -- Delete --
			await fieldOptions.WithId(fieldOptionId).DeleteAsync();
			fieldOptionId = null;
		}
		finally
		{
			if (fieldOptionId is not null)
			{
				try
				{
					await Org.FieldDefinitions.WithId(selectFieldDefId!).FieldOptions
						.WithId(fieldOptionId).DeleteAsync();
				}
				catch { /* best effort */ }
			}

			if (selectFieldDefId is not null)
			{
				try
				{
					await Org.FieldDefinitions.WithId(selectFieldDefId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
