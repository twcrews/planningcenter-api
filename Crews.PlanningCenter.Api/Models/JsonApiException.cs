using Crews.Web.JsonApiClient;
using System.Collections.Immutable;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Represents an error response from the Planning Center API.
/// </summary>
public class JsonApiException : HttpRequestException
{
    /// <summary>
    /// The colleciton of errors from the API response body.
    /// </summary>
    public ImmutableArray<JsonApiError> Errors { get; init; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonApiException"/> class.
    /// </summary>
    /// <param name="responseContent">The response body content from the API.</param>
    /// <param name="inner">The inner <see cref="HttpRequestException"/> instance.</param>
    public JsonApiException(string responseContent, HttpRequestException? inner)
        : base(inner?.Message, inner)
    {
        JsonApiDocument? document = JsonSerializer.Deserialize<JsonApiDocument>(responseContent);
        Errors = document?.Errors?.ToImmutableArray() ?? [];
    }
}
