/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2024_09_12.Entities;
using Crews.PlanningCenter.Models.People.V2024_09_12.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2024_09_12;

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

