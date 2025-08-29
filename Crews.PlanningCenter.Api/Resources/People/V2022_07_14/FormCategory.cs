/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_07_14.Entities;
using Crews.PlanningCenter.Models.People.V2022_07_14.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_07_14;

/// <summary>
/// A fetchable People FormCategory resource.
/// </summary>
public class FormCategoryResource
  : PlanningCenterSingletonFetchableResource<FormCategory, FormCategoryResource, PeopleDocumentContext>
{

  internal FormCategoryResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<FormCategory>> PostAsync(FormCategory resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<FormCategory>> PatchAsync(FormCategory resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People FormCategory resources.
/// </summary>
public class FormCategoryResourceCollection
  : PlanningCenterPaginatedFetchableResource<FormCategory, FormCategoryResourceCollection, FormCategoryResource, PeopleDocumentContext>,
  IOrderable<FormCategoryResourceCollection, FormCategoryOrderable>,
  IQueryable<FormCategoryResourceCollection, FormCategoryQueryable>
{
  internal FormCategoryResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FormCategoryResourceCollection OrderBy(FormCategoryOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public FormCategoryResourceCollection Query(params (FormCategoryQueryable, string)[] queries)
    => base.Query(queries);
}

