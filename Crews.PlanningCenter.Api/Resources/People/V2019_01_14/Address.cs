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
/// A fetchable People Address resource.
/// </summary>
public class AddressResource
  : PlanningCenterSingletonFetchableResource<Address, AddressResource, PeopleDocumentContext>
{

  internal AddressResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Address>> PostAsync(Address resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Address>> PatchAsync(Address resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
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
  public AddressResourceCollection Query(params (AddressQueryable, string)[] queries)
    => base.Query(queries);
}

