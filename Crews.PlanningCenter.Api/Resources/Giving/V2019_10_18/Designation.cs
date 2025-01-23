/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// A fetchable Giving Designation resource.
/// </summary>
public class DesignationResource
  : PlanningCenterSingletonFetchableResource<Designation, DesignationResource, GivingDocumentContext>
{

  /// <summary>
  /// The related <see cref="FundResource" />.
  /// </summary>
  public FundResource Fund => GetRelated<FundResource>("fund");

  internal DesignationResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Giving Designation resources.
/// </summary>
public class DesignationResourceCollection
  : PlanningCenterPaginatedFetchableResource<Designation, DesignationResourceCollection, DesignationResource, GivingDocumentContext>
{
  internal DesignationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

