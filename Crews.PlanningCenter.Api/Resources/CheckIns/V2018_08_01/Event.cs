/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2018_08_01;

/// <summary>
/// A fetchable CheckIns Event resource.
/// </summary>
public class EventResource
  : PlanningCenterSingletonFetchableResource<Event, EventResource, CheckInsDocumentContext>,
  IIncludable<EventResource, EventIncludable>
{

  /// <summary>
  /// The related <see cref="AttendanceTypeResourceCollection" />.
  /// </summary>
  public AttendanceTypeResourceCollection AttendanceTypes => GetRelated<AttendanceTypeResourceCollection>("attendance_types");

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="EventTimeResourceCollection" />.
  /// </summary>
  public EventTimeResourceCollection CurrentEventTimes => GetRelated<EventTimeResourceCollection>("current_event_times");

  /// <summary>
  /// The related <see cref="EventLabelResourceCollection" />.
  /// </summary>
  public EventLabelResourceCollection EventLabels => GetRelated<EventLabelResourceCollection>("event_labels");

  /// <summary>
  /// The related <see cref="EventPeriodResourceCollection" />.
  /// </summary>
  public EventPeriodResourceCollection EventPeriods => GetRelated<EventPeriodResourceCollection>("event_periods");

  /// <summary>
  /// The related <see cref="IntegrationLinkResourceCollection" />.
  /// </summary>
  public IntegrationLinkResourceCollection IntegrationLinks => GetRelated<IntegrationLinkResourceCollection>("integration_links");

  /// <summary>
  /// The related <see cref="LocationResourceCollection" />.
  /// </summary>
  public LocationResourceCollection Locations => GetRelated<LocationResourceCollection>("locations");

  /// <summary>
  /// The related <see cref="PersonEventResourceCollection" />.
  /// </summary>
  public PersonEventResourceCollection PersonEvents => GetRelated<PersonEventResourceCollection>("person_events");

  internal EventResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventResource Include(params EventIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns Event resources.
/// </summary>
public class EventResourceCollection
  : PlanningCenterPaginatedFetchableResource<Event, EventResourceCollection, EventResource, CheckInsDocumentContext>,
  IIncludable<EventResourceCollection, EventIncludable>,
  IOrderable<EventResourceCollection, EventOrderable>,
  IQueryable<EventResourceCollection, EventQueryable>
{
  internal EventResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventResourceCollection Include(params EventIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public EventResourceCollection OrderBy(EventOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public EventResourceCollection Query(params KeyValuePair<EventQueryable, string>[] queries)
    => base.Query(queries);
}

