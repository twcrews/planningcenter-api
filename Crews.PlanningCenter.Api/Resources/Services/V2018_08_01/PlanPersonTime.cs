/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services PlanPersonTime resource.
/// </summary>
public class PlanPersonTimeResource
  : PlanningCenterSingletonFetchableResource<PlanPersonTime, PlanPersonTimeResource, ServicesDocumentContext>
{

  internal PlanPersonTimeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services PlanPersonTime resources.
/// </summary>
public class PlanPersonTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<PlanPersonTime, PlanPersonTimeResourceCollection, PlanPersonTimeResource, ServicesDocumentContext>
{
  internal PlanPersonTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

