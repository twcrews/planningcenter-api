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
/// A fetchable Giving Donation resource.
/// </summary>
public class DonationResource
  : PlanningCenterSingletonFetchableResource<Donation, DonationResource, GivingDocumentContext>,
  IIncludable<DonationResource, DonationIncludable>
{

  /// <summary>
  /// The related <see cref="CampusResource" />.
  /// </summary>
  public CampusResource Campus => GetRelated<CampusResource>("campus");

  /// <summary>
  /// The related <see cref="DesignationResourceCollection" />.
  /// </summary>
  public DesignationResourceCollection Designations => GetRelated<DesignationResourceCollection>("designations");

  /// <summary>
  /// The related <see cref="LabelResourceCollection" />.
  /// </summary>
  public LabelResourceCollection Labels => GetRelated<LabelResourceCollection>("labels");

  /// <summary>
  /// The related <see cref="NoteResource" />.
  /// </summary>
  public NoteResource Note => GetRelated<NoteResource>("note");

  /// <summary>
  /// The related <see cref="RefundResource" />.
  /// </summary>
  public RefundResource Refund => GetRelated<RefundResource>("refund");

  internal DonationResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public DonationResource Include(params DonationIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Giving Donation resources.
/// </summary>
public class DonationResourceCollection
  : PlanningCenterPaginatedFetchableResource<Donation, DonationResourceCollection, DonationResource, GivingDocumentContext>,
  IIncludable<DonationResourceCollection, DonationIncludable>,
  IOrderable<DonationResourceCollection, DonationOrderable>,
  IQueryable<DonationResourceCollection, DonationQueryable>
{
  internal DonationResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public DonationResourceCollection Include(params DonationIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public DonationResourceCollection OrderBy(DonationOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public DonationResourceCollection Query(params KeyValuePair<DonationQueryable, string>[] queries)
    => base.Query(queries);
}

