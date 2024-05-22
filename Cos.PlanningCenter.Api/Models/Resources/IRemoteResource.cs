namespace Cos.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents a fetchable resource.
/// </summary>
public interface IRemoteResource
{
	/// <summary>
	/// The URI referencing the resource.
	/// </summary>
	public Uri Uri { get; }
}
