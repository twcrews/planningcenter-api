/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using Crews.PlanningCenter.Models.Giving.V2019_10_18.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// A fetchable Giving Campus resource.
/// </summary>
public class CampusResource
  : PlanningCenterSingletonFetchableResource<Campus, CampusResource, GivingDocumentContext>
{

  /// <summary>
  /// The related <see cref="DonationResourceCollection" />.
  /// </summary>
  public DonationResourceCollection Donations => GetRelated<DonationResourceCollection>("donations");

  internal CampusResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Giving Campus resources.
/// </summary>
public class CampusResourceCollection
  : PlanningCenterPaginatedFetchableResource<Campus, CampusResourceCollection, CampusResource, GivingDocumentContext>
{
  internal CampusResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

