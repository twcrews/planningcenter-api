using Crews.PlanningCenter.Api.Extensions;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Services;
using Crews.PlanningCenter.Api.Utility;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;

namespace Crews.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <summary>
/// A Planning Center resource that can be fetched from the API.
/// </summary>
public abstract class PlanningCenterFetchableResource<TSelf>(Uri uri) : PlanningCenterRemoteResource(uri)
	where TSelf : PlanningCenterFetchableResource<TSelf>
{
	/// <summary>
	/// Adds the given parameters to the end of the query string. The query string is not checked for duplicates.
	/// </summary>
	/// <param name="parameters">A collection of query string parameters.</param>
	/// <returns>This same instance of the request for call chaining.</returns>
	public virtual TSelf AppendCustomParameters(
		List<QueryString.QueryStringParameter> parameters)
	{
		GuardUri();
		QueryStringBuilder builder = new(Uri.Query);
		builder.Parameters.AddRange(parameters);
		Uri = Uri.SetQueryString(builder.QueryString);
		return (this as TSelf)!;
	}

	/// <summary>
	/// Removes the entire query string.
	/// </summary>
	/// <returns>This same instance of the request for call chaining.</returns>
	public virtual TSelf ClearAllParameters()
	{
		if (Uri == null) return (this as TSelf)!;
		Uri = Uri.ClearQueryString();
		return (this as TSelf)!;
	}

	/// <summary>
	/// Adds parameters to the URI query string. If a duplicate is found, the original is replaced.
	/// </summary>
	/// <param name="key">The parameter key.</param>
	/// <param name="values">The values assigned to the parameter.</param>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected TSelf AddParameters(string key, params string[] values)
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
		return (this as TSelf)!;
	}

	/// <summary>
	/// Adds includable resources to the request.
	/// </summary>
	/// <param name="includables">The includable resource types.</param>
	/// <typeparam name="TEnum">The enumerable associated with the includable resource types.</typeparam>
	/// <returns>This same instance of the request for call chaining.</returns>
	protected virtual TSelf Include<TEnum>(params TEnum[] includables)
		=> AddParameters("include", includables.Select(i => i.GetJsonApiName()).ToArray());

	/// <summary>
	/// Throws an exception if the URI property value is null.
	/// </summary>
	protected void GuardUri()
	{
		if (Uri == null) throw new NullReferenceException("Cannot append parameters because the request URI was null.");
	}

	/// <summary>
	/// Sends an HTTP request and returns the content as a JSON document.
	/// </summary>
	/// <param name="request">The request to send.</param>
	/// <returns>A JSON API resource document instance.</returns>
	protected async Task<Document> GetDocumentAsync(HttpRequestMessage request)
	{
		PlanningCenterApiClient client = new(new()
		{
			BaseAddress = new("https://api.planningcenteronline.com/")
		});

		HttpResponseMessage response = await client.SendAsync(request);
		return JsonObject.Parse<Document>(await response.Content.ReadAsStringAsync());
	}
}