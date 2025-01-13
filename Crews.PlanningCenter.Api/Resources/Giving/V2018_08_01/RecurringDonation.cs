/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Giving.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2018_08_01;

/// <summary>
/// A fetchable Giving RecurringDonation resource.
/// </summary>
public class RecurringDonationResource
  : PlanningCenterSingletonFetchableResource<RecurringDonation, RecurringDonationResource, GivingDocumentContext>,
  IIncludable<RecurringDonationResource, RecurringDonationIncludable>
{

  /// <summary>
  /// The related <see cref="PaymentMethodResource" />.
  /// </summary>
  public PaymentMethodResource PaymentMethod => GetRelated<PaymentMethodResource>("payment_method");

  /// <summary>
  /// The related <see cref="RecurringDonationDesignationResourceCollection" />.
  /// </summary>
  public RecurringDonationDesignationResourceCollection Designations => GetRelated<RecurringDonationDesignationResourceCollection>("designations");

  internal RecurringDonationResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public RecurringDonationResource Include(params RecurringDonationIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Giving RecurringDonation resources.
/// </summary>
public class RecurringDonationResourceCollection
  : PlanningCenterPaginatedFetchableResource<RecurringDonation, RecurringDonationResourceCollection, RecurringDonationResource, GivingDocumentContext>,
  IIncludable<RecurringDonationResourceCollection, RecurringDonationIncludable>
{
  internal RecurringDonationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public RecurringDonationResourceCollection Include(params RecurringDonationIncludable[] included)
    => base.Include(included);
}

