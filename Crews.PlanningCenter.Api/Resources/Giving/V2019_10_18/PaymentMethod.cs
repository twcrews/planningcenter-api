/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using Crews.PlanningCenter.Models.Giving.V2019_10_18.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// A fetchable Giving PaymentMethod resource.
/// </summary>
public class PaymentMethodResource
  : PlanningCenterSingletonFetchableResource<PaymentMethod, PaymentMethodResource, GivingDocumentContext>
{

  /// <summary>
  /// The related <see cref="RecurringDonationResourceCollection" />.
  /// </summary>
  public RecurringDonationResourceCollection RecurringDonations => GetRelated<RecurringDonationResourceCollection>("recurring_donations");

  internal PaymentMethodResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Giving PaymentMethod resources.
/// </summary>
public class PaymentMethodResourceCollection
  : PlanningCenterPaginatedFetchableResource<PaymentMethod, PaymentMethodResourceCollection, PaymentMethodResource, GivingDocumentContext>
{
  internal PaymentMethodResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

