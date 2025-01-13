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
/// A fetchable Giving DesignationRefund resource.
/// </summary>
public class DesignationRefundResource
  : PlanningCenterSingletonFetchableResource<DesignationRefund, DesignationRefundResource, GivingDocumentContext>,
  IIncludable<DesignationRefundResource, DesignationRefundIncludable>
{

  /// <summary>
  /// The related <see cref="DesignationResource" />.
  /// </summary>
  public DesignationResource Designation => GetRelated<DesignationResource>("designation");

  internal DesignationRefundResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public DesignationRefundResource Include(params DesignationRefundIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Giving DesignationRefund resources.
/// </summary>
public class DesignationRefundResourceCollection
  : PlanningCenterPaginatedFetchableResource<DesignationRefund, DesignationRefundResourceCollection, DesignationRefundResource, GivingDocumentContext>,
  IIncludable<DesignationRefundResourceCollection, DesignationRefundIncludable>
{
  internal DesignationRefundResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public DesignationRefundResourceCollection Include(params DesignationRefundIncludable[] included)
    => base.Include(included);
}

