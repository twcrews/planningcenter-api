/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2023_02_15.Entities;
using Crews.PlanningCenter.Models.People.V2023_02_15.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2023_02_15;

/// <summary>
/// A fetchable People Campus resource.
/// </summary>
public class CampusResource
  : PlanningCenterSingletonFetchableResource<Campus, CampusResource, PeopleDocumentContext>,
  IIncludable<CampusResource, CampusIncludable>
{

  /// <summary>
  /// The related <see cref="ListResourceCollection" />.
  /// </summary>
  public ListResourceCollection Lists => GetRelated<ListResourceCollection>("lists");

  /// <summary>
  /// The related <see cref="ServiceTimeResourceCollection" />.
  /// </summary>
  public ServiceTimeResourceCollection ServiceTimes => GetRelated<ServiceTimeResourceCollection>("service_times");

  internal CampusResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public CampusResource Include(params CampusIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Campus>> PostAsync(Campus resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Campus>> PatchAsync(Campus resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People Campus resources.
/// </summary>
public class CampusResourceCollection
  : PlanningCenterPaginatedFetchableResource<Campus, CampusResourceCollection, CampusResource, PeopleDocumentContext>,
  IIncludable<CampusResourceCollection, CampusIncludable>,
  IOrderable<CampusResourceCollection, CampusOrderable>,
  IQueryable<CampusResourceCollection, CampusQueryable>
{
  internal CampusResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public CampusResourceCollection Include(params CampusIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public CampusResourceCollection OrderBy(CampusOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public CampusResourceCollection Query(params (CampusQueryable, string)[] queries)
    => base.Query(queries);
}

