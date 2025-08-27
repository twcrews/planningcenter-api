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
/// A fetchable Services PublicView resource.
/// </summary>
public class PublicViewResource
  : PlanningCenterSingletonFetchableResource<PublicView, PublicViewResource, ServicesDocumentContext>
{

  internal PublicViewResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services PublicView resources.
/// </summary>
public class PublicViewResourceCollection
  : PlanningCenterPaginatedFetchableResource<PublicView, PublicViewResourceCollection, PublicViewResource, ServicesDocumentContext>
{
  internal PublicViewResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

