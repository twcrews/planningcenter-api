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
/// A fetchable Services BlockoutException resource.
/// </summary>
public class BlockoutExceptionResource
  : PlanningCenterSingletonFetchableResource<BlockoutException, BlockoutExceptionResource, ServicesDocumentContext>
{

  internal BlockoutExceptionResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services BlockoutException resources.
/// </summary>
public class BlockoutExceptionResourceCollection
  : PlanningCenterPaginatedFetchableResource<BlockoutException, BlockoutExceptionResourceCollection, BlockoutExceptionResource, ServicesDocumentContext>
{
  internal BlockoutExceptionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

