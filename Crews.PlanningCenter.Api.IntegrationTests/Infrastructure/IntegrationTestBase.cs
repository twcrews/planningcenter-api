namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

[Collection(PlanningCenterCollection.Name)]
public abstract class IntegrationTestBase(PlanningCenterFixture fixture)
{
	protected HttpClient HttpClient => fixture.HttpClient;

	protected Uri BaseUri => fixture.HttpClient.BaseAddress!;

	protected string UniqueId { get; } = Guid.NewGuid().ToString("N")[..8];
}
