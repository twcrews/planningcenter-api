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
/// A fetchable Services CustomSlide resource.
/// </summary>
public class CustomSlideResource
  : PlanningCenterSingletonFetchableResource<CustomSlide, CustomSlideResource, ServicesDocumentContext>
{

  internal CustomSlideResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services CustomSlide resources.
/// </summary>
public class CustomSlideResourceCollection
  : PlanningCenterPaginatedFetchableResource<CustomSlide, CustomSlideResourceCollection, CustomSlideResource, ServicesDocumentContext>
{
  internal CustomSlideResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

