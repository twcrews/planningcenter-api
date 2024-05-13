using System.Text.Json.Serialization;

namespace Cos.PlanningCenter.Api.Models;

public class PlanningCenterRootObject : PlanningCenterLinkedObject
{
	[JsonPropertyName("included")]
	public IEnumerable<PlanningCenterDataObject>? Included { get; init; }

	[JsonPropertyName("meta")]
	public PlanningCenterResponseMetadata? Metadata { get; init; }
}

public class PlanningCenterRootCollectionObject<T> : PlanningCenterRootObject where T : class
{
	[JsonPropertyName("data")]
	public required IEnumerable<PlanningCenterDataObject<T>> Data { get; init; }
}

public class PlanningCenterRootSingletonObject<T> : PlanningCenterRootObject where T : class
{
	[JsonPropertyName("data")]
	public required PlanningCenterDataObject<T> Data { get; init; }
}

public class PlanningCenterResponseMetadata
{
	[JsonPropertyName("can_include")]
	public IEnumerable<string> IncludableProperties { get; init; } = [];

	[JsonPropertyName("can_query_by")]
	public IEnumerable<string> QueriableAttributes { get; init; } = [];

	[JsonPropertyName("can_filter")]
	public IEnumerable<string> FilterableProperties { get; init; } = [];

	[JsonPropertyName("can_order_by")]
	public IEnumerable<string> OrderableAttributes { get; init; } = [];

	[JsonPropertyName("count")]
	public int? Count { get; init; }

	[JsonPropertyName("total_count")]
	public int? TotalCount { get; init; }

	[JsonPropertyName("parent")]
	public PlanningCenterDataObject? Parent { get; init; }

	[JsonPropertyName("next")]
	public PlanningCenterDataPage? NextPage { get; init; }

	[JsonPropertyName("prev")]
	public PlanningCenterDataPage? PreviousPage { get; init; }
}

public class PlanningCenterDataPage {
	[JsonPropertyName("offset")]
	public required int Offset { get; init; }
}

public class PlanningCenterDataObject : PlanningCenterLinkedObject
{
	[JsonPropertyName("type")]
	public string? @Type { get; init; }

	[JsonPropertyName("id")]
	public string? ID { get; init; }

	[JsonPropertyName("relationships")]
	public IDictionary<string, PlanningCenterRelationship>? Relationships { get; init; }
}

public class PlanningCenterDataObject<T> : PlanningCenterDataObject
{
	[JsonPropertyName("attributes")]
	public required T Attributes { get; init; }
}

public class PlanningCenterLinkedObject
{
	[JsonPropertyName("links")]
	public IDictionary<string, Uri>? Links { get; init; }
}

public class PlanningCenterRelationship : PlanningCenterLinkedObject
{
	[JsonPropertyName("data")]
	public required PlanningCenterDataObject Data { get; init; }
}