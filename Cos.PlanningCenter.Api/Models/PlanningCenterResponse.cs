using System.Text.Json.Serialization;

namespace Cos.PlanningCenter.Api.Models;

public abstract class PlanningCenterRootObject : PlanningCenterLinkedObject
{
	[JsonPropertyName("included")]
	public abstract IEnumerable<PlanningCenterDataObject> Included { get; init; }

	[JsonPropertyName("meta")]
	public required PlanningCenterResponseMetadata Metadata { get; init; }
}

public abstract class PlanningCenterRootCollectionObject<T> : PlanningCenterRootObject where T : class
{
	[JsonPropertyName("data")]
	public required IEnumerable<PlanningCenterDataObject<T>> Data { get; init; }
}

public abstract class PlanningCenterRootSingletonObject<T> : PlanningCenterRootObject where T : class
{
	[JsonPropertyName("data")]
	public required PlanningCenterDataObject<T> Data { get; init; }
}

public class PlanningCenterResponseMetadata
{
	[JsonPropertyName("can_include")]
	public IEnumerable<string> IncludableProperties { get; init; } = [];

	[JsonPropertyName("can_query_by")]
	public IEnumerable<string> QueriableProperties { get; init; } = [];

	[JsonPropertyName("can_filter_by")]
	public IEnumerable<string> FilterableProperties { get; init; } = [];

	[JsonPropertyName("can_order_by")]
	public IEnumerable<string> OrderableProperties { get; init; } = [];

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

public abstract class PlanningCenterDataObject : PlanningCenterLinkedObject
{
	[JsonPropertyName("type")]
	public required string @Type { get; init; }

	[JsonPropertyName("id")]
	public required string ID { get; init; }

	[JsonPropertyName("relationships")]
	public IDictionary<string, PlanningCenterDataObject>? Relationships { get; init; }
}

public abstract class PlanningCenterDataObject<T> : PlanningCenterDataObject
{
	[JsonPropertyName("attributes")]
	public required T Attributes { get; init; }
}

public class PlanningCenterLinkedObject
{
	[JsonPropertyName("links")]
	public IDictionary<string, Uri>? Links { get; init; }
}
