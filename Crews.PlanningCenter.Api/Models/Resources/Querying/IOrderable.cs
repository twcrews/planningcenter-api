namespace Crews.PlanningCenter.Api.Models.Resources.Querying;

/// <summary>
/// Represents a resource collection that can be ordered.
/// </summary>
/// <typeparam name="T">The implementing object type.</typeparam>
/// <typeparam name="TEnum">The enum representing available ordering attributes.</typeparam>
public interface IOrderable<T, TEnum>
{
	/// <summary>
	/// Filters the collection.
	/// </summary>
	/// <param name="orderer">An ordering attribute.</param>
	/// <returns>An instance of T.</returns>
	T OrderBy(TEnum orderer);
}
