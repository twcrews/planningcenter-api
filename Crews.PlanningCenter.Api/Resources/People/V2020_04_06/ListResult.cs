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
/// A fetchable People ListResult resource.
/// </summary>
public class ListResultResource
  : PlanningCenterSingletonFetchableResource<ListResult, ListResultResource, PeopleDocumentContext>
{

  internal ListResultResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People ListResult resources.
/// </summary>
public class ListResultResourceCollection
  : PlanningCenterPaginatedFetchableResource<ListResult, ListResultResourceCollection, ListResultResource, PeopleDocumentContext>,
  IOrderable<ListResultResourceCollection, ListResultOrderable>
{
  internal ListResultResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ListResultResourceCollection OrderBy(ListResultOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

