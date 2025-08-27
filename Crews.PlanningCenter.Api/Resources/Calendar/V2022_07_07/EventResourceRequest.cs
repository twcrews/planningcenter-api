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
/// A fetchable Calendar EventResourceRequest resource.
/// </summary>
public class EventResourceRequestResource
  : PlanningCenterSingletonFetchableResource<EventResourceRequest, EventResourceRequestResource, CalendarDocumentContext>,
  IIncludable<EventResourceRequestResource, EventResourceRequestIncludable>
{

  /// <summary>
  /// The related <see cref="EventResourceAnswerResourceCollection" />.
  /// </summary>
  public EventResourceAnswerResourceCollection Answers => GetRelated<EventResourceAnswerResourceCollection>("answers");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource CreatedBy => GetRelated<PersonResource>("created_by");

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="ResourceBookingResourceCollection" />.
  /// </summary>
  public ResourceBookingResourceCollection ResourceBookings => GetRelated<ResourceBookingResourceCollection>("resource_bookings");

  /// <summary>
  /// The related <see cref="ResourceResource" />.
  /// </summary>
  public ResourceResource Resource => GetRelated<ResourceResource>("resource");

  /// <summary>
  /// The related <see cref="RoomSetupResource" />.
  /// </summary>
  public RoomSetupResource RoomSetup => GetRelated<RoomSetupResource>("room_setup");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource UpdatedBy => GetRelated<PersonResource>("updated_by");

  internal EventResourceRequestResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventResourceRequestResource Include(params EventResourceRequestIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar EventResourceRequest resources.
/// </summary>
public class EventResourceRequestResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventResourceRequest, EventResourceRequestResourceCollection, EventResourceRequestResource, CalendarDocumentContext>,
  IIncludable<EventResourceRequestResourceCollection, EventResourceRequestIncludable>,
  IQueryable<EventResourceRequestResourceCollection, EventResourceRequestQueryable>
{
  internal EventResourceRequestResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventResourceRequestResourceCollection Include(params EventResourceRequestIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public EventResourceRequestResourceCollection Query(params (EventResourceRequestQueryable, string)[] queries)
    => base.Query(queries);
}

