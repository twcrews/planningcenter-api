/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2020_07_22.Entities;
using Crews.PlanningCenter.Models.People.V2020_07_22.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2020_07_22;

/// <summary>
/// A fetchable People Person resource.
/// </summary>
public class PersonResource
  : PlanningCenterSingletonFetchableResource<Person, PersonResource, PeopleDocumentContext>,
  IIncludable<PersonResource, PersonIncludable>
{

  /// <summary>
  /// The related <see cref="AddressResourceCollection" />.
  /// </summary>
  public AddressResourceCollection Addresses => GetRelated<AddressResourceCollection>("addresses");

  /// <summary>
  /// The related <see cref="AppResourceCollection" />.
  /// </summary>
  public AppResourceCollection Apps => GetRelated<AppResourceCollection>("apps");

  /// <summary>
  /// The related <see cref="ConnectedPersonResource" />.
  /// </summary>
  public ConnectedPersonResource ConnectedPeople => GetRelated<ConnectedPersonResource>("connected_people");

  /// <summary>
  /// The related <see cref="EmailResourceCollection" />.
  /// </summary>
  public EmailResourceCollection Emails => GetRelated<EmailResourceCollection>("emails");

  /// <summary>
  /// The related <see cref="FieldDatumResource" />.
  /// </summary>
  public FieldDatumResource FieldData => GetRelated<FieldDatumResource>("field_data");

  /// <summary>
  /// The related <see cref="HouseholdMembershipResourceCollection" />.
  /// </summary>
  public HouseholdMembershipResourceCollection HouseholdMemberships => GetRelated<HouseholdMembershipResourceCollection>("household_memberships");

  /// <summary>
  /// The related <see cref="HouseholdResourceCollection" />.
  /// </summary>
  public HouseholdResourceCollection Households => GetRelated<HouseholdResourceCollection>("households");

  /// <summary>
  /// The related <see cref="InactiveReasonResource" />.
  /// </summary>
  public InactiveReasonResource InactiveReason => GetRelated<InactiveReasonResource>("inactive_reason");

  /// <summary>
  /// The related <see cref="MaritalStatusResource" />.
  /// </summary>
  public MaritalStatusResource MaritalStatus => GetRelated<MaritalStatusResource>("marital_status");

  /// <summary>
  /// The related <see cref="MessageGroupResourceCollection" />.
  /// </summary>
  public MessageGroupResourceCollection MessageGroups => GetRelated<MessageGroupResourceCollection>("message_groups");

  /// <summary>
  /// The related <see cref="MessageResourceCollection" />.
  /// </summary>
  public MessageResourceCollection Messages => GetRelated<MessageResourceCollection>("messages");

  /// <summary>
  /// The related <see cref="NamePrefixResource" />.
  /// </summary>
  public NamePrefixResource NamePrefix => GetRelated<NamePrefixResource>("name_prefix");

  /// <summary>
  /// The related <see cref="NameSuffixResource" />.
  /// </summary>
  public NameSuffixResource NameSuffix => GetRelated<NameSuffixResource>("name_suffix");

  /// <summary>
  /// The related <see cref="NoteResourceCollection" />.
  /// </summary>
  public NoteResourceCollection Notes => GetRelated<NoteResourceCollection>("notes");

  /// <summary>
  /// The related <see cref="OrganizationResource" />.
  /// </summary>
  public OrganizationResource Organization => GetRelated<OrganizationResource>("organization");

  /// <summary>
  /// The related <see cref="PersonAppResourceCollection" />.
  /// </summary>
  public PersonAppResourceCollection PersonApps => GetRelated<PersonAppResourceCollection>("person_apps");

  /// <summary>
  /// The related <see cref="PhoneNumberResourceCollection" />.
  /// </summary>
  public PhoneNumberResourceCollection PhoneNumbers => GetRelated<PhoneNumberResourceCollection>("phone_numbers");

  /// <summary>
  /// The related <see cref="PlatformNotificationResourceCollection" />.
  /// </summary>
  public PlatformNotificationResourceCollection PlatformNotifications => GetRelated<PlatformNotificationResourceCollection>("platform_notifications");

  /// <summary>
  /// The related <see cref="CampusResource" />.
  /// </summary>
  public CampusResource PrimaryCampus => GetRelated<CampusResource>("primary_campus");

  /// <summary>
  /// The related <see cref="SchoolOptionResource" />.
  /// </summary>
  public SchoolOptionResource School => GetRelated<SchoolOptionResource>("school");

  /// <summary>
  /// The related <see cref="SocialProfileResourceCollection" />.
  /// </summary>
  public SocialProfileResourceCollection SocialProfiles => GetRelated<SocialProfileResourceCollection>("social_profiles");

  /// <summary>
  /// The related <see cref="WorkflowCardResourceCollection" />.
  /// </summary>
  public WorkflowCardResourceCollection WorkflowCards => GetRelated<WorkflowCardResourceCollection>("workflow_cards");

  /// <summary>
  /// The related <see cref="WorkflowShareResourceCollection" />.
  /// </summary>
  public WorkflowShareResourceCollection WorkflowShares => GetRelated<WorkflowShareResourceCollection>("workflow_shares");

  internal PersonResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PersonResource Include(params PersonIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People Person resources.
/// </summary>
public class PersonResourceCollection
  : PlanningCenterPaginatedFetchableResource<Person, PersonResourceCollection, PersonResource, PeopleDocumentContext>,
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
  public PersonResourceCollection Query(params KeyValuePair<PersonQueryable, string>[] queries)
    => base.Query(queries);
}

