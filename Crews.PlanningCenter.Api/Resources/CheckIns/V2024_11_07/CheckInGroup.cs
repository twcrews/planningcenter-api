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
/// A fetchable CheckIns CheckInGroup resource.
/// </summary>
public class CheckInGroupResource
  : PlanningCenterSingletonFetchableResource<CheckInGroup, CheckInGroupResource, CheckInsDocumentContext>,
  IIncludable<CheckInGroupResource, CheckInGroupIncludable>
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
  /// The related <see cref="StationResource" />.
  /// </summary>
  public StationResource PrintStation => GetRelated<StationResource>("print_station");

  internal CheckInGroupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public CheckInGroupResource Include(params CheckInGroupIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns CheckInGroup resources.
/// </summary>
public class CheckInGroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<CheckInGroup, CheckInGroupResourceCollection, CheckInGroupResource, CheckInsDocumentContext>,
  IIncludable<CheckInGroupResourceCollection, CheckInGroupIncludable>
{
  internal CheckInGroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public CheckInGroupResourceCollection Include(params CheckInGroupIncludable[] included)
    => base.Include(included);
}

