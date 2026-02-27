using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class CalendarCollection : ICollectionFixture<CalendarFixture>
{
	public const string Name = "Calendar";
}
