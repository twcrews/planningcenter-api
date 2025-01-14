/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_07_22.Entities;
using Crews.PlanningCenter.Models.People.V2020_07_22.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_07_22;

/// <summary>
/// A fetchable People InactiveReason resource.
/// </summary>
public class InactiveReasonResource
  : PlanningCenterSingletonFetchableResource<InactiveReason, InactiveReasonResource, PeopleDocumentContext>
{

  internal InactiveReasonResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<InactiveReason>> PostAsync(InactiveReason resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<InactiveReason>> PatchAsync(InactiveReason resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People InactiveReason resources.
/// </summary>
public class InactiveReasonResourceCollection
  : PlanningCenterPaginatedFetchableResource<InactiveReason, InactiveReasonResourceCollection, InactiveReasonResource, PeopleDocumentContext>,
  IOrderable<InactiveReasonResourceCollection, InactiveReasonOrderable>,
  IQueryable<InactiveReasonResourceCollection, InactiveReasonQueryable>
{
  internal InactiveReasonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public InactiveReasonResourceCollection OrderBy(InactiveReasonOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public InactiveReasonResourceCollection Query(params (InactiveReasonQueryable, string)[] queries)
    => base.Query(queries);
}

