/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Entities;
using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2022_07_07;

/// <summary>
/// A fetchable Calendar ReportTemplate resource.
/// </summary>
public class ReportTemplateResource
  : PlanningCenterSingletonFetchableResource<ReportTemplate, ReportTemplateResource, CalendarDocumentContext>
{

  internal ReportTemplateResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Calendar ReportTemplate resources.
/// </summary>
public class ReportTemplateResourceCollection
  : PlanningCenterPaginatedFetchableResource<ReportTemplate, ReportTemplateResourceCollection, ReportTemplateResource, CalendarDocumentContext>
{
  internal ReportTemplateResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

