/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2020_04_08.Entities;
using Crews.PlanningCenter.Models.Calendar.V2020_04_08.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2020_04_08;

/// <summary>
/// A fetchable Calendar Tag resource.
/// </summary>
public class TagResource
  : PlanningCenterSingletonFetchableResource<Tag, TagResource, CalendarDocumentContext>,
  IIncludable<TagResource, TagIncludable>
{

  /// <summary>
  /// The related <see cref="EventInstanceResourceCollection" />.
  /// </summary>
  public EventInstanceResourceCollection EventInstances => GetRelated<EventInstanceResourceCollection>("event_instances");

  /// <summary>
  /// The related <see cref="EventResourceCollection" />.
  /// </summary>
  public EventResourceCollection Events => GetRelated<EventResourceCollection>("events");

  /// <summary>
  /// The related <see cref="TagGroupResource" />.
  /// </summary>
  public TagGroupResource TagGroup => GetRelated<TagGroupResource>("tag_group");

  internal TagResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public TagResource Include(params TagIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar Tag resources.
/// </summary>
public class TagResourceCollection
  : PlanningCenterPaginatedFetchableResource<Tag, TagResourceCollection, TagResource, CalendarDocumentContext>,
  IIncludable<TagResourceCollection, TagIncludable>,
  IOrderable<TagResourceCollection, TagOrderable>,
  IQueryable<TagResourceCollection, TagQueryable>
{
  internal TagResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TagResourceCollection Include(params TagIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public TagResourceCollection OrderBy(TagOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public TagResourceCollection Query(params KeyValuePair<TagQueryable, string>[] queries)
    => base.Query(queries);
}

