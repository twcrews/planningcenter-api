/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2019_07_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2019_07_17;

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
  public IntegrationLinkResourceCollection Query(params (IntegrationLinkQueryable, string)[] queries)
    => base.Query(queries);
}

