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
/// A fetchable Groups Membership resource.
/// </summary>
public class MembershipResource
  : PlanningCenterSingletonFetchableResource<Membership, MembershipResource, GroupsDocumentContext>,
  IIncludable<MembershipResource, MembershipIncludable>
{

  /// <summary>
  /// The related <see cref="GroupResource" />.
  /// </summary>
  public GroupResource Group => GetRelated<GroupResource>("group");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal MembershipResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public MembershipResource Include(params MembershipIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Groups Membership resources.
/// </summary>
public class MembershipResourceCollection
  : PlanningCenterPaginatedFetchableResource<Membership, MembershipResourceCollection, MembershipResource, GroupsDocumentContext>,
  IIncludable<MembershipResourceCollection, MembershipIncludable>,
  IOrderable<MembershipResourceCollection, MembershipOrderable>,
  IQueryable<MembershipResourceCollection, MembershipQueryable>
{
  internal MembershipResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public MembershipResourceCollection Include(params MembershipIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public MembershipResourceCollection OrderBy(MembershipOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public MembershipResourceCollection Query(params KeyValuePair<MembershipQueryable, string>[] queries)
    => base.Query(queries);
}

