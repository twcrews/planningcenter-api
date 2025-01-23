using JsonApiFramework.JsonApi;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Represents a response object from a JSON API.
/// </summary>
public abstract class JsonApiResponse
{
	/// <summary>
	/// The metadata associated with the response.
	/// </summary>
	public JsonApiMetadata? Metadata { get; init; }

	/// <summary>
	/// The original, unmodified HTTP response.
	/// </summary>
	public required HttpResponseMessage RawResponse { get; init; }

	/// <summary>
	/// The JSON API document constructed from the response.
	/// </summary>
	public Document? JsonApiDocument { get; init; }
}

/// <summary>
/// Represents a singleton response object from a JSON API.
/// </summary>
/// <typeparam name="T">The type of data returned.</typeparam>
public class JsonApiSingletonResponse<T> : JsonApiResponse
{
	/// <summary>
	/// The data content of the response.
	/// </summary>
	public T? Data { get; init; }
}

/// <summary>
/// Represents a collection response object from a JSON API.
/// </summary>
/// <typeparam name="T">The type of data returned.</typeparam>
public class JsonApiCollectionResponse<T> : JsonApiResponse
{	
	/// <summary>
	/// The data content of the collection.
	/// </summary>
	public IEnumerable<T> Data { get; init; } = [];
}