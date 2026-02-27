using Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.ProductFixtures;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure.Collections;

[CollectionDefinition(Name)]
public class RegistrationsCollection : ICollectionFixture<RegistrationsFixture>
{
	public const string Name = "Registrations";
}
