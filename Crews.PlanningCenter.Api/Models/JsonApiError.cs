using System.Net;
using JsonApiFramework.JsonApi;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Represents an error object returned in a JSON API error response.
/// </summary>
class JsonApiError
{
	public string? ID { get; set; }

	public Links? Links { get; set; }

	/// <summary>
	/// The status code of the error object. This should be equivalent to the API response's status code.
	/// </summary>
	public HttpStatusCode? HttpStatusCode { get; init; }

	/// <summary>
	/// A string identifying the type of error as defined by JSON.
	/// </summary>
	public string? ErrorCode { get; init; }

	/// <summary>
	/// The title of the error message.
	/// </summary>
	public string? Title { get; init; }

	/// <summary>
	/// A message detailing the error.
	/// </summary>
	public string? Details { get; init; }

	public ErrorSource? Source { get; set; }

	/// <summary>
	/// Metadata associated with the error.
	/// </summary>
	public PlanningCenterErrorMetadata? Metadata { get; init; }
}

class ErrorSource
{
	public required string Value { get; set; }
	public required ErrorSourceType Type { get; set; }
}

enum ErrorSourceType
{
	Pointer,
	Parameter,
	Header
}

/// <summary>
/// Represents metadata that may be included in a JSON API error response.
/// </summary>
class PlanningCenterErrorMetadata
{
	/// <summary>
	/// A more specific description of the error.
	/// </summary>
	public string? Description { get; init; }
}
