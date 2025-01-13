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
/// A fetchable Giving Refund resource.
/// </summary>
public class RefundResource
  : PlanningCenterSingletonFetchableResource<Refund, RefundResource, GivingDocumentContext>,
  IIncludable<RefundResource, RefundIncludable>
{

  /// <summary>
  /// The related <see cref="DesignationRefundResourceCollection" />.
  /// </summary>
  public DesignationRefundResourceCollection DesignationRefunds => GetRelated<DesignationRefundResourceCollection>("designation_refunds");

  internal RefundResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public RefundResource Include(params RefundIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Giving Refund resources.
/// </summary>
public class RefundResourceCollection
  : PlanningCenterPaginatedFetchableResource<Refund, RefundResourceCollection, RefundResource, GivingDocumentContext>,
  IIncludable<RefundResourceCollection, RefundIncludable>
{
  internal RefundResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public RefundResourceCollection Include(params RefundIncludable[] included)
    => base.Include(included);
}

