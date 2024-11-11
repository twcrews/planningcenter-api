namespace Crews.PlanningCenter.Api.Models.Resources.Querying;

/// <summary>
/// Represents a resource collection that can be queried.
/// </summary>
/// <typeparam name="TSelf">The implementing object type.</typeparam>
/// <typeparam name="TEnum">The enum representing queryable attributes.</typeparam>
public interface IQueryable<TSelf, TEnum>
{
	/// <summary>
	/// Queries the resource with the given parameters.
	/// </summary>
	/// <param name="queries">A list of queries.</param>
	/// <returns>An instance of T.</returns>
	TSelf Query(params KeyValuePair<TEnum, string>[] queries);
}