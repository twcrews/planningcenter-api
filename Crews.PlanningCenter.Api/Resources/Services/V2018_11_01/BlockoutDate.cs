/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services BlockoutDate resource.
/// </summary>
public class BlockoutDateResource
  : PlanningCenterSingletonFetchableResource<BlockoutDate, BlockoutDateResource, ServicesDocumentContext>
{

  internal BlockoutDateResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services BlockoutDate resources.
/// </summary>
public class BlockoutDateResourceCollection
  : PlanningCenterPaginatedFetchableResource<BlockoutDate, BlockoutDateResourceCollection, BlockoutDateResource, ServicesDocumentContext>
{
  internal BlockoutDateResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

