using System.Text.Json.Serialization;

namespace Cos.PlanningCenter.Api;

class PlanningCenterErrorResponse
{
	[JsonPropertyName("errors")]
	public required IEnumerable<PlanningCenterError> Errors { get; init; }
}

class PlanningCenterError
{
	[JsonPropertyName("status")]
	public required int Status { get; init; }

	[JsonPropertyName("title")]
	public required string Title { get; init; }

	[JsonPropertyName("code")]
	public required string Code { get; init; }

	[JsonPropertyName("detail")]
	public required string Details { get; init; }
}
