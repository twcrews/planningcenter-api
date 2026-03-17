using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Products.People;

public class FieldDatumTests(PeopleFixture fixture) : PeopleTestBase(fixture)
{
	[Fact]
	public async Task FieldDatum_FullCrudLifecycle()
	{
		string? fieldDatumId = null;

		try
		{
			var personFieldData = Org.People.WithId(Fixture.PersonId).FieldData;

			// -- Create --
			var createResult = await personFieldData.PostAsync(new JsonApiDocument<FieldDatumResource>
			{
				Data = new()
				{
					Attributes = new FieldDatum { Value = $"IntTest-{UniqueId}" },
					Relationships = new() { FieldDefinition = new() { Data = new() { Type = "FieldDefinition", Id = Fixture.FieldDefinitionId } } }
				}
			});
			Assert.NotNull(createResult.Data);
			fieldDatumId = createResult.Data.Id;
			Assert.NotNull(fieldDatumId);
			Assert.Equal($"IntTest-{UniqueId}", createResult.Data.Attributes?.Value);

			// -- Read --
			var readResult = await Org.FieldData.WithId(fieldDatumId).GetAsync();
			Assert.NotNull(readResult.Data);
			Assert.Equal(fieldDatumId, readResult.Data.Id);

			// -- Update --
			var updateResult = await Org.FieldData.WithId(fieldDatumId).PatchAsync(
				new FieldDatum { Value = $"IntTest-Updated-{UniqueId}" });
			Assert.NotNull(updateResult.Data);

			// -- Verify Update --
			var verifyResult = await Org.FieldData.WithId(fieldDatumId).GetAsync();
			Assert.Equal($"IntTest-Updated-{UniqueId}", verifyResult.Data?.Attributes?.Value);

			// -- Delete --
			await Org.FieldData.WithId(fieldDatumId).DeleteAsync();
			fieldDatumId = null;
		}
		finally
		{
			if (fieldDatumId is not null)
			{
				try
				{
					await Org.FieldData.WithId(fieldDatumId).DeleteAsync();
				}
				catch { /* best effort */ }
			}
		}
	}
}
