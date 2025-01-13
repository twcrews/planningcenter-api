/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services Live resource.
/// </summary>
public class LiveResource
  : PlanningCenterSingletonFetchableResource<Live, LiveResource, ServicesDocumentContext>,
  IIncludable<LiveResource, LiveIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Controller => GetRelated<PersonResource>("controller");

  /// <summary>
  /// The related <see cref="ItemTimeResource" />.
  /// </summary>
  public ItemTimeResource CurrentItemTime => GetRelated<ItemTimeResource>("current_item_time");

  /// <summary>
  /// The related <see cref="ItemResourceCollection" />.
  /// </summary>
  public ItemResourceCollection Items => GetRelated<ItemResourceCollection>("items");

  /// <summary>
  /// The related <see cref="ItemTimeResource" />.
  /// </summary>
  public ItemTimeResource NextItemTime => GetRelated<ItemTimeResource>("next_item_time");

  /// <summary>
  /// The related <see cref="ServiceTypeResource" />.
  /// </summary>
  public ServiceTypeResource ServiceType => GetRelated<ServiceTypeResource>("service_type");

  /// <summary>
  /// The related <see cref="PlanResourceCollection" />.
  /// </summary>
  public PlanResourceCollection WatchablePlans => GetRelated<PlanResourceCollection>("watchable_plans");

  internal LiveResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public LiveResource Include(params LiveIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services Live resources.
/// </summary>
public class LiveResourceCollection
  : PlanningCenterPaginatedFetchableResource<Live, LiveResourceCollection, LiveResource, ServicesDocumentContext>,
  IIncludable<LiveResourceCollection, LiveIncludable>
{
  internal LiveResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LiveResourceCollection Include(params LiveIncludable[] included)
    => base.Include(included);
}

