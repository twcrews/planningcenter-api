/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2019_07_17;

/// <summary>
/// A fetchable CheckIns Location resource.
/// </summary>
public class LocationResource
  : PlanningCenterSingletonFetchableResource<Location, LocationResource, CheckInsDocumentContext>,
  IIncludable<LocationResource, LocationIncludable>
{

  /// <summary>
  /// The related <see cref="CheckInResourceCollection" />.
  /// </summary>
  public CheckInResourceCollection CheckIns => GetRelated<CheckInResourceCollection>("check_ins");

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="IntegrationLinkResourceCollection" />.
  /// </summary>
  public IntegrationLinkResourceCollection IntegrationLinks => GetRelated<IntegrationLinkResourceCollection>("integration_links");

  /// <summary>
  /// The related <see cref="LocationEventPeriodResourceCollection" />.
  /// </summary>
  public LocationEventPeriodResourceCollection LocationEventPeriods => GetRelated<LocationEventPeriodResourceCollection>("location_event_periods");

  /// <summary>
  /// The related <see cref="LocationEventTimeResourceCollection" />.
  /// </summary>
  public LocationEventTimeResourceCollection LocationEventTimes => GetRelated<LocationEventTimeResourceCollection>("location_event_times");

  /// <summary>
  /// The related <see cref="LocationLabelResourceCollection" />.
  /// </summary>
  public LocationLabelResourceCollection LocationLabels => GetRelated<LocationLabelResourceCollection>("location_labels");

  /// <summary>
  /// The related <see cref="LocationResourceCollection" />.
  /// </summary>
  public LocationResourceCollection Locations => GetRelated<LocationResourceCollection>("locations");

  /// <summary>
  /// The related <see cref="OptionResourceCollection" />.
  /// </summary>
  public OptionResourceCollection Options => GetRelated<OptionResourceCollection>("options");

  /// <summary>
  /// The related <see cref="LocationResource" />.
  /// </summary>
  public LocationResource Parent => GetRelated<LocationResource>("parent");

  internal LocationResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public LocationResource Include(params LocationIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns Location resources.
/// </summary>
public class LocationResourceCollection
  : PlanningCenterPaginatedFetchableResource<Location, LocationResourceCollection, LocationResource, CheckInsDocumentContext>,
  IIncludable<LocationResourceCollection, LocationIncludable>,
  IOrderable<LocationResourceCollection, LocationOrderable>
{
  internal LocationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LocationResourceCollection Include(params LocationIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public LocationResourceCollection OrderBy(LocationOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

