/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_07_02.Entities;
using Crews.PlanningCenter.Models.People.V2025_07_02.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_07_02;

/// <summary>
/// A fetchable People WorkflowStepAssigneeSummary resource.
/// </summary>
public class WorkflowStepAssigneeSummaryResource
  : PlanningCenterSingletonFetchableResource<WorkflowStepAssigneeSummary, WorkflowStepAssigneeSummaryResource, PeopleDocumentContext>,
  IIncludable<WorkflowStepAssigneeSummaryResource, WorkflowStepAssigneeSummaryIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal WorkflowStepAssigneeSummaryResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public WorkflowStepAssigneeSummaryResource Include(params WorkflowStepAssigneeSummaryIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People WorkflowStepAssigneeSummary resources.
/// </summary>
public class WorkflowStepAssigneeSummaryResourceCollection
  : PlanningCenterPaginatedFetchableResource<WorkflowStepAssigneeSummary, WorkflowStepAssigneeSummaryResourceCollection, WorkflowStepAssigneeSummaryResource, PeopleDocumentContext>,
  IIncludable<WorkflowStepAssigneeSummaryResourceCollection, WorkflowStepAssigneeSummaryIncludable>
{
  internal WorkflowStepAssigneeSummaryResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public WorkflowStepAssigneeSummaryResourceCollection Include(params WorkflowStepAssigneeSummaryIncludable[] included)
    => base.Include(included);
}

