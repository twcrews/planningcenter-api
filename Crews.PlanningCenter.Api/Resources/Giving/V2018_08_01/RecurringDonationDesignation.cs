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
/// A fetchable Giving RecurringDonationDesignation resource.
/// </summary>
public class RecurringDonationDesignationResource
  : PlanningCenterSingletonFetchableResource<RecurringDonationDesignation, RecurringDonationDesignationResource, GivingDocumentContext>,
  IIncludable<RecurringDonationDesignationResource, RecurringDonationDesignationIncludable>
{

  /// <summary>
  /// The related <see cref="FundResource" />.
  /// </summary>
  public FundResource Fund => GetRelated<FundResource>("fund");

  internal RecurringDonationDesignationResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public RecurringDonationDesignationResource Include(params RecurringDonationDesignationIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Giving RecurringDonationDesignation resources.
/// </summary>
public class RecurringDonationDesignationResourceCollection
  : PlanningCenterPaginatedFetchableResource<RecurringDonationDesignation, RecurringDonationDesignationResourceCollection, RecurringDonationDesignationResource, GivingDocumentContext>,
  IIncludable<RecurringDonationDesignationResourceCollection, RecurringDonationDesignationIncludable>
{
  internal RecurringDonationDesignationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public RecurringDonationDesignationResourceCollection Include(params RecurringDonationDesignationIncludable[] included)
    => base.Include(included);
}

