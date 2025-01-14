/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2024_11_07.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2024_11_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2024_11_07;

/// <summary>
/// A fetchable CheckIns EventTime resource.
/// </summary>
public class EventTimeResource
  : PlanningCenterSingletonFetchableResource<EventTime, EventTimeResource, CheckInsDocumentContext>,
  IIncludable<EventTimeResource, EventTimeIncludable>
{

  /// <summary>
  /// The related <see cref="LocationResourceCollection" />.
  /// </summary>
  public LocationResourceCollection AvailableLocations => GetRelated<LocationResourceCollection>("available_locations");

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="EventPeriodResource" />.
  /// </summary>
  public EventPeriodResource EventPeriod => GetRelated<EventPeriodResource>("event_period");

  /// <summary>
  /// The related <see cref="HeadcountResourceCollection" />.
  /// </summary>
  public HeadcountResourceCollection Headcounts => GetRelated<HeadcountResourceCollection>("headcounts");

  /// <summary>
  /// The related <see cref="LocationEventTimeResourceCollection" />.
  /// </summary>
  public LocationEventTimeResourceCollection LocationEventTimes => GetRelated<LocationEventTimeResourceCollection>("location_event_times");

  internal EventTimeResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventTimeResource Include(params EventTimeIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns EventTime resources.
/// </summary>
public class EventTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventTime, EventTimeResourceCollection, EventTimeResource, CheckInsDocumentContext>,
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
  public EventTimeResourceCollection Query(params (EventTimeQueryable, string)[] queries)
    => base.Query(queries);
}

