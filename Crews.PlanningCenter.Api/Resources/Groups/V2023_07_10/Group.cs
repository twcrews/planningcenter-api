/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2023_07_10.Entities;
using Crews.PlanningCenter.Models.Groups.V2023_07_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2023_07_10;

/// <summary>
/// A fetchable Groups Group resource.
/// </summary>
public class GroupResource
  : PlanningCenterSingletonFetchableResource<Group, GroupResource, GroupsDocumentContext>,
  IIncludable<GroupResource, GroupIncludable>
{

  /// <summary>
  /// The related <see cref="GroupApplicationResourceCollection" />.
  /// </summary>
  public GroupApplicationResourceCollection Applications => GetRelated<GroupApplicationResourceCollection>("applications");

  /// <summary>
  /// The related <see cref="CampusResourceCollection" />.
  /// </summary>
  public CampusResourceCollection Campuses => GetRelated<CampusResourceCollection>("campuses");

  /// <summary>
  /// The related <see cref="EnrollmentResource" />.
  /// </summary>
  public EnrollmentResource Enrollment => GetRelated<EnrollmentResource>("enrollment");

  /// <summary>
  /// The related <see cref="EventResourceCollection" />.
  /// </summary>
  public EventResourceCollection Events => GetRelated<EventResourceCollection>("events");

  /// <summary>
  /// The related <see cref="GroupTypeResource" />.
  /// </summary>
  public GroupTypeResource GroupType => GetRelated<GroupTypeResource>("group_type");

  /// <summary>
  /// The related <see cref="LocationResource" />.
  /// </summary>
  public LocationResource Location => GetRelated<LocationResource>("location");

  /// <summary>
  /// The related <see cref="MembershipResourceCollection" />.
  /// </summary>
  public MembershipResourceCollection Memberships => GetRelated<MembershipResourceCollection>("memberships");

  /// <summary>
  /// The related <see cref="PersonResourceCollection" />.
  /// </summary>
  public PersonResourceCollection People => GetRelated<PersonResourceCollection>("people");

  /// <summary>
  /// The related <see cref="ResourceResourceCollection" />.
  /// </summary>
  public ResourceResourceCollection Resources => GetRelated<ResourceResourceCollection>("resources");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal GroupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public GroupResource Include(params GroupIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Group>> PatchAsync(Group resource)
    => base.PatchAsync(resource);
}

/// <summary>
/// A fetchable collection of Groups Group resources.
/// </summary>
public class GroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<Group, GroupResourceCollection, GroupResource, GroupsDocumentContext>,
  IIncludable<GroupResourceCollection, GroupIncludable>,
  IOrderable<GroupResourceCollection, GroupOrderable>,
  IQueryable<GroupResourceCollection, GroupQueryable>
{
  internal GroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public GroupResourceCollection Include(params GroupIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public GroupResourceCollection OrderBy(GroupOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public GroupResourceCollection Query(params (GroupQueryable, string)[] queries)
    => base.Query(queries);
}

