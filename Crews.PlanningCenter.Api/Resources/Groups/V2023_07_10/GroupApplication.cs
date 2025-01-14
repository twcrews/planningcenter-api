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
/// A fetchable Groups GroupApplication resource.
/// </summary>
public class GroupApplicationResource
  : PlanningCenterSingletonFetchableResource<GroupApplication, GroupApplicationResource, GroupsDocumentContext>,
  IIncludable<GroupApplicationResource, GroupApplicationIncludable>
{

  /// <summary>
  /// The related <see cref="GroupResource" />.
  /// </summary>
  public GroupResource Group => GetRelated<GroupResource>("group");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal GroupApplicationResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public GroupApplicationResource Include(params GroupApplicationIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Groups GroupApplication resources.
/// </summary>
public class GroupApplicationResourceCollection
  : PlanningCenterPaginatedFetchableResource<GroupApplication, GroupApplicationResourceCollection, GroupApplicationResource, GroupsDocumentContext>,
  IIncludable<GroupApplicationResourceCollection, GroupApplicationIncludable>,
  IOrderable<GroupApplicationResourceCollection, GroupApplicationOrderable>,
  IQueryable<GroupApplicationResourceCollection, GroupApplicationQueryable>
{
  internal GroupApplicationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public GroupApplicationResourceCollection Include(params GroupApplicationIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public GroupApplicationResourceCollection OrderBy(GroupApplicationOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public GroupApplicationResourceCollection Query(params (GroupApplicationQueryable, string)[] queries)
    => base.Query(queries);
}

