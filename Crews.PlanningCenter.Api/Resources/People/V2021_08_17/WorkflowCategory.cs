/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2021_08_17.Entities;
using Crews.PlanningCenter.Models.People.V2021_08_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2021_08_17;

/// <summary>
/// A fetchable People WorkflowCategory resource.
/// </summary>
public class WorkflowCategoryResource
  : PlanningCenterSingletonFetchableResource<WorkflowCategory, WorkflowCategoryResource, PeopleDocumentContext>
{

  internal WorkflowCategoryResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<WorkflowCategory>> PostAsync(WorkflowCategory resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<WorkflowCategory>> PatchAsync(WorkflowCategory resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People WorkflowCategory resources.
/// </summary>
public class WorkflowCategoryResourceCollection
  : PlanningCenterPaginatedFetchableResource<WorkflowCategory, WorkflowCategoryResourceCollection, WorkflowCategoryResource, PeopleDocumentContext>,
  IOrderable<WorkflowCategoryResourceCollection, WorkflowCategoryOrderable>,
  IQueryable<WorkflowCategoryResourceCollection, WorkflowCategoryQueryable>
{
  internal WorkflowCategoryResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public WorkflowCategoryResourceCollection OrderBy(WorkflowCategoryOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public WorkflowCategoryResourceCollection Query(params (WorkflowCategoryQueryable, string)[] queries)
    => base.Query(queries);
}

