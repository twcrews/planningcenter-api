/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2022_01_05.Entities;
using Crews.PlanningCenter.Models.People.V2022_01_05.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2022_01_05;

/// <summary>
/// A fetchable People WorkflowStep resource.
/// </summary>
public class WorkflowStepResource
  : PlanningCenterSingletonFetchableResource<WorkflowStep, WorkflowStepResource, PeopleDocumentContext>,
  IIncludable<WorkflowStepResource, WorkflowStepIncludable>
{

  /// <summary>
  /// The related <see cref="WorkflowStepAssigneeSummaryResourceCollection" />.
  /// </summary>
  public WorkflowStepAssigneeSummaryResourceCollection AssigneeSummaries => GetRelated<WorkflowStepAssigneeSummaryResourceCollection>("assignee_summaries");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource DefaultAssignee => GetRelated<PersonResource>("default_assignee");

  internal WorkflowStepResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public WorkflowStepResource Include(params WorkflowStepIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People WorkflowStep resources.
/// </summary>
public class WorkflowStepResourceCollection
  : PlanningCenterPaginatedFetchableResource<WorkflowStep, WorkflowStepResourceCollection, WorkflowStepResource, PeopleDocumentContext>,
  IIncludable<WorkflowStepResourceCollection, WorkflowStepIncludable>,
  IOrderable<WorkflowStepResourceCollection, WorkflowStepOrderable>,
  IQueryable<WorkflowStepResourceCollection, WorkflowStepQueryable>
{
  internal WorkflowStepResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public WorkflowStepResourceCollection Include(params WorkflowStepIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public WorkflowStepResourceCollection OrderBy(WorkflowStepOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public WorkflowStepResourceCollection Query(params KeyValuePair<WorkflowStepQueryable, string>[] queries)
    => base.Query(queries);
}

