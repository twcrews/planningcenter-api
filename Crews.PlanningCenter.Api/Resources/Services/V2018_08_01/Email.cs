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
/// A fetchable Services Email resource.
/// </summary>
public class EmailResource
  : PlanningCenterSingletonFetchableResource<Email, EmailResource, ServicesDocumentContext>
{

  internal EmailResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services Email resources.
/// </summary>
public class EmailResourceCollection
  : PlanningCenterPaginatedFetchableResource<Email, EmailResourceCollection, EmailResource, ServicesDocumentContext>
{
  internal EmailResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

