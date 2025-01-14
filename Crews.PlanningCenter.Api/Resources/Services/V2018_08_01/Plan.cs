/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services Plan resource.
/// </summary>
public class PlanResource
  : PlanningCenterSingletonFetchableResource<Plan, PlanResource, ServicesDocumentContext>,
  IIncludable<PlanResource, PlanIncludable>
{

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection AllAttachments => GetRelated<AttachmentResourceCollection>("all_attachments");

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  /// <summary>
  /// The related <see cref="AttendanceResourceCollection" />.
  /// </summary>
  public AttendanceResourceCollection Attendances => GetRelated<AttendanceResourceCollection>("attendances");

  /// <summary>
  /// The related <see cref="ContributorResourceCollection" />.
  /// </summary>
  public ContributorResourceCollection Contributors => GetRelated<ContributorResourceCollection>("contributors");

  /// <summary>
  /// The related <see cref="ItemResourceCollection" />.
  /// </summary>
  public ItemResourceCollection Items => GetRelated<ItemResourceCollection>("items");

  /// <summary>
  /// The related <see cref="LiveResource" />.
  /// </summary>
  public LiveResource Live => GetRelated<LiveResource>("live");

  /// <summary>
  /// The related <see cref="ScheduleResourceCollection" />.
  /// </summary>
  public ScheduleResourceCollection MySchedules => GetRelated<ScheduleResourceCollection>("my_schedules");

  /// <summary>
  /// The related <see cref="NeededPositionResourceCollection" />.
  /// </summary>
  public NeededPositionResourceCollection NeededPositions => GetRelated<NeededPositionResourceCollection>("needed_positions");

  /// <summary>
  /// The related <see cref="PlanResource" />.
  /// </summary>
  public PlanResource NextPlan => GetRelated<PlanResource>("next_plan");

  /// <summary>
  /// The related <see cref="PlanNoteResourceCollection" />.
  /// </summary>
  public PlanNoteResourceCollection Notes => GetRelated<PlanNoteResourceCollection>("notes");

  /// <summary>
  /// The related <see cref="PlanTimeResourceCollection" />.
  /// </summary>
  public PlanTimeResourceCollection PlanTimes => GetRelated<PlanTimeResourceCollection>("plan_times");

  /// <summary>
  /// The related <see cref="PlanResource" />.
  /// </summary>
  public PlanResource PreviousPlan => GetRelated<PlanResource>("previous_plan");

  /// <summary>
  /// The related <see cref="SeriesResourceCollection" />.
  /// </summary>
  public SeriesResourceCollection Series => GetRelated<SeriesResourceCollection>("series");

  /// <summary>
  /// The related <see cref="TeamResourceCollection" />.
  /// </summary>
  public TeamResourceCollection SignupTeams => GetRelated<TeamResourceCollection>("signup_teams");

  /// <summary>
  /// The related <see cref="PlanPersonResourceCollection" />.
  /// </summary>
  public PlanPersonResourceCollection TeamMembers => GetRelated<PlanPersonResourceCollection>("team_members");

  internal PlanResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PlanResource Include(params PlanIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Plan>> PostAsync(Plan resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Plan>> PatchAsync(Plan resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services Plan resources.
/// </summary>
public class PlanResourceCollection
  : PlanningCenterPaginatedFetchableResource<Plan, PlanResourceCollection, PlanResource, ServicesDocumentContext>,
  IIncludable<PlanResourceCollection, PlanIncludable>,
  IOrderable<PlanResourceCollection, PlanOrderable>,
  IQueryable<PlanResourceCollection, PlanQueryable>
{
  internal PlanResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PlanResourceCollection Include(params PlanIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PlanResourceCollection OrderBy(PlanOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PlanResourceCollection Query(params (PlanQueryable, string)[] queries)
    => base.Query(queries);
}

