/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2018_08_01;

/// <summary>
/// A fetchable Giving Organization resource.
/// </summary>
public class OrganizationResource
  : PlanningCenterSingletonFetchableResource<Organization, OrganizationResource, GivingDocumentContext>
{

  /// <summary>
  /// The related <see cref="BatchGroupResourceCollection" />.
  /// </summary>
  public BatchGroupResourceCollection BatchGroups => GetRelated<BatchGroupResourceCollection>("batch_groups");

  /// <summary>
  /// The related <see cref="BatchResourceCollection" />.
  /// </summary>
  public BatchResourceCollection Batches => GetRelated<BatchResourceCollection>("batches");

  /// <summary>
  /// The related <see cref="CampusResourceCollection" />.
  /// </summary>
  public CampusResourceCollection Campuses => GetRelated<CampusResourceCollection>("campuses");

  /// <summary>
  /// The related <see cref="DonationResourceCollection" />.
  /// </summary>
  public DonationResourceCollection Donations => GetRelated<DonationResourceCollection>("donations");

  /// <summary>
  /// The related <see cref="FundResourceCollection" />.
  /// </summary>
  public FundResourceCollection Funds => GetRelated<FundResourceCollection>("funds");

  /// <summary>
  /// The related <see cref="LabelResourceCollection" />.
  /// </summary>
  public LabelResourceCollection Labels => GetRelated<LabelResourceCollection>("labels");

  /// <summary>
  /// The related <see cref="PaymentSourceResourceCollection" />.
  /// </summary>
  public PaymentSourceResourceCollection PaymentSources => GetRelated<PaymentSourceResourceCollection>("payment_sources");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource People => GetRelated<PersonResource>("people");

  /// <summary>
  /// The related <see cref="RecurringDonationResourceCollection" />.
  /// </summary>
  public RecurringDonationResourceCollection RecurringDonations => GetRelated<RecurringDonationResourceCollection>("recurring_donations");

  internal OrganizationResource(Uri uri, HttpClient client) : base(uri, client) { }
}


