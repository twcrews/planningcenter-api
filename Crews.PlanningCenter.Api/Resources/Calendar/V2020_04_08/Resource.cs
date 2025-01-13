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
/// A fetchable Calendar Resource resource.
/// </summary>
public class ResourceResource
  : PlanningCenterSingletonFetchableResource<Resource, ResourceResource, CalendarDocumentContext>,
  IIncludable<ResourceResource, ResourceIncludable>
{

  /// <summary>
  /// The related <see cref="ConflictResourceCollection" />.
  /// </summary>
  public ConflictResourceCollection Conflicts => GetRelated<ConflictResourceCollection>("conflicts");

  /// <summary>
  /// The related <see cref="EventResourceRequestResourceCollection" />.
  /// </summary>
  public EventResourceRequestResourceCollection EventResourceRequests => GetRelated<EventResourceRequestResourceCollection>("event_resource_requests");

  /// <summary>
  /// The related <see cref="RequiredApprovalResourceCollection" />.
  /// </summary>
  public RequiredApprovalResourceCollection RequiredApprovals => GetRelated<RequiredApprovalResourceCollection>("required_approvals");

  /// <summary>
  /// The related <see cref="ResourceApprovalGroupResourceCollection" />.
  /// </summary>
  public ResourceApprovalGroupResourceCollection ResourceApprovalGroups => GetRelated<ResourceApprovalGroupResourceCollection>("resource_approval_groups");

  /// <summary>
  /// The related <see cref="ResourceBookingResourceCollection" />.
  /// </summary>
  public ResourceBookingResourceCollection ResourceBookings => GetRelated<ResourceBookingResourceCollection>("resource_bookings");

  /// <summary>
  /// The related <see cref="ResourceFolderResource" />.
  /// </summary>
  public ResourceFolderResource ResourceFolder => GetRelated<ResourceFolderResource>("resource_folder");

  /// <summary>
  /// The related <see cref="ResourceQuestionResourceCollection" />.
  /// </summary>
  public ResourceQuestionResourceCollection ResourceQuestions => GetRelated<ResourceQuestionResourceCollection>("resource_questions");

  /// <summary>
  /// The related <see cref="RoomSetupResourceCollection" />.
  /// </summary>
  public RoomSetupResourceCollection RoomSetups => GetRelated<RoomSetupResourceCollection>("room_setups");

  internal ResourceResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ResourceResource Include(params ResourceIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar Resource resources.
/// </summary>
public class ResourceResourceCollection
  : PlanningCenterPaginatedFetchableResource<Resource, ResourceResourceCollection, ResourceResource, CalendarDocumentContext>,
  IIncludable<ResourceResourceCollection, ResourceIncludable>,
  IOrderable<ResourceResourceCollection, ResourceOrderable>,
  IQueryable<ResourceResourceCollection, ResourceQueryable>
{
  internal ResourceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ResourceResourceCollection Include(params ResourceIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ResourceResourceCollection OrderBy(ResourceOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ResourceResourceCollection Query(params KeyValuePair<ResourceQueryable, string>[] queries)
    => base.Query(queries);
}

