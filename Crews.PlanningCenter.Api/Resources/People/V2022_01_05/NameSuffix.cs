/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_01_05.Entities;
using Crews.PlanningCenter.Models.People.V2022_01_05.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_01_05;

/// <summary>
/// A fetchable People NameSuffix resource.
/// </summary>
public class NameSuffixResource
  : PlanningCenterSingletonFetchableResource<NameSuffix, NameSuffixResource, PeopleDocumentContext>
{

  internal NameSuffixResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<NameSuffix>> PostAsync(NameSuffix resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<NameSuffix>> PatchAsync(NameSuffix resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People NameSuffix resources.
/// </summary>
public class NameSuffixResourceCollection
  : PlanningCenterPaginatedFetchableResource<NameSuffix, NameSuffixResourceCollection, NameSuffixResource, PeopleDocumentContext>,
  IOrderable<NameSuffixResourceCollection, NameSuffixOrderable>,
  IQueryable<NameSuffixResourceCollection, NameSuffixQueryable>
{
  internal NameSuffixResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public NameSuffixResourceCollection OrderBy(NameSuffixOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public NameSuffixResourceCollection Query(params (NameSuffixQueryable, string)[] queries)
    => base.Query(queries);
}

