/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2018_08_01;

/// <summary>
/// A fetchable Giving PaymentSource resource.
/// </summary>
public class PaymentSourceResource
  : PlanningCenterSingletonFetchableResource<PaymentSource, PaymentSourceResource, GivingDocumentContext>
{

  /// <summary>
  /// The related <see cref="DonationResourceCollection" />.
  /// </summary>
  public DonationResourceCollection Donations => GetRelated<DonationResourceCollection>("donations");

  internal PaymentSourceResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Giving PaymentSource resources.
/// </summary>
public class PaymentSourceResourceCollection
  : PlanningCenterPaginatedFetchableResource<PaymentSource, PaymentSourceResourceCollection, PaymentSourceResource, GivingDocumentContext>
{
  internal PaymentSourceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

