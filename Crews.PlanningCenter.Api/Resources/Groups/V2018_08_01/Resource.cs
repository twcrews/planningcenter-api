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
/// A fetchable Groups Resource resource.
/// </summary>
public class ResourceResource
  : PlanningCenterSingletonFetchableResource<Resource, ResourceResource, GroupsDocumentContext>
{

  /// <summary>
  /// The related <see cref="ResourceResource" />.
  /// </summary>
  public ResourceResource Download => GetRelated<ResourceResource>("download");

  /// <summary>
  /// The related <see cref="ResourceResource" />.
  /// </summary>
  public ResourceResource Visit => GetRelated<ResourceResource>("visit");

  internal ResourceResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Groups Resource resources.
/// </summary>
public class ResourceResourceCollection
  : PlanningCenterPaginatedFetchableResource<Resource, ResourceResourceCollection, ResourceResource, GroupsDocumentContext>,
  IOrderable<ResourceResourceCollection, ResourceOrderable>
{
  internal ResourceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ResourceResourceCollection OrderBy(ResourceOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

