/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services Tag resource.
/// </summary>
public class TagResource
  : PlanningCenterSingletonFetchableResource<Tag, TagResource, ServicesDocumentContext>
{

  internal TagResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services Tag resources.
/// </summary>
public class TagResourceCollection
  : PlanningCenterPaginatedFetchableResource<Tag, TagResourceCollection, TagResource, ServicesDocumentContext>
{
  internal TagResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

