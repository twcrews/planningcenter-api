/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services EmailTemplateRenderedResponse resource.
/// </summary>
public class EmailTemplateRenderedResponseResource
  : PlanningCenterSingletonFetchableResource<EmailTemplateRenderedResponse, EmailTemplateRenderedResponseResource, ServicesDocumentContext>
{

  internal EmailTemplateRenderedResponseResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services EmailTemplateRenderedResponse resources.
/// </summary>
public class EmailTemplateRenderedResponseResourceCollection
  : PlanningCenterPaginatedFetchableResource<EmailTemplateRenderedResponse, EmailTemplateRenderedResponseResourceCollection, EmailTemplateRenderedResponseResource, ServicesDocumentContext>
{
  internal EmailTemplateRenderedResponseResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

