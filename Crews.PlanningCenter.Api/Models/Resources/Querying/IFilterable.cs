namespace Crews.PlanningCenter.Api.Models.Resources.Querying;

/// <summary>
/// Represents a resource collection that can be filtered.
/// </summary>
/// <typeparam name="TSelf">The implementing object type.</typeparam>
/// <typeparam name="TEnum">The enum representing available filters.</typeparam>
public interface IFilterable<TSelf, TEnum> where TEnum : Enum
{
	/// <summary>
	/// Filters the collection.
	/// </summary>
	/// <param name="filters">A list of filters.</param>
	/// <returns>An instance of T.</returns>
	TSelf FilterBy(params TEnum[] filters);
}
