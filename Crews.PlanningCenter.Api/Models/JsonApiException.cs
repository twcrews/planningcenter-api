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
        : this(ParseErrors(responseContent), inner)
    {
    }

    private JsonApiException(ImmutableArray<JsonApiError> errors, HttpRequestException? inner)
        : base(BuildMessage(errors, inner), inner)
    {
        Errors = errors;
    }

    private static ImmutableArray<JsonApiError> ParseErrors(string responseContent)
    {
        JsonApiDocument? document = JsonSerializer.Deserialize<JsonApiDocument>(responseContent);
        return document?.Errors?.ToImmutableArray() ?? [];
    }

    private static string? BuildMessage(ImmutableArray<JsonApiError> errors, HttpRequestException? inner)
    {
        if (errors.IsEmpty) return inner?.Message;

        string details = string.Join("; ", errors.Select(error => string.Join(": ",
            new[] { error.Title, error.Detail }.Where(text => !string.IsNullOrEmpty(text)))));

        return inner is null ? details : $"{inner.Message} {details}";
    }
}
