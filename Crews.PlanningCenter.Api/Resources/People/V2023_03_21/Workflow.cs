/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2023_03_21.Entities;
using Crews.PlanningCenter.Models.People.V2023_03_21.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2023_03_21;

/// <summary>
/// A fetchable People Workflow resource.
/// </summary>
public class WorkflowResource
  : PlanningCenterSingletonFetchableResource<Workflow, WorkflowResource, PeopleDocumentContext>,
  IIncludable<WorkflowResource, WorkflowIncludable>
{

  /// <summary>
  /// The related <see cref="WorkflowCardResourceCollection" />.
  /// </summary>
  public WorkflowCardResourceCollection Cards => GetRelated<WorkflowCardResourceCollection>("cards");

  /// <summary>
  /// The related <see cref="WorkflowCategoryResource" />.
  /// </summary>
  public WorkflowCategoryResource Category => GetRelated<WorkflowCategoryResource>("category");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource SharedPeople => GetRelated<PersonResource>("shared_people");

  /// <summary>
  /// The related <see cref="WorkflowShareResourceCollection" />.
  /// </summary>
  public WorkflowShareResourceCollection Shares => GetRelated<WorkflowShareResourceCollection>("shares");

  /// <summary>
  /// The related <see cref="WorkflowStepResourceCollection" />.
  /// </summary>
  public WorkflowStepResourceCollection Steps => GetRelated<WorkflowStepResourceCollection>("steps");

  internal WorkflowResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public WorkflowResource Include(params WorkflowIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Workflow>> PostAsync(Workflow resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Workflow>> PatchAsync(Workflow resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People Workflow resources.
/// </summary>
public class WorkflowResourceCollection
  : PlanningCenterPaginatedFetchableResource<Workflow, WorkflowResourceCollection, WorkflowResource, PeopleDocumentContext>,
  IIncludable<WorkflowResourceCollection, WorkflowIncludable>,
  IOrderable<WorkflowResourceCollection, WorkflowOrderable>,
  IQueryable<WorkflowResourceCollection, WorkflowQueryable>
{
  internal WorkflowResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public WorkflowResourceCollection Include(params WorkflowIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public WorkflowResourceCollection OrderBy(WorkflowOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public WorkflowResourceCollection Query(params (WorkflowQueryable, string)[] queries)
    => base.Query(queries);
}

