/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services Layout resource.
/// </summary>
public class LayoutResource
  : PlanningCenterSingletonFetchableResource<Layout, LayoutResource, ServicesDocumentContext>
{

  internal LayoutResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services Layout resources.
/// </summary>
public class LayoutResourceCollection
  : PlanningCenterPaginatedFetchableResource<Layout, LayoutResourceCollection, LayoutResource, ServicesDocumentContext>
{
  internal LayoutResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

