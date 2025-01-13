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
/// A fetchable Giving Person resource.
/// </summary>
public class PersonResource
  : PlanningCenterSingletonFetchableResource<Person, PersonResource, GivingDocumentContext>
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
  /// The related <see cref="DonationResourceCollection" />.
  /// </summary>
  public DonationResourceCollection Donations => GetRelated<DonationResourceCollection>("donations");

  /// <summary>
  /// The related <see cref="PaymentMethodResourceCollection" />.
  /// </summary>
  public PaymentMethodResourceCollection PaymentMethods => GetRelated<PaymentMethodResourceCollection>("payment_methods");

  /// <summary>
  /// The related <see cref="PledgeResourceCollection" />.
  /// </summary>
  public PledgeResourceCollection Pledges => GetRelated<PledgeResourceCollection>("pledges");

  /// <summary>
  /// The related <see cref="CampusResource" />.
  /// </summary>
  public CampusResource PrimaryCampus => GetRelated<CampusResource>("primary_campus");

  /// <summary>
  /// The related <see cref="RecurringDonationResourceCollection" />.
  /// </summary>
  public RecurringDonationResourceCollection RecurringDonations => GetRelated<RecurringDonationResourceCollection>("recurring_donations");

  internal PersonResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Giving Person resources.
/// </summary>
public class PersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<Person, PersonResourceCollection, PersonResource, GivingDocumentContext>,
  IQueryable<PersonResourceCollection, PersonQueryable>
{
  internal PersonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PersonResourceCollection Query(params KeyValuePair<PersonQueryable, string>[] queries)
    => base.Query(queries);
}

