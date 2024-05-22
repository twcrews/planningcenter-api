namespace Cos.PlanningCenter.Api.Models.Resources.Querying;

/// <summary>
/// Represents a resource or resource collection that can include other resources.
/// </summary>
/// <typeparam name="T">The implementing object type.</typeparam>
/// <typeparam name="TEnum">The enum representing available includable resources.</typeparam>
public interface IIncludable<T, TEnum>
{
	/// <summary>
	/// Includes the given resources.
	/// </summary>
	/// <param name="included">A list of includable resources.</param>
	/// <returns>An instance of T.</returns>
	T Include(params TEnum[] included);
}