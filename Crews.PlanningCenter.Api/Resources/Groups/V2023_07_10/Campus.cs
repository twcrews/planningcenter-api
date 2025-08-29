/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2023_07_10.Entities;
using Crews.PlanningCenter.Models.Groups.V2023_07_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2023_07_10;

/// <summary>
/// A fetchable Groups Campus resource.
/// </summary>
public class CampusResource
  : PlanningCenterSingletonFetchableResource<Campus, CampusResource, GroupsDocumentContext>
{

  /// <summary>
  /// The related <see cref="GroupResourceCollection" />.
  /// </summary>
  public GroupResourceCollection Groups => GetRelated<GroupResourceCollection>("groups");

  internal CampusResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Groups Campus resources.
/// </summary>
public class CampusResourceCollection
  : PlanningCenterPaginatedFetchableResource<Campus, CampusResourceCollection, CampusResource, GroupsDocumentContext>,
  IOrderable<CampusResourceCollection, CampusOrderable>,
  IQueryable<CampusResourceCollection, CampusQueryable>
{
  internal CampusResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public CampusResourceCollection OrderBy(CampusOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public CampusResourceCollection Query(params (CampusQueryable, string)[] queries)
    => base.Query(queries);
}

