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
/// A fetchable Services ServiceTypePath resource.
/// </summary>
public class ServiceTypePathResource
  : PlanningCenterSingletonFetchableResource<ServiceTypePath, ServiceTypePathResource, ServicesDocumentContext>
{

  internal ServiceTypePathResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services ServiceTypePath resources.
/// </summary>
public class ServiceTypePathResourceCollection
  : PlanningCenterPaginatedFetchableResource<ServiceTypePath, ServiceTypePathResourceCollection, ServiceTypePathResource, ServicesDocumentContext>
{
  internal ServiceTypePathResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

