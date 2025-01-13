/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services EmailTemplate resource.
/// </summary>
public class EmailTemplateResource
  : PlanningCenterSingletonFetchableResource<EmailTemplate, EmailTemplateResource, ServicesDocumentContext>
{

  internal EmailTemplateResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services EmailTemplate resources.
/// </summary>
public class EmailTemplateResourceCollection
  : PlanningCenterPaginatedFetchableResource<EmailTemplate, EmailTemplateResourceCollection, EmailTemplateResource, ServicesDocumentContext>
{
  internal EmailTemplateResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

