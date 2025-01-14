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
/// A fetchable Groups GroupType resource.
/// </summary>
public class GroupTypeResource
  : PlanningCenterSingletonFetchableResource<GroupType, GroupTypeResource, GroupsDocumentContext>
{

  /// <summary>
  /// The related <see cref="EventResourceCollection" />.
  /// </summary>
  public EventResourceCollection Events => GetRelated<EventResourceCollection>("events");

  /// <summary>
  /// The related <see cref="GroupResourceCollection" />.
  /// </summary>
  public GroupResourceCollection Groups => GetRelated<GroupResourceCollection>("groups");

  /// <summary>
  /// The related <see cref="ResourceResourceCollection" />.
  /// </summary>
  public ResourceResourceCollection Resources => GetRelated<ResourceResourceCollection>("resources");

  internal GroupTypeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Groups GroupType resources.
/// </summary>
public class GroupTypeResourceCollection
  : PlanningCenterPaginatedFetchableResource<GroupType, GroupTypeResourceCollection, GroupTypeResource, GroupsDocumentContext>,
  IOrderable<GroupTypeResourceCollection, GroupTypeOrderable>,
  IQueryable<GroupTypeResourceCollection, GroupTypeQueryable>
{
  internal GroupTypeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public GroupTypeResourceCollection OrderBy(GroupTypeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public GroupTypeResourceCollection Query(params (GroupTypeQueryable, string)[] queries)
    => base.Query(queries);
}

