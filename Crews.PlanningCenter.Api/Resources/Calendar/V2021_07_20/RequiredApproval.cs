/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Entities;
using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2021_07_20;

/// <summary>
/// A fetchable Calendar RequiredApproval resource.
/// </summary>
public class RequiredApprovalResource
  : PlanningCenterSingletonFetchableResource<RequiredApproval, RequiredApprovalResource, CalendarDocumentContext>,
  IIncludable<RequiredApprovalResource, RequiredApprovalIncludable>
{

  /// <summary>
  /// The related <see cref="ResourceResource" />.
  /// </summary>
  public ResourceResource Resource => GetRelated<ResourceResource>("resource");

  internal RequiredApprovalResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public RequiredApprovalResource Include(params RequiredApprovalIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar RequiredApproval resources.
/// </summary>
public class RequiredApprovalResourceCollection
  : PlanningCenterPaginatedFetchableResource<RequiredApproval, RequiredApprovalResourceCollection, RequiredApprovalResource, CalendarDocumentContext>,
  IIncludable<RequiredApprovalResourceCollection, RequiredApprovalIncludable>
{
  internal RequiredApprovalResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public RequiredApprovalResourceCollection Include(params RequiredApprovalIncludable[] included)
    => base.Include(included);
}

