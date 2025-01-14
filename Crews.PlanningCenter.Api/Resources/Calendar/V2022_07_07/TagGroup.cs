/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Entities;
using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2022_07_07;

/// <summary>
/// A fetchable Calendar TagGroup resource.
/// </summary>
public class TagGroupResource
  : PlanningCenterSingletonFetchableResource<TagGroup, TagGroupResource, CalendarDocumentContext>,
  IIncludable<TagGroupResource, TagGroupIncludable>
{

  /// <summary>
  /// The related <see cref="EventResourceCollection" />.
  /// </summary>
  public EventResourceCollection Events => GetRelated<EventResourceCollection>("events");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal TagGroupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public TagGroupResource Include(params TagGroupIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<TagGroup>> PostAsync(TagGroup resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<TagGroup>> PatchAsync(TagGroup resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Calendar TagGroup resources.
/// </summary>
public class TagGroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<TagGroup, TagGroupResourceCollection, TagGroupResource, CalendarDocumentContext>,
  IIncludable<TagGroupResourceCollection, TagGroupIncludable>,
  IOrderable<TagGroupResourceCollection, TagGroupOrderable>,
  IQueryable<TagGroupResourceCollection, TagGroupQueryable>
{
  internal TagGroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TagGroupResourceCollection Include(params TagGroupIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public TagGroupResourceCollection OrderBy(TagGroupOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public TagGroupResourceCollection Query(params (TagGroupQueryable, string)[] queries)
    => base.Query(queries);
}

