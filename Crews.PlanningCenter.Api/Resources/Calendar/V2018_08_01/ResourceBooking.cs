/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Calendar.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2018_08_01;

/// <summary>
/// A fetchable Calendar ResourceBooking resource.
/// </summary>
public class ResourceBookingResource
  : PlanningCenterSingletonFetchableResource<ResourceBooking, ResourceBookingResource, CalendarDocumentContext>,
  IIncludable<ResourceBookingResource, ResourceBookingIncludable>
{

  /// <summary>
  /// The related <see cref="EventInstanceResource" />.
  /// </summary>
  public EventInstanceResource EventInstance => GetRelated<EventInstanceResource>("event_instance");

  /// <summary>
  /// The related <see cref="EventResourceRequestResource" />.
  /// </summary>
  public EventResourceRequestResource EventResourceRequest => GetRelated<EventResourceRequestResource>("event_resource_request");

  /// <summary>
  /// The related <see cref="ResourceResource" />.
  /// </summary>
  public ResourceResource Resource => GetRelated<ResourceResource>("resource");

  internal ResourceBookingResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ResourceBookingResource Include(params ResourceBookingIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar ResourceBooking resources.
/// </summary>
public class ResourceBookingResourceCollection
  : PlanningCenterPaginatedFetchableResource<ResourceBooking, ResourceBookingResourceCollection, ResourceBookingResource, CalendarDocumentContext>,
  IIncludable<ResourceBookingResourceCollection, ResourceBookingIncludable>,
  IOrderable<ResourceBookingResourceCollection, ResourceBookingOrderable>,
  IQueryable<ResourceBookingResourceCollection, ResourceBookingQueryable>
{
  internal ResourceBookingResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ResourceBookingResourceCollection Include(params ResourceBookingIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ResourceBookingResourceCollection OrderBy(ResourceBookingOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ResourceBookingResourceCollection Query(params KeyValuePair<ResourceBookingQueryable, string>[] queries)
    => base.Query(queries);
}

