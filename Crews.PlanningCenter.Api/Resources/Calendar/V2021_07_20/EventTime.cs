/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Entities;
using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2021_07_20;

/// <summary>
/// A fetchable Calendar EventTime resource.
/// </summary>
public class EventTimeResource
  : PlanningCenterSingletonFetchableResource<EventTime, EventTimeResource, CalendarDocumentContext>,
  IIncludable<EventTimeResource, EventTimeIncludable>
{

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  internal EventTimeResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventTimeResource Include(params EventTimeIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar EventTime resources.
/// </summary>
public class EventTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventTime, EventTimeResourceCollection, EventTimeResource, CalendarDocumentContext>,
  IIncludable<EventTimeResourceCollection, EventTimeIncludable>,
  IOrderable<EventTimeResourceCollection, EventTimeOrderable>,
  IQueryable<EventTimeResourceCollection, EventTimeQueryable>
{
  internal EventTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventTimeResourceCollection Include(params EventTimeIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public EventTimeResourceCollection OrderBy(EventTimeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public EventTimeResourceCollection Query(params KeyValuePair<EventTimeQueryable, string>[] queries)
    => base.Query(queries);
}

