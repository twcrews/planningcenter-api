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
/// A fetchable People Address resource.
/// </summary>
public class AddressResource
  : PlanningCenterSingletonFetchableResource<Address, AddressResource, PeopleDocumentContext>
{

  internal AddressResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People Address resources.
/// </summary>
public class AddressResourceCollection
  : PlanningCenterPaginatedFetchableResource<Address, AddressResourceCollection, AddressResource, PeopleDocumentContext>,
  IOrderable<AddressResourceCollection, AddressOrderable>,
  IQueryable<AddressResourceCollection, AddressQueryable>
{
  internal AddressResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public AddressResourceCollection OrderBy(AddressOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public AddressResourceCollection Query(params KeyValuePair<AddressQueryable, string>[] queries)
    => base.Query(queries);
}

