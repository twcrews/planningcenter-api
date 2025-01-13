/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services ItemTime resource.
/// </summary>
public class ItemTimeResource
  : PlanningCenterSingletonFetchableResource<ItemTime, ItemTimeResource, ServicesDocumentContext>
{

  internal ItemTimeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services ItemTime resources.
/// </summary>
public class ItemTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<ItemTime, ItemTimeResourceCollection, ItemTimeResource, ServicesDocumentContext>
{
  internal ItemTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

