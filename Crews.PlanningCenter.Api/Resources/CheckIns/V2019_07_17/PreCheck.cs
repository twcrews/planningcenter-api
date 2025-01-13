/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2019_07_17;

/// <summary>
/// A fetchable CheckIns PreCheck resource.
/// </summary>
public class PreCheckResource
  : PlanningCenterSingletonFetchableResource<PreCheck, PreCheckResource, CheckInsDocumentContext>
{

  internal PreCheckResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of CheckIns PreCheck resources.
/// </summary>
public class PreCheckResourceCollection
  : PlanningCenterPaginatedFetchableResource<PreCheck, PreCheckResourceCollection, PreCheckResource, CheckInsDocumentContext>
{
  internal PreCheckResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

