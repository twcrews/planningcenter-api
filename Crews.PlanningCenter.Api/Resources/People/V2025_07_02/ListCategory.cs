/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_07_02.Entities;
using Crews.PlanningCenter.Models.People.V2025_07_02.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_07_02;

/// <summary>
/// A fetchable People ListCategory resource.
/// </summary>
public class ListCategoryResource
  : PlanningCenterSingletonFetchableResource<ListCategory, ListCategoryResource, PeopleDocumentContext>,
  IIncludable<ListCategoryResource, ListCategoryIncludable>
{

  /// <summary>
  /// The related <see cref="ListResourceCollection" />.
  /// </summary>
  public ListResourceCollection Lists => GetRelated<ListResourceCollection>("lists");

  internal ListCategoryResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ListCategoryResource Include(params ListCategoryIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ListCategory>> PostAsync(ListCategory resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ListCategory>> PatchAsync(ListCategory resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People ListCategory resources.
/// </summary>
public class ListCategoryResourceCollection
  : PlanningCenterPaginatedFetchableResource<ListCategory, ListCategoryResourceCollection, ListCategoryResource, PeopleDocumentContext>,
  IIncludable<ListCategoryResourceCollection, ListCategoryIncludable>,
  IOrderable<ListCategoryResourceCollection, ListCategoryOrderable>,
  IQueryable<ListCategoryResourceCollection, ListCategoryQueryable>
{
  internal ListCategoryResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ListCategoryResourceCollection Include(params ListCategoryIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ListCategoryResourceCollection OrderBy(ListCategoryOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ListCategoryResourceCollection Query(params (ListCategoryQueryable, string)[] queries)
    => base.Query(queries);
}

