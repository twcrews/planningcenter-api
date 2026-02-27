using Crews.PlanningCenter.Api.Calendar.V2022_07_07;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;
using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.TestBases;

[Collection(CalendarCollection.Name)]
[Trait("Product", "Calendar")]
public abstract class CalendarTestBase(CalendarFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;
	protected CalendarFixture Fixture => fixture;
	protected OrganizationClient Org => new CalendarClient(HttpClient).Latest;
	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
