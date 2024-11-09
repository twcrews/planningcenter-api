using System.Net;
using Crews.PlanningCenter.Api.Extensions;
using Crews.PlanningCenter.Api.Utility;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using Newtonsoft.Json.Linq;

namespace Crews.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <summary>
/// A Planning Center resource that can be fetched from the API.
/// </summary>
public abstract class PlanningCenterFetchableResource<TSelf>(Uri uri, HttpClient client)
	: PlanningCenterRemoteResource(uri) where TSelf : PlanningCenterFetchableResource<TSelf>
{
	/// <summary>
	/// The <see cref="HttpClient"/> instance used to make requests to the Planning Center API.
	/// </summary>
	protected HttpClient Client { get; } = client;

	/// <summary>
	/// Adds the given parameters to the end of the query string. The query string is not checked for duplicates.
	/// </summary>
	/// <param name="parameters">A collection of query string parameters.</param>
	/// <returns>This same instance of the request for call chaining.</returns>
	public virtual TSelf AppendCustomParameters(
		List<QueryString.Parameter> parameters)
	{
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
		Uri = Uri.ClearQueryString();
		return (this as TSelf)!;
	}

	/// <summary>
	/// Adds parameters to the URI query string.
	/// </summary>
	/// <param name="key">The parameter key.</param>
	/// <param name="values">The values assigned to the parameter.</param>
	/// <returns>This same instance of the request for call chaining.</returns>
	/// <exception cref="ArgumentException">A parameter with the same name already exists.</exception>
	protected TSelf AddParameters(string key, params string[] values)
	{
		QueryString.Parameter newParameter = new()
		{
			Key = key,
			Values = [.. values]
		};

		QueryStringBuilder builder = new(Uri.Query);
		QueryString.Parameter? parameter = builder.Parameters
			.FirstOrDefault(p => p.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase));
		if (parameter == null)
		{
			builder.Parameters.Add(newParameter);
		}
		else
		{
			throw new ArgumentException("A parameter with the same name already exists.", nameof(key));
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
	/// Sends the given request and attempts to parse the response as a JSON:API document.
	/// </summary>
	/// <param name="request">The web request to send.</param>
	/// <returns>A Document object representing the response content.</returns>
	protected async Task<Document?> FetchDocumentAsync(HttpRequestMessage request)
	{
		HttpResponseMessage response = await Client.SendAsync(request);
		Document? document = await JsonObject.ParseAsync<Document>(await response.Content.ReadAsStringAsync());
		if (document == null) return null;

		HandleBadDocument(document);
		return document;
	}

	/// <summary>
	/// Creates a new <typeparamref name="TResource"/> instance and appends the value of <paramref name="path"/> to 
	/// the new <typeparamref name="TResource"/>'s <see cref="Uri"/> property.
	/// </summary>
	/// <typeparam name="TResource">
	/// The type of <see cref="PlanningCenterFetchableResource{TResource}"/> to return.
	/// </typeparam>
	/// <param name="path">The path to append to <typeparamref name="TResource"/>'s <see cref="Uri"/> property.</param>
	/// <returns>A new <typeparamref name="TResource"/> instance.</returns>
	protected TResource GetAssociated<TResource>(string path) where TResource : PlanningCenterFetchableResource<TResource>
		=> (TResource)Activator.CreateInstance(typeof(TResource), Uri.SafelyAppendPath(path), Client)!;

	/// <summary>
	/// Creates a new <typeparamref name="TNamedResource"/> instance and appends the <see cref="INamedApiResource.ApiName"/> 
	/// value belonging to <typeparamref name="TNamedResource"/> to its <see cref="Uri"/> property.
	/// </summary>
	/// <typeparam name="TNamedResource">
	/// The type of <see cref="PlanningCenterFetchableResource{TNamedResource}"/> to return.
	/// </typeparam>
	/// <returns>A new <typeparamref name="TNamedResource"/> instance.</returns>
	protected TNamedResource GetNamedAssociated<TNamedResource>()
		where TNamedResource : PlanningCenterFetchableResource<TNamedResource>, INamedApiResource
		=> (TNamedResource)Activator.CreateInstance(
			typeof(TNamedResource),
			Uri.SafelyAppendPath(TNamedResource.ApiName), Client)!;

	private static void HandleBadDocument(Document document)
	{
		if (!document.IsValidDocument())
		{
			throw new FormatException("The JSON:API document is not in a valid format.");
		}

		if (document.IsErrorsDocument()) HandleErrorDocument(document);
	}

	private static void HandleErrorDocument(Document document)
	{
		IEnumerable<Error> errors = GetErrorsFromDocument(document);
		IEnumerable<HttpRequestException> exceptions = errors
			.Select(error => new HttpRequestException(
				string.Join(": ", error.Title, error.Details), null, error.HttpStatusCode));

		if (exceptions.Count() == 1)
		{
			throw exceptions.Single();
		}

		throw new AggregateException(exceptions);
	}

	private static IEnumerable<Error> GetErrorsFromDocument(Document document)
	{
		return document.GetErrors().Select(error => new Error()
		{
			ID = error.Id,
			Links = error.Links,
			HttpStatusCode = error.Status == null
								? null
								: (HttpStatusCode)Convert.ToInt32(error.Status),
			ErrorCode = error.Code,
			Title = error.Title,
			Details = error.Detail,
			Source = GetErrorSource(error.Source),
			Metadata = error.Meta?.GetData<PlanningCenterErrorMetadata>()
		});
	}

	private static ErrorSource? GetErrorSource(JObject jsonApiSource)
	{
		JProperty? sourceProperty = jsonApiSource?.Properties().FirstOrDefault();
		if (sourceProperty == null) return null;
		if (sourceProperty.Value.Type != JTokenType.String) return null;

		string? sourceName = sourceProperty.Name;
		ErrorSourceType sourceType;

		switch (sourceName)
		{
			case "pointer":
				sourceType = ErrorSourceType.Pointer;
				break;
			case "parameter":
				sourceType = ErrorSourceType.Parameter;
				break;
			case "header":
				sourceType = ErrorSourceType.Header;
				break;
			default:
				return null;
		}

		return new()
		{
			Type = sourceType,
			Value = sourceProperty.Value.ToString()
		};
	}
}