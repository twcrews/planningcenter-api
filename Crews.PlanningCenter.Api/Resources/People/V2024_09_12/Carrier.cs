/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Models.People.V2024_09_12.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

/// <summary>
/// A fetchable People Carrier resource.
/// </summary>
public class CarrierResource
  : PlanningCenterSingletonFetchableResource<Carrier, CarrierResource, PeopleDocumentContext>
{

  internal CarrierResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People Carrier resources.
/// </summary>
public class CarrierResourceCollection
  : PlanningCenterPaginatedFetchableResource<Carrier, CarrierResourceCollection, CarrierResource, PeopleDocumentContext>,
  IOrderable<CarrierResourceCollection, CarrierOrderable>
{
  internal CarrierResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public CarrierResourceCollection OrderBy(CarrierOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

