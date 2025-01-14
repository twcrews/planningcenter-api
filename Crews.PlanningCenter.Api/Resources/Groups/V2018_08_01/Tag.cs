/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Groups.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2018_08_01;

/// <summary>
/// A fetchable Groups Tag resource.
/// </summary>
public class TagResource
  : PlanningCenterSingletonFetchableResource<Tag, TagResource, GroupsDocumentContext>
{

  /// <summary>
  /// The related <see cref="GroupResourceCollection" />.
  /// </summary>
  public GroupResourceCollection Groups => GetRelated<GroupResourceCollection>("groups");

  internal TagResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Groups Tag resources.
/// </summary>
public class TagResourceCollection
  : PlanningCenterPaginatedFetchableResource<Tag, TagResourceCollection, TagResource, GroupsDocumentContext>,
  IOrderable<TagResourceCollection, TagOrderable>,
  IQueryable<TagResourceCollection, TagQueryable>
{
  internal TagResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TagResourceCollection OrderBy(TagOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public TagResourceCollection Query(params (TagQueryable, string)[] queries)
    => base.Query(queries);
}

