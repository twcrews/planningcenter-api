using Crews.PlanningCenter.Api.Extensions;
using JsonApiFramework.Client;
using JsonApiFramework.Conventions;
using JsonApiFramework.JsonApi;

namespace Crews.PlanningCenter.Api;

/// <summary>
/// Base context for all Planning Center APIs.
/// </summary>
public abstract class PlanningCenterDocumentContext : DocumentContext
{
	/// <summary>
	/// Instantiates a new generic Planning Center document context.
	/// </summary>
	protected PlanningCenterDocumentContext() : base() {}

	/// <summary>
	/// Instantiates a new generic Planning Center document context using the provided document.
	/// </summary>
/// <param name="document">The document to use when instantiating the context.</param>
	protected PlanningCenterDocumentContext(Document document) : base(document) {}

	/// <summary>
	/// Adds Planning Center conventions to the context.
	/// </summary>
	/// <param name="conventionSetBuilder">The builder to use when creating conventions.</param>
	protected override void OnConventionsCreating(IConventionsBuilder conventionSetBuilder)
	{
		base.OnConventionsCreating(conventionSetBuilder);

		conventionSetBuilder.ApiTypeNamingConventions()
			.AddPascalCaseNamingConvention();
		conventionSetBuilder.ApiAttributeNamingConventions()
			.AddSnakeCaseNamingConvention();
		conventionSetBuilder.ResourceTypeConventions()
			.AddPropertyDiscoveryConvention();
		conventionSetBuilder.ComplexTypeConventions()
			.AddPropertyDiscoveryConvention();
	}
}
