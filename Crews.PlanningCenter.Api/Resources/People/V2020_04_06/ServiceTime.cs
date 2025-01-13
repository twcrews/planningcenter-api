/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_04_06.Entities;
using Crews.PlanningCenter.Models.People.V2020_04_06.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_04_06;

/// <summary>
/// A fetchable People ServiceTime resource.
/// </summary>
public class ServiceTimeResource
  : PlanningCenterSingletonFetchableResource<ServiceTime, ServiceTimeResource, PeopleDocumentContext>
{

  internal ServiceTimeResource(Uri uri, HttpClient client) : base(uri, client) { }
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

