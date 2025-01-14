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
/// A fetchable Calendar EventInstance resource.
/// </summary>
public class EventInstanceResource
  : PlanningCenterSingletonFetchableResource<EventInstance, EventInstanceResource, CalendarDocumentContext>,
  IIncludable<EventInstanceResource, EventInstanceIncludable>
{

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="EventTimeResourceCollection" />.
  /// </summary>
  public EventTimeResourceCollection EventTimes => GetRelated<EventTimeResourceCollection>("event_times");

  /// <summary>
  /// The related <see cref="ResourceBookingResourceCollection" />.
  /// </summary>
  public ResourceBookingResourceCollection ResourceBookings => GetRelated<ResourceBookingResourceCollection>("resource_bookings");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal EventInstanceResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventInstanceResource Include(params EventInstanceIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar EventInstance resources.
/// </summary>
public class EventInstanceResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventInstance, EventInstanceResourceCollection, EventInstanceResource, CalendarDocumentContext>,
  IIncludable<EventInstanceResourceCollection, EventInstanceIncludable>,
  IOrderable<EventInstanceResourceCollection, EventInstanceOrderable>,
  IQueryable<EventInstanceResourceCollection, EventInstanceQueryable>
{
  internal EventInstanceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventInstanceResourceCollection Include(params EventInstanceIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public EventInstanceResourceCollection OrderBy(EventInstanceOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public EventInstanceResourceCollection Query(params (EventInstanceQueryable, string)[] queries)
    => base.Query(queries);
}

