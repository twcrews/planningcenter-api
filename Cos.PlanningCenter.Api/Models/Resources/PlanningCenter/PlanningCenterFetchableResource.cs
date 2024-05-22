using Cos.PlanningCenter.Api.Extensions;
using Cos.PlanningCenter.Api.Utility;

namespace Cos.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <summary>
/// A Planning Center resource that can be fetched from the API.
/// </summary>
public abstract class PlanningCenterFetchableResource(Uri uri) : PlanningCenterRemoteResource(uri)
{
	/// <summary>
	/// A dictionary used to define which resource types can be included with this resource.
	/// Each key represents an includable type, 
	/// </summary>
	protected Dictionary<Type, string> IncludableTypes { get; } = [];

	/// <summary>
	/// Adds the given parameters to the end of the query string. The query string is not checked for duplicates.
	/// </summary>
	/// <param name="parameters">A collection of query string parameters.</param>
	/// <returns>This same instance of the request for call chaining.</returns>
	public virtual PlanningCenterFetchableResource AppendCustomParameters(List<QueryString.QueryStringParameter> parameters)
	{
		GuardUri();
		QueryStringBuilder builder = new(Uri.Query);
		builder.Parameters.AddRange(parameters);
		Uri = Uri.SetQueryString(builder.QueryString);
		return this;
	}

	/// <summary>
	/// Removes the entire query string.
	/// </summary>
	/// <returns>This same instance of the request for call chaining.</returns>
	public virtual PlanningCenterFetchableResource ClearAllParameters()
	{
		if (Uri == null) return this;
		Uri = Uri.ClearQueryString();
		return this;
	}

	/// <summary>
	/// Adds an includable resource type to the query string. Should be wrapped by <c>IIncludable.Include()</c> in the 
	/// derived type.
	/// </summary>
	/// <typeparam name="U">The resource type to include.</typeparam>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected PlanningCenterFetchableResource Include<U>()
	{
		Type type = typeof(U);
		if (!IncludableTypes.TryGetValue(type, out string? value))
		{
			throw new ArgumentException("The resource type '{type.FullName}' cannot be included with this resource.");
		}
		return AddParameters("include", value);
	}

	/// <summary>
	/// Adds parameters to the URI query string. If a duplicate is found, the original is replaced.
	/// </summary>
	/// <param name="key">The parameter key.</param>
	/// <param name="values">The values assigned to the parameter.</param>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected PlanningCenterFetchableResource AddParameters(string key, params string[] values)
	{
		GuardUri();

		QueryString.QueryStringParameter newParameter = new()
		{
			Key = key,
			Values = [..values]
		};

		QueryStringBuilder builder = new(Uri!.Query);
		QueryString.QueryStringParameter? parameter = builder.Parameters
			.FirstOrDefault(p => p.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase));
		if (parameter == null)
		{
			builder.Parameters.Add(newParameter);
		}
		else
		{
			parameter = newParameter;
		}
		Uri = Uri.SetQueryString(builder.QueryString);
		return this;
	}

	/// <summary>
	/// Throws an exception if the URI property value is null.
	/// </summary>
	protected void GuardUri()
	{
		if (Uri == null) throw new NullReferenceException("Cannot append parameters because the request URI was null.");
	}
}
