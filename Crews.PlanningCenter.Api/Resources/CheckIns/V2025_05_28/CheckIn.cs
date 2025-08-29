/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2025_05_28.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2025_05_28.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2025_05_28;

/// <summary>
/// A fetchable CheckIns CheckIn resource.
/// </summary>
public class CheckInResource
  : PlanningCenterSingletonFetchableResource<CheckIn, CheckInResource, CheckInsDocumentContext>,
  IIncludable<CheckInResource, CheckInIncludable>
{

  /// <summary>
  /// The related <see cref="CheckInGroupResource" />.
  /// </summary>
  public CheckInGroupResource CheckInGroup => GetRelated<CheckInGroupResource>("check_in_group");

  /// <summary>
  /// The related <see cref="CheckInTimeResourceCollection" />.
  /// </summary>
  public CheckInTimeResourceCollection CheckInTimes => GetRelated<CheckInTimeResourceCollection>("check_in_times");

  /// <summary>
  /// The related <see cref="StationResource" />.
  /// </summary>
  public StationResource CheckedInAt => GetRelated<StationResource>("checked_in_at");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource CheckedInBy => GetRelated<PersonResource>("checked_in_by");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource CheckedOutBy => GetRelated<PersonResource>("checked_out_by");

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="EventPeriodResource" />.
  /// </summary>
  public EventPeriodResource EventPeriod => GetRelated<EventPeriodResource>("event_period");

  /// <summary>
  /// The related <see cref="EventTimeResourceCollection" />.
  /// </summary>
  public EventTimeResourceCollection EventTimes => GetRelated<EventTimeResourceCollection>("event_times");

  /// <summary>
  /// The related <see cref="LocationResourceCollection" />.
  /// </summary>
  public LocationResourceCollection Locations => GetRelated<LocationResourceCollection>("locations");

  /// <summary>
  /// The related <see cref="OptionResourceCollection" />.
  /// </summary>
  public OptionResourceCollection Options => GetRelated<OptionResourceCollection>("options");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal CheckInResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public CheckInResource Include(params CheckInIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns CheckIn resources.
/// </summary>
public class CheckInResourceCollection
  : PlanningCenterPaginatedFetchableResource<CheckIn, CheckInResourceCollection, CheckInResource, CheckInsDocumentContext>,
  IIncludable<CheckInResourceCollection, CheckInIncludable>,
  IOrderable<CheckInResourceCollection, CheckInOrderable>,
  IQueryable<CheckInResourceCollection, CheckInQueryable>
{
  internal CheckInResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public CheckInResourceCollection Include(params CheckInIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public CheckInResourceCollection OrderBy(CheckInOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public CheckInResourceCollection Query(params (CheckInQueryable, string)[] queries)
    => base.Query(queries);
}

