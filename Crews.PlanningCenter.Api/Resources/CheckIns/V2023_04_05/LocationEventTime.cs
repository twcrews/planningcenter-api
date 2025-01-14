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
/// A fetchable CheckIns LocationEventTime resource.
/// </summary>
public class LocationEventTimeResource
  : PlanningCenterSingletonFetchableResource<LocationEventTime, LocationEventTimeResource, CheckInsDocumentContext>,
  IIncludable<LocationEventTimeResource, LocationEventTimeIncludable>
{

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="EventTimeResource" />.
  /// </summary>
  public EventTimeResource EventTime => GetRelated<EventTimeResource>("event_time");

  /// <summary>
  /// The related <see cref="LocationResource" />.
  /// </summary>
  public LocationResource Location => GetRelated<LocationResource>("location");

  internal LocationEventTimeResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public LocationEventTimeResource Include(params LocationEventTimeIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns LocationEventTime resources.
/// </summary>
public class LocationEventTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<LocationEventTime, LocationEventTimeResourceCollection, LocationEventTimeResource, CheckInsDocumentContext>,
  IIncludable<LocationEventTimeResourceCollection, LocationEventTimeIncludable>,
  IQueryable<LocationEventTimeResourceCollection, LocationEventTimeQueryable>
{
  internal LocationEventTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LocationEventTimeResourceCollection Include(params LocationEventTimeIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public LocationEventTimeResourceCollection Query(params (LocationEventTimeQueryable, string)[] queries)
    => base.Query(queries);
}

