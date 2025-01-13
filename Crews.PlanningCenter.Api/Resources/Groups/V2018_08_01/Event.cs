/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Groups.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2018_08_01;

/// <summary>
/// A fetchable Groups Event resource.
/// </summary>
public class EventResource
  : PlanningCenterSingletonFetchableResource<Event, EventResource, GroupsDocumentContext>,
  IIncludable<EventResource, EventIncludable>
{

  /// <summary>
  /// The related <see cref="AttendanceResourceCollection" />.
  /// </summary>
  public AttendanceResourceCollection Attendances => GetRelated<AttendanceResourceCollection>("attendances");

  /// <summary>
  /// The related <see cref="GroupResource" />.
  /// </summary>
  public GroupResource Group => GetRelated<GroupResource>("group");

  /// <summary>
  /// The related <see cref="LocationResource" />.
  /// </summary>
  public LocationResource Location => GetRelated<LocationResource>("location");

  /// <summary>
  /// The related <see cref="EventNoteResourceCollection" />.
  /// </summary>
  public EventNoteResourceCollection Notes => GetRelated<EventNoteResourceCollection>("notes");

  internal EventResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventResource Include(params EventIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Groups Event resources.
/// </summary>
public class EventResourceCollection
  : PlanningCenterPaginatedFetchableResource<Event, EventResourceCollection, EventResource, GroupsDocumentContext>,
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

