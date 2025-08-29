/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Entities;
using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2021_07_20;

/// <summary>
/// A fetchable Calendar EventResourceAnswer resource.
/// </summary>
public class EventResourceAnswerResource
  : PlanningCenterSingletonFetchableResource<EventResourceAnswer, EventResourceAnswerResource, CalendarDocumentContext>
{

  internal EventResourceAnswerResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Calendar EventResourceAnswer resources.
/// </summary>
public class EventResourceAnswerResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventResourceAnswer, EventResourceAnswerResourceCollection, EventResourceAnswerResource, CalendarDocumentContext>
{
  internal EventResourceAnswerResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

