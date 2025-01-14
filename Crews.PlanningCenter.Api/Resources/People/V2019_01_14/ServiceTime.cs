/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_01_14.Entities;
using Crews.PlanningCenter.Models.People.V2019_01_14.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_01_14;

/// <summary>
/// A fetchable People ServiceTime resource.
/// </summary>
public class ServiceTimeResource
  : PlanningCenterSingletonFetchableResource<ServiceTime, ServiceTimeResource, PeopleDocumentContext>
{

  internal ServiceTimeResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ServiceTime>> PostAsync(ServiceTime resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ServiceTime>> PatchAsync(ServiceTime resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People ServiceTime resources.
/// </summary>
public class ServiceTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<ServiceTime, ServiceTimeResourceCollection, ServiceTimeResource, PeopleDocumentContext>,
  IOrderable<ServiceTimeResourceCollection, ServiceTimeOrderable>
{
  internal ServiceTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ServiceTimeResourceCollection OrderBy(ServiceTimeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

