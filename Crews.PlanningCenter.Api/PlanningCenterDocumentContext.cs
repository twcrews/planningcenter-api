using Crews.PlanningCenter.Api.Extensions;
using JsonApiFramework.Client;
using JsonApiFramework.Conventions;

namespace Crews.PlanningCenter.Api;

/// <summary>
/// Base context for all Planning Center APIs.
/// </summary>
public abstract class PlanningCenterDocumentContext : DocumentContext
{
	/// <summary>
	/// Adds Planning Center conventions to the context.
	/// </summary>
	/// <param name="conventionSetBuilder">The builder to use when creating conventions.</param>
	protected override void OnConventionsCreating(IConventionsBuilder conventionSetBuilder)
	{
		conventionSetBuilder.ApiTypeNamingConventions()
			.AddPascalCaseNamingConvention();
		conventionSetBuilder.ApiAttributeNamingConventions()
			.AddSnakeCaseNamingConvention();
		conventionSetBuilder.ResourceTypeConventions()
			.AddPropertyDiscoveryConvention();
	}
}
