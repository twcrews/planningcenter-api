/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services ReportTemplate resource.
/// </summary>
public class ReportTemplateResource
  : PlanningCenterSingletonFetchableResource<ReportTemplate, ReportTemplateResource, ServicesDocumentContext>
{

  internal ReportTemplateResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services ReportTemplate resources.
/// </summary>
public class ReportTemplateResourceCollection
  : PlanningCenterPaginatedFetchableResource<ReportTemplate, ReportTemplateResourceCollection, ReportTemplateResource, ServicesDocumentContext>
{
  internal ReportTemplateResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

