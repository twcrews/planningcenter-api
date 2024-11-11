namespace Crews.PlanningCenter.Api.Models.Resources.Querying;

/// <summary>
/// Represents a resource collection that can be ordered.
/// </summary>
/// <typeparam name="TSelf">The implementing object type.</typeparam>
/// <typeparam name="TEnum">The enum representing available ordering attributes.</typeparam>
public interface IOrderable<TSelf, TEnum>
{
	/// <summary>
	/// Filters the collection.
	/// </summary>
	/// <param name="orderer">An ordering attribute.</param>
	/// <param name="order">The order in which to sort the <see cref="IOrderable{TSelf, TEnum}"/> items.</param>
	/// <returns>An instance of T.</returns>
	TSelf OrderBy(TEnum orderer, Order order);
}

/// <summary>
/// Represents an ordering method.
/// </summary>
public enum Order
{
	/// <summary>
	/// Sort items in ascending order.
	/// </summary>
	Ascending,

	/// <summary>
	/// Sort items in descending order.
	/// </summary>
	Descending
}
