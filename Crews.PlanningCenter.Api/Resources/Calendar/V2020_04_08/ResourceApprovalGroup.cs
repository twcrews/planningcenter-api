/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2020_04_08.Entities;
using Crews.PlanningCenter.Models.Calendar.V2020_04_08.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2020_04_08;

/// <summary>
/// A fetchable Calendar ResourceApprovalGroup resource.
/// </summary>
public class ResourceApprovalGroupResource
  : PlanningCenterSingletonFetchableResource<ResourceApprovalGroup, ResourceApprovalGroupResource, CalendarDocumentContext>,
  IIncludable<ResourceApprovalGroupResource, ResourceApprovalGroupIncludable>
{

  /// <summary>
  /// The related <see cref="EventResourceRequestResourceCollection" />.
  /// </summary>
  public EventResourceRequestResourceCollection EventResourceRequests => GetRelated<EventResourceRequestResourceCollection>("event_resource_requests");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource People => GetRelated<PersonResource>("people");

  /// <summary>
  /// The related <see cref="RequiredApprovalResourceCollection" />.
  /// </summary>
  public RequiredApprovalResourceCollection RequiredApprovals => GetRelated<RequiredApprovalResourceCollection>("required_approvals");

  /// <summary>
  /// The related <see cref="ResourceResourceCollection" />.
  /// </summary>
  public ResourceResourceCollection Resources => GetRelated<ResourceResourceCollection>("resources");

  internal ResourceApprovalGroupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ResourceApprovalGroupResource Include(params ResourceApprovalGroupIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar ResourceApprovalGroup resources.
/// </summary>
public class ResourceApprovalGroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<ResourceApprovalGroup, ResourceApprovalGroupResourceCollection, ResourceApprovalGroupResource, CalendarDocumentContext>,
  IIncludable<ResourceApprovalGroupResourceCollection, ResourceApprovalGroupIncludable>,
  IOrderable<ResourceApprovalGroupResourceCollection, ResourceApprovalGroupOrderable>,
  IQueryable<ResourceApprovalGroupResourceCollection, ResourceApprovalGroupQueryable>
{
  internal ResourceApprovalGroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ResourceApprovalGroupResourceCollection Include(params ResourceApprovalGroupIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ResourceApprovalGroupResourceCollection OrderBy(ResourceApprovalGroupOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ResourceApprovalGroupResourceCollection Query(params KeyValuePair<ResourceApprovalGroupQueryable, string>[] queries)
    => base.Query(queries);
}

