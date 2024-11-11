namespace Crews.PlanningCenter.Api.Attributes;

/// <summary>
/// An attribute used to apply a JSON API serial name to an element.
/// </summary>
/// <remarks>
/// Applies the JSON API name attribute to an element.
/// </remarks>
/// <param name="name">The JSON API name of the element.</param>
[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class JsonApiNameAttribute(string name) : Attribute
{
	/// <summary>
	/// The JSON API name associated with the element.
	/// </summary>
	/// <value></value>
	public string Name { get; } = name;
}
