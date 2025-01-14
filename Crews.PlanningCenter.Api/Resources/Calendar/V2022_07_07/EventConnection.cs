/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Entities;
using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2022_07_07;

/// <summary>
/// A fetchable Calendar EventConnection resource.
/// </summary>
public class EventConnectionResource
  : PlanningCenterSingletonFetchableResource<EventConnection, EventConnectionResource, CalendarDocumentContext>
{

  internal EventConnectionResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<EventConnection>> PostAsync(EventConnection resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<EventConnection>> PatchAsync(EventConnection resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Calendar EventConnection resources.
/// </summary>
public class EventConnectionResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventConnection, EventConnectionResourceCollection, EventConnectionResource, CalendarDocumentContext>,
  IQueryable<EventConnectionResourceCollection, EventConnectionQueryable>
{
  internal EventConnectionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventConnectionResourceCollection Query(params (EventConnectionQueryable, string)[] queries)
    => base.Query(queries);
}

