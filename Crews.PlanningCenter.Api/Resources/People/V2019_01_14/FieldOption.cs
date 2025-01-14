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
/// A fetchable People FieldOption resource.
/// </summary>
public class FieldOptionResource
  : PlanningCenterSingletonFetchableResource<FieldOption, FieldOptionResource, PeopleDocumentContext>
{

  internal FieldOptionResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<FieldOption>> PostAsync(FieldOption resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<FieldOption>> PatchAsync(FieldOption resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People FieldOption resources.
/// </summary>
public class FieldOptionResourceCollection
  : PlanningCenterPaginatedFetchableResource<FieldOption, FieldOptionResourceCollection, FieldOptionResource, PeopleDocumentContext>,
  IOrderable<FieldOptionResourceCollection, FieldOptionOrderable>,
  IQueryable<FieldOptionResourceCollection, FieldOptionQueryable>
{
  internal FieldOptionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FieldOptionResourceCollection OrderBy(FieldOptionOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public FieldOptionResourceCollection Query(params (FieldOptionQueryable, string)[] queries)
    => base.Query(queries);
}

