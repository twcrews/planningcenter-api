using System.Text.Json;
using Crews.PlanningCenter.Api.People.V2025_11_10;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

/// <summary>
/// Fixture for People product integration tests.
/// Pre-creates shared parent resources needed by child resource tests.
/// </summary>
public class PeopleFixture : PlanningCenterFixture
{
	string _fixtureId = null!;

	/// <summary>ID of a pre-created Person for child resource tests (Email, Address, PhoneNumber, etc.).</summary>
	public string PersonId { get; private set; } = null!;

	/// <summary>ID of a pre-created Household for HouseholdMembership tests.</summary>
	public string HouseholdId { get; private set; } = null!;

	/// <summary>ID of a pre-created Workflow for WorkflowCard/Step/Note tests.</summary>
	public string WorkflowId { get; private set; } = null!;

	/// <summary>ID of a pre-created NoteCategory for Note tests.</summary>
	public string NoteCategoryId { get; private set; } = null!;

	/// <summary>ID of a pre-created Tab for FieldDefinition tests.</summary>
	public string TabId { get; private set; } = null!;

	/// <summary>ID of a pre-created Campus for ServiceTime tests.</summary>
	public string CampusId { get; private set; } = null!;

	public override async Task InitializeAsync()
	{
		await base.InitializeAsync();
		_fixtureId = Guid.NewGuid().ToString("N")[..8];

		var org = new PeopleClient(HttpClient).Latest;

		var personResult = await org.People.PostAsync(new Person
		{
			FirstName = "Fixture",
			LastName = $"Person-{_fixtureId}"
		});
		PersonId = personResult.Data!.Id!;

		var householdResult = await org.Households.PostAsync(new JsonApiDocument<HouseholdResource>
        {
            Data = new() 
            {
                Attributes = new Household 
                {
                    Name = $"Fixture-Household-{_fixtureId}" 
                },
                Relationships = new()
                {
                    People = new()
                    {
                        Data = JsonSerializer.SerializeToElement<IEnumerable<JsonApiResourceIdentifier>>([
                            new() { Type = "Person", Id = "188120583" }
                        ])
                    },
                    PrimaryContact = new()
                    {
                        Data = JsonSerializer.SerializeToElement<JsonApiResourceIdentifier>(new()
                        {
                            Type = "Person",
                            Id = "188120583"
                        })
                    }
                }
            }
        });
		HouseholdId = householdResult.Data!.Id!;

		var workflowResult = await org.Workflows.PostAsync(
			new Workflow { Name = $"Fixture-Workflow-{_fixtureId}" });
		WorkflowId = workflowResult.Data!.Id!;

		var noteCategoryResult = await org.NoteCategories.PostAsync(
			new NoteCategory { Name = $"Fixture-NoteCategory-{_fixtureId}" });
		NoteCategoryId = noteCategoryResult.Data!.Id!;

		var tabResult = await org.Tabs.PostAsync(
			new Tab { Name = $"Fixture-Tab-{_fixtureId}" });
		TabId = tabResult.Data!.Id!;

		var campusResult = await org.Campuses.PostAsync(new Campus 
        { 
            Name = $"Fixture-Campus-{_fixtureId}", 
            Street = "123 Easy Street", 
            City = "Oklahoma City", 
            State = "OK", 
            Zip = "73013", 
            Country = "United States", 
            Latitude = (decimal)35.61839, 
            Longitude = (decimal)-97.56967 
        });
		CampusId = campusResult.Data!.Id!;
	}

	public override async Task DisposeAsync()
	{
		var org = new PeopleClient(HttpClient).Latest;

		try { await org.Campuses.WithId(CampusId).DeleteAsync(); } catch { }
		try { await org.Tabs.WithId(TabId).DeleteAsync(); } catch { }
		try { await org.NoteCategories.WithId(NoteCategoryId).DeleteAsync(); } catch { }
		try { await org.Workflows.WithId(WorkflowId).DeleteAsync(); } catch { }
		try { await org.Households.WithId(HouseholdId).DeleteAsync(); } catch { }
		try { await org.People.WithId(PersonId).DeleteAsync(); } catch { }

		await base.DisposeAsync();
	}
}
