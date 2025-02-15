/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_01_14.Entities;
using Crews.PlanningCenter.Models.People.V2019_01_14.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_01_14;

/// <summary>
/// A fetchable People WorkflowShare resource.
/// </summary>
public class WorkflowShareResource
  : PlanningCenterSingletonFetchableResource<WorkflowShare, WorkflowShareResource, PeopleDocumentContext>,
  IIncludable<WorkflowShareResource, WorkflowShareIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal WorkflowShareResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public WorkflowShareResource Include(params WorkflowShareIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<WorkflowShare>> PostAsync(WorkflowShare resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<WorkflowShare>> PatchAsync(WorkflowShare resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People WorkflowShare resources.
/// </summary>
public class WorkflowShareResourceCollection
  : PlanningCenterPaginatedFetchableResource<WorkflowShare, WorkflowShareResourceCollection, WorkflowShareResource, PeopleDocumentContext>,
  IIncludable<WorkflowShareResourceCollection, WorkflowShareIncludable>,
  IQueryable<WorkflowShareResourceCollection, WorkflowShareQueryable>
{
  internal WorkflowShareResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public WorkflowShareResourceCollection Include(params WorkflowShareIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public WorkflowShareResourceCollection Query(params (WorkflowShareQueryable, string)[] queries)
    => base.Query(queries);
}

