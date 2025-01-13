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
/// A fetchable Groups TagGroup resource.
/// </summary>
public class TagGroupResource
  : PlanningCenterSingletonFetchableResource<TagGroup, TagGroupResource, GroupsDocumentContext>
{

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal TagGroupResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Groups TagGroup resources.
/// </summary>
public class TagGroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<TagGroup, TagGroupResourceCollection, TagGroupResource, GroupsDocumentContext>,
  IOrderable<TagGroupResourceCollection, TagGroupOrderable>,
  IQueryable<TagGroupResourceCollection, TagGroupQueryable>
{
  internal TagGroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TagGroupResourceCollection OrderBy(TagGroupOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public TagGroupResourceCollection Query(params KeyValuePair<TagGroupQueryable, string>[] queries)
    => base.Query(queries);
}

