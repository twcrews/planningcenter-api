/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2023_02_15.Entities;
using Crews.PlanningCenter.Models.People.V2023_02_15.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2023_02_15;

/// <summary>
/// A fetchable People WorkflowCard resource.
/// </summary>
public class WorkflowCardResource
  : PlanningCenterSingletonFetchableResource<WorkflowCard, WorkflowCardResource, PeopleDocumentContext>,
  IIncludable<WorkflowCardResource, WorkflowCardIncludable>
{

  /// <summary>
  /// The related <see cref="WorkflowCardActivityResourceCollection" />.
  /// </summary>
  public WorkflowCardActivityResourceCollection Activities => GetRelated<WorkflowCardActivityResourceCollection>("activities");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Assignee => GetRelated<PersonResource>("assignee");

  /// <summary>
  /// The related <see cref="WorkflowStepResource" />.
  /// </summary>
  public WorkflowStepResource CurrentStep => GetRelated<WorkflowStepResource>("current_step");

  /// <summary>
  /// The related <see cref="WorkflowCardNoteResourceCollection" />.
  /// </summary>
  public WorkflowCardNoteResourceCollection Notes => GetRelated<WorkflowCardNoteResourceCollection>("notes");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  /// <summary>
  /// The related <see cref="WorkflowResource" />.
  /// </summary>
  public WorkflowResource Workflow => GetRelated<WorkflowResource>("workflow");

  internal WorkflowCardResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public WorkflowCardResource Include(params WorkflowCardIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People WorkflowCard resources.
/// </summary>
public class WorkflowCardResourceCollection
  : PlanningCenterPaginatedFetchableResource<WorkflowCard, WorkflowCardResourceCollection, WorkflowCardResource, PeopleDocumentContext>,
  IIncludable<WorkflowCardResourceCollection, WorkflowCardIncludable>,
  IOrderable<WorkflowCardResourceCollection, WorkflowCardOrderable>,
  IQueryable<WorkflowCardResourceCollection, WorkflowCardQueryable>
{
  internal WorkflowCardResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public WorkflowCardResourceCollection Include(params WorkflowCardIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public WorkflowCardResourceCollection OrderBy(WorkflowCardOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public WorkflowCardResourceCollection Query(params KeyValuePair<WorkflowCardQueryable, string>[] queries)
    => base.Query(queries);
}

