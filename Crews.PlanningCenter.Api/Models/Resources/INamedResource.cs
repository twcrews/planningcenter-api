namespace Crews.PlanningCenter.Api.Models.Resources;

/// <summary>
/// Represents an API resource which has an identifying name defined in its API.
/// </summary>
public interface INamedApiResource
{
	/// <summary>
	/// The name of the resource as defined in its API.
	/// </summary>
	public static abstract string ApiName { get; }
}
