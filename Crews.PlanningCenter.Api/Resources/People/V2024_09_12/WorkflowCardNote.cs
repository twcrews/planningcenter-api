/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Models.People.V2024_09_12.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

/// <summary>
/// A fetchable People WorkflowCardNote resource.
/// </summary>
public class WorkflowCardNoteResource
  : PlanningCenterSingletonFetchableResource<WorkflowCardNote, WorkflowCardNoteResource, PeopleDocumentContext>
{

  internal WorkflowCardNoteResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<WorkflowCardNote>> PostAsync(WorkflowCardNote resource)
    => base.PostAsync(resource);
}

/// <summary>
/// A fetchable collection of People WorkflowCardNote resources.
/// </summary>
public class WorkflowCardNoteResourceCollection
  : PlanningCenterPaginatedFetchableResource<WorkflowCardNote, WorkflowCardNoteResourceCollection, WorkflowCardNoteResource, PeopleDocumentContext>,
  IOrderable<WorkflowCardNoteResourceCollection, WorkflowCardNoteOrderable>
{
  internal WorkflowCardNoteResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public WorkflowCardNoteResourceCollection OrderBy(WorkflowCardNoteOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

