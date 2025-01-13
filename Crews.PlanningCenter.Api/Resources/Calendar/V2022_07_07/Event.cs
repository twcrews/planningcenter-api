/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Entities;
using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2022_07_07;

/// <summary>
/// A fetchable Calendar Event resource.
/// </summary>
public class EventResource
  : PlanningCenterSingletonFetchableResource<Event, EventResource, CalendarDocumentContext>,
  IIncludable<EventResource, EventIncludable>
{

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  /// <summary>
  /// The related <see cref="ConflictResourceCollection" />.
  /// </summary>
  public ConflictResourceCollection Conflicts => GetRelated<ConflictResourceCollection>("conflicts");

  /// <summary>
  /// The related <see cref="EventConnectionResourceCollection" />.
  /// </summary>
  public EventConnectionResourceCollection EventConnections => GetRelated<EventConnectionResourceCollection>("event_connections");

  /// <summary>
  /// The related <see cref="EventInstanceResourceCollection" />.
  /// </summary>
  public EventInstanceResourceCollection EventInstances => GetRelated<EventInstanceResourceCollection>("event_instances");

  /// <summary>
  /// The related <see cref="EventResourceRequestResourceCollection" />.
  /// </summary>
  public EventResourceRequestResourceCollection EventResourceRequests => GetRelated<EventResourceRequestResourceCollection>("event_resource_requests");

  /// <summary>
  /// The related <see cref="FeedResource" />.
  /// </summary>
  public FeedResource Feed => GetRelated<FeedResource>("feed");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Owner => GetRelated<PersonResource>("owner");

  /// <summary>
  /// The related <see cref="ResourceBookingResourceCollection" />.
  /// </summary>
  public ResourceBookingResourceCollection ResourceBookings => GetRelated<ResourceBookingResourceCollection>("resource_bookings");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal EventResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventResource Include(params EventIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar Event resources.
/// </summary>
public class EventResourceCollection
  : PlanningCenterPaginatedFetchableResource<Event, EventResourceCollection, EventResource, CalendarDocumentContext>,
  IIncludable<EventResourceCollection, EventIncludable>,
  IQueryable<EventResourceCollection, EventQueryable>
{
  internal EventResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventResourceCollection Include(params EventIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public EventResourceCollection Query(params KeyValuePair<EventQueryable, string>[] queries)
    => base.Query(queries);
}

