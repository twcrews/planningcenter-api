using System.Text.Json.Serialization;

namespace Cos.PlanningCenter.Api.Models;

/// <summary>
/// Represents a type-agnostic root level Planning Center object.
/// </summary>
public class PlanningCenterRootObject : PlanningCenterLinkedObject
{
	/// <summary>
	/// Planning Center data objects included in an API response.
	/// </summary>
	[JsonPropertyName("included")]
	public IEnumerable<PlanningCenterDataObject>? Included { get; init; }

	/// <summary>
	/// Metadata included in an API response.
	/// </summary>
	[JsonPropertyName("meta")]
	public PlanningCenterResponseMetadata? Metadata { get; init; }
}

/// <summary>
/// Represents a root level Planning Center object containing a collection of data objects.
/// </summary>
/// <typeparam name="T">The type of data object.</typeparam>
public class PlanningCenterRootCollectionObject<T> : PlanningCenterRootObject where T : class
{
	/// <summary>
	/// The collection of data objects in the request or response object.
	/// </summary>
	[JsonPropertyName("data")]
	public required IEnumerable<PlanningCenterDataObject<T>> Data { get; init; }
}

/// <summary>
/// Represents a root level Planning Center object containing a single data objects.
/// </summary>
/// <typeparam name="T">The type of data object.</typeparam>
public class PlanningCenterRootSingletonObject<T> : PlanningCenterRootObject where T : class
{
	/// <summary>
	/// The data object in the request or response object.
	/// </summary>
	[JsonPropertyName("data")]
	public required PlanningCenterDataObject<T> Data { get; init; }
}

/// <summary>
/// Represents metadata returned in a Planning Center API response.
/// </summary>
public class PlanningCenterResponseMetadata
{
	/// <summary>
	/// Names of Planning Center data object types that can be included in the API response.
	/// </summary>
	[JsonPropertyName("can_include")]
	public IEnumerable<string> IncludableProperties { get; init; } = [];

	/// <summary>
	/// Names of attributes of the current data object type that can be used for querying the collection of data objects.
	/// </summary>
	[JsonPropertyName("can_query_by")]
	public IEnumerable<string> QueriableAttributes { get; init; } = [];

	/// <summary>
	/// Names of properties that can be used to filter the collection of data objects.
	/// </summary>
	[JsonPropertyName("can_filter")]
	public IEnumerable<string> FilterableProperties { get; init; } = [];

	/// <summary>
	/// Names of attributes of the current data object type that can be used for ordering the collection of data objects.
	/// </summary>
	[JsonPropertyName("can_order_by")]
	public IEnumerable<string> OrderableAttributes { get; init; } = [];

	/// <summary>
	/// The number of data objects returned in the current API response object. If this number is less than `TotalCount`,
	/// modifying the page offset of the request will return a different subset of paginated objects from this total.
	/// </summary>
	[JsonPropertyName("count")]
	public int? Count { get; init; }

	/// <summary>
	/// The total number of paginated data objects available based on the API request. If this number is greater than 
	/// `Count`, modifying the page offset of the request will return a different subset of paginated objects from this 
	/// total.
	/// </summary>
	[JsonPropertyName("total_count")]
	public int? TotalCount { get; init; }

	/// <summary>
	/// The parent data object of the data object or collection of data objects in the current API response.
	/// </summary>
	[JsonPropertyName("parent")]
	public PlanningCenterDataObject? Parent { get; init; }

	/// <summary>
	/// The next page of paginated data objects.
	/// </summary>
	[JsonPropertyName("next")]
	public PlanningCenterDataPage? NextPage { get; init; }

	/// <summary>
	/// The previous page of paginated data objects.
	/// </summary>
	[JsonPropertyName("prev")]
	public PlanningCenterDataPage? PreviousPage { get; init; }
}

/// <summary>
/// Represents a page in a Planning Center API responses containing a paginated collection of data objects.
/// </summary>
public class PlanningCenterDataPage {
	/// <summary>
	/// The index of the start of the page.
	/// </summary>
	[JsonPropertyName("offset")]
	public required int Offset { get; init; }
}

/// <summary>
/// Represents a type-agnostic Planning Center data object along with its identification metadata.
/// </summary>
public class PlanningCenterDataObject : PlanningCenterLinkedObject
{
	/// <summary>
	/// The name of the data object's type as defined in Planning Center.
	/// </summary>
	[JsonPropertyName("type")]
	public string? @Type { get; init; }

	/// <summary>
	/// The unique ID of the data object. This value is only guaranteed to be unique among other data objects of the same
	/// type.
	/// </summary>
	[JsonPropertyName("id")]
	public string? ID { get; init; }

	/// <summary>
	/// Data objects related to the current data object.
	/// </summary>
	[JsonPropertyName("relationships")]
	public IDictionary<string, PlanningCenterRelationship>? Relationships { get; init; }
}

/// <summary>
/// Represents a Planning Center data object along with its identification metadata.
/// </summary>
/// <typeparam name="T">The type of data object.</typeparam>
public class PlanningCenterDataObject<T> : PlanningCenterDataObject
{
	/// <summary>
	/// Attributes specific to the current data object's type.
	/// </summary>
	[JsonPropertyName("attributes")]
	public required T Attributes { get; init; }
}

/// <summary>
/// Represents any Planning Center object that can be linked to itself or to other objects.
/// </summary>
public class PlanningCenterLinkedObject
{
	/// <summary>
	/// A collection of URIs relating to the object, often including a link to the object itself.
	/// </summary>
	[JsonPropertyName("links")]
	public IDictionary<string, Uri>? Links { get; init; }
}

/// <summary>
/// Represents a relationship carried by a Planning Center data object.
/// </summary>
public class PlanningCenterRelationship : PlanningCenterLinkedObject
{
	/// <summary>
	/// The related data object. Contains only identification metadata
	/// </summary>
	[JsonPropertyName("data")]
	public required PlanningCenterDataObject Data { get; init; }
}