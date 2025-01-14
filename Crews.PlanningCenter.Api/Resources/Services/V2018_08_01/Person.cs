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
/// A fetchable Services Person resource.
/// </summary>
public class PersonResource
  : PlanningCenterSingletonFetchableResource<Person, PersonResource, ServicesDocumentContext>,
  IIncludable<PersonResource, PersonIncludable>
{

  /// <summary>
  /// The related <see cref="AvailableSignupResourceCollection" />.
  /// </summary>
  public AvailableSignupResourceCollection AvailableSignups => GetRelated<AvailableSignupResourceCollection>("available_signups");

  /// <summary>
  /// The related <see cref="BlockoutResourceCollection" />.
  /// </summary>
  public BlockoutResourceCollection Blockouts => GetRelated<BlockoutResourceCollection>("blockouts");

  /// <summary>
  /// The related <see cref="EmailResourceCollection" />.
  /// </summary>
  public EmailResourceCollection Emails => GetRelated<EmailResourceCollection>("emails");

  /// <summary>
  /// The related <see cref="PersonTeamPositionAssignmentResourceCollection" />.
  /// </summary>
  public PersonTeamPositionAssignmentResourceCollection PersonTeamPositionAssignments => GetRelated<PersonTeamPositionAssignmentResourceCollection>("person_team_position_assignments");

  /// <summary>
  /// The related <see cref="PlanPersonResource" />.
  /// </summary>
  public PlanPersonResource PlanPeople => GetRelated<PlanPersonResource>("plan_people");

  /// <summary>
  /// The related <see cref="ScheduleResourceCollection" />.
  /// </summary>
  public ScheduleResourceCollection Schedules => GetRelated<ScheduleResourceCollection>("schedules");

  /// <summary>
  /// The related <see cref="SchedulingPreferenceResourceCollection" />.
  /// </summary>
  public SchedulingPreferenceResourceCollection SchedulingPreferences => GetRelated<SchedulingPreferenceResourceCollection>("scheduling_preferences");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  /// <summary>
  /// The related <see cref="TeamLeaderResourceCollection" />.
  /// </summary>
  public TeamLeaderResourceCollection TeamLeaders => GetRelated<TeamLeaderResourceCollection>("team_leaders");

  /// <summary>
  /// The related <see cref="TextSettingResourceCollection" />.
  /// </summary>
  public TextSettingResourceCollection TextSettings => GetRelated<TextSettingResourceCollection>("text_settings");

  internal PersonResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PersonResource Include(params PersonIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Person>> PatchAsync(Person resource)
    => base.PatchAsync(resource);
}

/// <summary>
/// A fetchable collection of Services Person resources.
/// </summary>
public class PersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<Person, PersonResourceCollection, PersonResource, ServicesDocumentContext>,
  IIncludable<PersonResourceCollection, PersonIncludable>,
  IOrderable<PersonResourceCollection, PersonOrderable>,
  IQueryable<PersonResourceCollection, PersonQueryable>
{
  internal PersonResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PersonResourceCollection Include(params PersonIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PersonResourceCollection OrderBy(PersonOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public PersonResourceCollection Query(params (PersonQueryable, string)[] queries)
    => base.Query(queries);
}

