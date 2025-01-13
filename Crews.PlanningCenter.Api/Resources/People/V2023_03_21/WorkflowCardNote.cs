/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2023_03_21.Entities;
using Crews.PlanningCenter.Models.People.V2023_03_21.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2023_03_21;

/// <summary>
/// A fetchable People WorkflowCardNote resource.
/// </summary>
public class WorkflowCardNoteResource
  : PlanningCenterSingletonFetchableResource<WorkflowCardNote, WorkflowCardNoteResource, PeopleDocumentContext>
{

  internal WorkflowCardNoteResource(Uri uri, HttpClient client) : base(uri, client) { }
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

