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
/// A fetchable People WorkflowCardActivity resource.
/// </summary>
public class WorkflowCardActivityResource
  : PlanningCenterSingletonFetchableResource<WorkflowCardActivity, WorkflowCardActivityResource, PeopleDocumentContext>
{

  internal WorkflowCardActivityResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People WorkflowCardActivity resources.
/// </summary>
public class WorkflowCardActivityResourceCollection
  : PlanningCenterPaginatedFetchableResource<WorkflowCardActivity, WorkflowCardActivityResourceCollection, WorkflowCardActivityResource, PeopleDocumentContext>,
  IOrderable<WorkflowCardActivityResourceCollection, WorkflowCardActivityOrderable>
{
  internal WorkflowCardActivityResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public WorkflowCardActivityResourceCollection OrderBy(WorkflowCardActivityOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

