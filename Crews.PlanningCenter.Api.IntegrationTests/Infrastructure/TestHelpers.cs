using JsonApiFramework.JsonApi;

namespace Crews.PlanningCenter.Api.IntegrationTests.Infrastructure;

/// <summary>
/// Helper methods for integration tests.
/// </summary>
public static class TestHelpers
{
	/// <summary>
	/// Asserts that a JSON:API document is valid and contains data.
	/// </summary>
	public static void AssertJsonApiDocument(Document? document)
	{
		Assert.NotNull(document);
		Assert.NotNull(document.JsonApiVersion);
	}

	/// <summary>
	/// Asserts that included resources are present.
	/// </summary>
	public static void AssertHasIncludedResources(Document? document)
	{
		Assert.NotNull(document);

		var included = document.GetIncludedResources();
		Assert.NotNull(included);
		Assert.NotEmpty(included);
	}
}
