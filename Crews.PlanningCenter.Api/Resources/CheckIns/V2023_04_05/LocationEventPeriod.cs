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
/// A fetchable CheckIns LocationEventPeriod resource.
/// </summary>
public class LocationEventPeriodResource
  : PlanningCenterSingletonFetchableResource<LocationEventPeriod, LocationEventPeriodResource, CheckInsDocumentContext>,
  IIncludable<LocationEventPeriodResource, LocationEventPeriodIncludable>
{

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="EventPeriodResource" />.
  /// </summary>
  public EventPeriodResource EventPeriod => GetRelated<EventPeriodResource>("event_period");

  /// <summary>
  /// The related <see cref="LocationResource" />.
  /// </summary>
  public LocationResource Location => GetRelated<LocationResource>("location");

  internal LocationEventPeriodResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public LocationEventPeriodResource Include(params LocationEventPeriodIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns LocationEventPeriod resources.
/// </summary>
public class LocationEventPeriodResourceCollection
  : PlanningCenterPaginatedFetchableResource<LocationEventPeriod, LocationEventPeriodResourceCollection, LocationEventPeriodResource, CheckInsDocumentContext>,
  IIncludable<LocationEventPeriodResourceCollection, LocationEventPeriodIncludable>
{
  internal LocationEventPeriodResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LocationEventPeriodResourceCollection Include(params LocationEventPeriodIncludable[] included)
    => base.Include(included);
}

