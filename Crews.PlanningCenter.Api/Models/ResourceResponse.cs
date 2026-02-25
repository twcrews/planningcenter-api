using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Represents a strongly-typed JSON:API response wrapper.
/// </summary>
/// <typeparam name="T">The type of the resource data.</typeparam>
public abstract class ResourceResponse<T>
{
    /// <summary>
    /// The deserialized resource data from the response body.
    /// </summary>
    /// <remarks>
    /// This is the equivalent of the <see cref="JsonApiDocument.Data"/> property deserialized to its specific type.
    /// </remarks>
    public T? Data { get; init; }

    /// <summary>
    /// The deserialized JSON:API document from the response body.
    /// </summary>
    public JsonApiDocument? ResponseBody { get; init; }

    /// <summary>
    /// The original <see cref="HttpResponseMessage"/> instance.
    /// </summary>
    public HttpResponseMessage? ResponseMessage { get; init; }
}
