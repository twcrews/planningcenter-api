/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2023_04_05.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2023_04_05.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2023_04_05;

/// <summary>
/// A fetchable CheckIns EventPeriod resource.
/// </summary>
public class EventPeriodResource
  : PlanningCenterSingletonFetchableResource<EventPeriod, EventPeriodResource, CheckInsDocumentContext>,
  IIncludable<EventPeriodResource, EventPeriodIncludable>
{

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="EventTimeResourceCollection" />.
  /// </summary>
  public EventTimeResourceCollection EventTimes => GetRelated<EventTimeResourceCollection>("event_times");

  /// <summary>
  /// The related <see cref="LocationEventPeriodResourceCollection" />.
  /// </summary>
  public LocationEventPeriodResourceCollection LocationEventPeriods => GetRelated<LocationEventPeriodResourceCollection>("location_event_periods");

  internal EventPeriodResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventPeriodResource Include(params EventPeriodIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns EventPeriod resources.
/// </summary>
public class EventPeriodResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventPeriod, EventPeriodResourceCollection, EventPeriodResource, CheckInsDocumentContext>,
  IIncludable<EventPeriodResourceCollection, EventPeriodIncludable>,
  IOrderable<EventPeriodResourceCollection, EventPeriodOrderable>,
  IQueryable<EventPeriodResourceCollection, EventPeriodQueryable>
{
  internal EventPeriodResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventPeriodResourceCollection Include(params EventPeriodIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public EventPeriodResourceCollection OrderBy(EventPeriodOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public EventPeriodResourceCollection Query(params KeyValuePair<EventPeriodQueryable, string>[] queries)
    => base.Query(queries);
}

