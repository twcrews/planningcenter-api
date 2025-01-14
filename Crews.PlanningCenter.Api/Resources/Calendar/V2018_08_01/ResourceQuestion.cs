/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Calendar.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2018_08_01;

/// <summary>
/// A fetchable Calendar ResourceQuestion resource.
/// </summary>
public class ResourceQuestionResource
  : PlanningCenterSingletonFetchableResource<ResourceQuestion, ResourceQuestionResource, CalendarDocumentContext>
{

  internal ResourceQuestionResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ResourceQuestion>> PostAsync(ResourceQuestion resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ResourceQuestion>> PatchAsync(ResourceQuestion resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Calendar ResourceQuestion resources.
/// </summary>
public class ResourceQuestionResourceCollection
  : PlanningCenterPaginatedFetchableResource<ResourceQuestion, ResourceQuestionResourceCollection, ResourceQuestionResource, CalendarDocumentContext>,
  IQueryable<ResourceQuestionResourceCollection, ResourceQuestionQueryable>
{
  internal ResourceQuestionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ResourceQuestionResourceCollection Query(params (ResourceQuestionQueryable, string)[] queries)
    => base.Query(queries);
}

