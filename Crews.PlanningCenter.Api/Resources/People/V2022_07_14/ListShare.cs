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
/// A fetchable People ListShare resource.
/// </summary>
public class ListShareResource
  : PlanningCenterSingletonFetchableResource<ListShare, ListShareResource, PeopleDocumentContext>,
  IIncludable<ListShareResource, ListShareIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal ListShareResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ListShareResource Include(params ListShareIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ListShare>> PostAsync(ListShare resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ListShare>> PatchAsync(ListShare resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People ListShare resources.
/// </summary>
public class ListShareResourceCollection
  : PlanningCenterPaginatedFetchableResource<ListShare, ListShareResourceCollection, ListShareResource, PeopleDocumentContext>,
  IIncludable<ListShareResourceCollection, ListShareIncludable>,
  IOrderable<ListShareResourceCollection, ListShareOrderable>,
  IQueryable<ListShareResourceCollection, ListShareQueryable>
{
  internal ListShareResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ListShareResourceCollection Include(params ListShareIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ListShareResourceCollection OrderBy(ListShareOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ListShareResourceCollection Query(params (ListShareQueryable, string)[] queries)
    => base.Query(queries);
}

