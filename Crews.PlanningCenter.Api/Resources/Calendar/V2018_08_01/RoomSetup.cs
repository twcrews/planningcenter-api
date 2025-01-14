/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Calendar.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2018_08_01;

/// <summary>
/// A fetchable Calendar RoomSetup resource.
/// </summary>
public class RoomSetupResource
  : PlanningCenterSingletonFetchableResource<RoomSetup, RoomSetupResource, CalendarDocumentContext>,
  IIncludable<RoomSetupResource, RoomSetupIncludable>
{

  /// <summary>
  /// The related <see cref="ResourceResource" />.
  /// </summary>
  public ResourceResource ContainingResource => GetRelated<ResourceResource>("containing_resource");

  /// <summary>
  /// The related <see cref="ResourceSuggestionResourceCollection" />.
  /// </summary>
  public ResourceSuggestionResourceCollection ResourceSuggestions => GetRelated<ResourceSuggestionResourceCollection>("resource_suggestions");

  internal RoomSetupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public RoomSetupResource Include(params RoomSetupIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar RoomSetup resources.
/// </summary>
public class RoomSetupResourceCollection
  : PlanningCenterPaginatedFetchableResource<RoomSetup, RoomSetupResourceCollection, RoomSetupResource, CalendarDocumentContext>,
  IIncludable<RoomSetupResourceCollection, RoomSetupIncludable>,
  IOrderable<RoomSetupResourceCollection, RoomSetupOrderable>,
  IQueryable<RoomSetupResourceCollection, RoomSetupQueryable>
{
  internal RoomSetupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public RoomSetupResourceCollection Include(params RoomSetupIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public RoomSetupResourceCollection OrderBy(RoomSetupOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public RoomSetupResourceCollection Query(params (RoomSetupQueryable, string)[] queries)
    => base.Query(queries);
}

