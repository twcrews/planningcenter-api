/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2023_04_05.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2023_04_05.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2023_04_05;

/// <summary>
/// A fetchable CheckIns IntegrationLink resource.
/// </summary>
public class IntegrationLinkResource
  : PlanningCenterSingletonFetchableResource<IntegrationLink, IntegrationLinkResource, CheckInsDocumentContext>
{

  internal IntegrationLinkResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of CheckIns IntegrationLink resources.
/// </summary>
public class IntegrationLinkResourceCollection
  : PlanningCenterPaginatedFetchableResource<IntegrationLink, IntegrationLinkResourceCollection, IntegrationLinkResource, CheckInsDocumentContext>,
  IQueryable<IntegrationLinkResourceCollection, IntegrationLinkQueryable>
{
  internal IntegrationLinkResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public IntegrationLinkResourceCollection Query(params KeyValuePair<IntegrationLinkQueryable, string>[] queries)
    => base.Query(queries);
}

