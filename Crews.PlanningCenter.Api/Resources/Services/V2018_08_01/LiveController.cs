/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services LiveController resource.
/// </summary>
public class LiveControllerResource
  : PlanningCenterSingletonFetchableResource<LiveController, LiveControllerResource, ServicesDocumentContext>
{

  internal LiveControllerResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services LiveController resources.
/// </summary>
public class LiveControllerResourceCollection
  : PlanningCenterPaginatedFetchableResource<LiveController, LiveControllerResourceCollection, LiveControllerResource, ServicesDocumentContext>,
  IOrderable<LiveControllerResourceCollection, LiveControllerOrderable>
{
  internal LiveControllerResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LiveControllerResourceCollection OrderBy(LiveControllerOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

