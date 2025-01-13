/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Models.People.V2024_09_12.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

/// <summary>
/// A fetchable People WorkflowCategory resource.
/// </summary>
public class WorkflowCategoryResource
  : PlanningCenterSingletonFetchableResource<WorkflowCategory, WorkflowCategoryResource, PeopleDocumentContext>
{

  internal WorkflowCategoryResource(Uri uri, HttpClient client) : base(uri, client) { }
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
  public WorkflowCategoryResourceCollection Query(params KeyValuePair<WorkflowCategoryQueryable, string>[] queries)
    => base.Query(queries);
}

