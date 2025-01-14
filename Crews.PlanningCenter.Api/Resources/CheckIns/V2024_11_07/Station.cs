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
/// A fetchable CheckIns Station resource.
/// </summary>
public class StationResource
  : PlanningCenterSingletonFetchableResource<Station, StationResource, CheckInsDocumentContext>,
  IIncludable<StationResource, StationIncludable>
{

  /// <summary>
  /// The related <see cref="CheckInGroupResourceCollection" />.
  /// </summary>
  public CheckInGroupResourceCollection CheckInGroups => GetRelated<CheckInGroupResourceCollection>("check_in_groups");

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckedInAtCheckIns => GetRelated<CheckInResourceCollection>("checked_in_at_check_ins");

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="LocationResource" />.
  /// </summary>
  public LocationResource Location => GetRelated<LocationResource>("location");

  /// <summary>
  /// The related <see cref="StationResource" />.
  /// </summary>
  public StationResource PrintStation => GetRelated<StationResource>("print_station");

  /// <summary>
  /// The related <see cref="ThemeResource" />.
  /// </summary>
  public ThemeResource Theme => GetRelated<ThemeResource>("theme");

  internal StationResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public StationResource Include(params StationIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns Station resources.
/// </summary>
public class StationResourceCollection
  : PlanningCenterPaginatedFetchableResource<Station, StationResourceCollection, StationResource, CheckInsDocumentContext>,
  IIncludable<StationResourceCollection, StationIncludable>
{
  internal StationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public StationResourceCollection Include(params StationIncludable[] included)
    => base.Include(included);
}

