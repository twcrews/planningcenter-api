/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.People.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2018_08_01;

/// <summary>
/// A fetchable People PhoneNumber resource.
/// </summary>
public class PhoneNumberResource
  : PlanningCenterSingletonFetchableResource<PhoneNumber, PhoneNumberResource, PeopleDocumentContext>
{

  internal PhoneNumberResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<PhoneNumber>> PostAsync(PhoneNumber resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<PhoneNumber>> PatchAsync(PhoneNumber resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People PhoneNumber resources.
/// </summary>
public class PhoneNumberResourceCollection
  : PlanningCenterPaginatedFetchableResource<PhoneNumber, PhoneNumberResourceCollection, PhoneNumberResource, PeopleDocumentContext>,
  IOrderable<PhoneNumberResourceCollection, PhoneNumberOrderable>,
  IQueryable<PhoneNumberResourceCollection, PhoneNumberQueryable>
{
  internal PhoneNumberResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PhoneNumberResourceCollection OrderBy(PhoneNumberOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PhoneNumberResourceCollection Query(params (PhoneNumberQueryable, string)[] queries)
    => base.Query(queries);
}

