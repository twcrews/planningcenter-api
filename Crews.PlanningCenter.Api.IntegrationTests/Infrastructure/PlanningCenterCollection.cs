namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

[CollectionDefinition(Name)]
public class PlanningCenterCollection : ICollectionFixture<PlanningCenterFixture>
{
	public const string Name = "PlanningCenter";
}
