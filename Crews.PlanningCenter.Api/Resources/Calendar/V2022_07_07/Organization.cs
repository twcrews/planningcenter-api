/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Entities;
using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2022_07_07;

/// <summary>
/// A fetchable Calendar Organization resource.
/// </summary>
public class OrganizationResource
  : PlanningCenterSingletonFetchableResource<Organization, OrganizationResource, CalendarDocumentContext>
{

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  /// <summary>
  /// The related <see cref="ConflictResourceCollection" />.
  /// </summary>
  public ConflictResourceCollection Conflicts => GetRelated<ConflictResourceCollection>("conflicts");

  /// <summary>
  /// The related <see cref="EventInstanceResourceCollection" />.
  /// </summary>
  public EventInstanceResourceCollection EventInstances => GetRelated<EventInstanceResourceCollection>("event_instances");

  /// <summary>
  /// The related <see cref="EventResourceRequestResourceCollection" />.
  /// </summary>
  public EventResourceRequestResourceCollection EventResourceRequests => GetRelated<EventResourceRequestResourceCollection>("event_resource_requests");

  /// <summary>
  /// The related <see cref="EventResourceCollection" />.
  /// </summary>
  public EventResourceCollection Events => GetRelated<EventResourceCollection>("events");

  /// <summary>
  /// The related <see cref="FeedResourceCollection" />.
  /// </summary>
  public FeedResourceCollection Feeds => GetRelated<FeedResourceCollection>("feeds");

  /// <summary>
  /// The related <see cref="JobStatusResourceCollection" />.
  /// </summary>
  public JobStatusResourceCollection JobStatuses => GetRelated<JobStatusResourceCollection>("job_statuses");

  /// <summary>
  /// The related <see cref="PersonResourceCollection" />.
  /// </summary>
  public PersonResourceCollection People => GetRelated<PersonResourceCollection>("people");

  /// <summary>
  /// The related <see cref="ReportTemplateResourceCollection" />.
  /// </summary>
  public ReportTemplateResourceCollection ReportTemplates => GetRelated<ReportTemplateResourceCollection>("report_templates");

  /// <summary>
  /// The related <see cref="ResourceApprovalGroupResourceCollection" />.
  /// </summary>
  public ResourceApprovalGroupResourceCollection ResourceApprovalGroups => GetRelated<ResourceApprovalGroupResourceCollection>("resource_approval_groups");

  /// <summary>
  /// The related <see cref="ResourceBookingResourceCollection" />.
  /// </summary>
  public ResourceBookingResourceCollection ResourceBookings => GetRelated<ResourceBookingResourceCollection>("resource_bookings");

  /// <summary>
  /// The related <see cref="ResourceFolderResourceCollection" />.
  /// </summary>
  public ResourceFolderResourceCollection ResourceFolders => GetRelated<ResourceFolderResourceCollection>("resource_folders");

  /// <summary>
  /// The related <see cref="ResourceQuestionResourceCollection" />.
  /// </summary>
  public ResourceQuestionResourceCollection ResourceQuestions => GetRelated<ResourceQuestionResourceCollection>("resource_questions");

  /// <summary>
  /// The related <see cref="ResourceResourceCollection" />.
  /// </summary>
  public ResourceResourceCollection Resources => GetRelated<ResourceResourceCollection>("resources");

  /// <summary>
  /// The related <see cref="RoomSetupResourceCollection" />.
  /// </summary>
  public RoomSetupResourceCollection RoomSetups => GetRelated<RoomSetupResourceCollection>("room_setups");

  /// <summary>
  /// The related <see cref="TagGroupResourceCollection" />.
  /// </summary>
  public TagGroupResourceCollection TagGroups => GetRelated<TagGroupResourceCollection>("tag_groups");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal OrganizationResource(Uri uri, HttpClient client) : base(uri, client) { }
}


