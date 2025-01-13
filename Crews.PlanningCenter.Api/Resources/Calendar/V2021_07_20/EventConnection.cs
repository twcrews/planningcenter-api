/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Entities;
using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2021_07_20;

/// <summary>
/// A fetchable Calendar EventConnection resource.
/// </summary>
public class EventConnectionResource
  : PlanningCenterSingletonFetchableResource<EventConnection, EventConnectionResource, CalendarDocumentContext>
{

  internal EventConnectionResource(Uri uri, HttpClient client) : base(uri, client) { }
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
  public EventConnectionResourceCollection Query(params KeyValuePair<EventConnectionQueryable, string>[] queries)
    => base.Query(queries);
}

