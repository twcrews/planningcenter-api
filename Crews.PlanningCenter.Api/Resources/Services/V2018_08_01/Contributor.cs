/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services Contributor resource.
/// </summary>
public class ContributorResource
  : PlanningCenterSingletonFetchableResource<Contributor, ContributorResource, ServicesDocumentContext>
{

  internal ContributorResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services Contributor resources.
/// </summary>
public class ContributorResourceCollection
  : PlanningCenterPaginatedFetchableResource<Contributor, ContributorResourceCollection, ContributorResource, ServicesDocumentContext>,
  IOrderable<ContributorResourceCollection, ContributorOrderable>
{
  internal ContributorResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ContributorResourceCollection OrderBy(ContributorOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

