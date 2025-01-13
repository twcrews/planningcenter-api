/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_04_06.Entities;
using Crews.PlanningCenter.Models.People.V2020_04_06.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_04_06;

/// <summary>
/// A fetchable People OrganizationStatistics resource.
/// </summary>
public class OrganizationStatisticsResource
  : PlanningCenterSingletonFetchableResource<OrganizationStatistics, OrganizationStatisticsResource, PeopleDocumentContext>
{

  internal OrganizationStatisticsResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of People OrganizationStatistics resources.
/// </summary>
public class OrganizationStatisticsResourceCollection
  : PlanningCenterPaginatedFetchableResource<OrganizationStatistics, OrganizationStatisticsResourceCollection, OrganizationStatisticsResource, PeopleDocumentContext>
{
  internal OrganizationStatisticsResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

