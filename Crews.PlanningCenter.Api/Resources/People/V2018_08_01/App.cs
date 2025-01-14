/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.People.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2018_08_01;

/// <summary>
/// A fetchable People App resource.
/// </summary>
public class AppResource
  : PlanningCenterSingletonFetchableResource<App, AppResource, PeopleDocumentContext>
{

  internal AppResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People App resources.
/// </summary>
public class AppResourceCollection
  : PlanningCenterPaginatedFetchableResource<App, AppResourceCollection, AppResource, PeopleDocumentContext>,
  IOrderable<AppResourceCollection, AppOrderable>,
  IQueryable<AppResourceCollection, AppQueryable>
{
  internal AppResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public AppResourceCollection OrderBy(AppOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public AppResourceCollection Query(params (AppQueryable, string)[] queries)
    => base.Query(queries);
}

