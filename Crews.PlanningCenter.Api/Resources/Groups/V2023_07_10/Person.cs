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
/// A fetchable Groups Person resource.
/// </summary>
public class PersonResource
  : PlanningCenterSingletonFetchableResource<Person, PersonResource, GroupsDocumentContext>
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
  /// The related <see cref="MembershipResourceCollection" />.
  /// </summary>
  public MembershipResourceCollection Memberships => GetRelated<MembershipResourceCollection>("memberships");

  internal PersonResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Groups Person resources.
/// </summary>
public class PersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<Person, PersonResourceCollection, PersonResource, GroupsDocumentContext>,
  IOrderable<PersonResourceCollection, PersonOrderable>,
  IQueryable<PersonResourceCollection, PersonQueryable>
{
  internal PersonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PersonResourceCollection OrderBy(PersonOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PersonResourceCollection Query(params (PersonQueryable, string)[] queries)
    => base.Query(queries);
}

