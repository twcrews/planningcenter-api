using System.Text.Json.Serialization;

namespace Cos.PlanningCenter.Api;

/// <summary>
/// Represents an error response body from the Planning Center API.
/// </summary>
class PlanningCenterErrorResponse
{
	/// <summary>
	/// The collection of error objects in the API error response.
	/// </summary>
	[JsonPropertyName("errors")]
	public required IEnumerable<PlanningCenterError> Errors { get; init; }
}

/// <summary>
/// Represents an error object returned in a Planning Center API error response.
/// </summary>
class PlanningCenterError
{
	/// <summary>
	/// The status code of the error object. This should be equivalent to the API response's status code.
	/// </summary>
	[JsonPropertyName("status")]
	public required int HttpStatusCode { get; init; }

	/// <summary>
	/// The title of the error message.
	/// </summary>
	[JsonPropertyName("title")]
	public required string Title { get; init; }

	/// <summary>
	/// A string identifying the type of error as defined by Planning Center.
	/// </summary>
	[JsonPropertyName("code")]
	public string? ErrorCode { get; init; }

	/// <summary>
	/// A message detailing the error.
	/// </summary>
	[JsonPropertyName("detail")]
	public required string Details { get; init; }
}
