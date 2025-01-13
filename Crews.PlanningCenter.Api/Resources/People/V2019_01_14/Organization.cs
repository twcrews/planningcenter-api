/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_01_14.Entities;
using Crews.PlanningCenter.Models.People.V2019_01_14.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_01_14;

/// <summary>
/// A fetchable People Organization resource.
/// </summary>
public class OrganizationResource
  : PlanningCenterSingletonFetchableResource<Organization, OrganizationResource, PeopleDocumentContext>
{

  /// <summary>
  /// The related <see cref="AddressResourceCollection" />.
  /// </summary>
  public AddressResourceCollection Addresses => GetRelated<AddressResourceCollection>("addresses");

  /// <summary>
  /// The related <see cref="AnniversaryCouplesResourceCollection" />.
  /// </summary>
  public AnniversaryCouplesResourceCollection AnniversaryCouples => GetRelated<AnniversaryCouplesResourceCollection>("anniversary_couples");

  /// <summary>
  /// The related <see cref="AppResourceCollection" />.
  /// </summary>
  public AppResourceCollection Apps => GetRelated<AppResourceCollection>("apps");

  /// <summary>
  /// The related <see cref="BirthdayPeopleResource" />.
  /// </summary>
  public BirthdayPeopleResource BirthdayPeople => GetRelated<BirthdayPeopleResource>("birthday_people");

  /// <summary>
  /// The related <see cref="CampusResourceCollection" />.
  /// </summary>
  public CampusResourceCollection Campuses => GetRelated<CampusResourceCollection>("campuses");

  /// <summary>
  /// The related <see cref="CarrierResourceCollection" />.
  /// </summary>
  public CarrierResourceCollection Carriers => GetRelated<CarrierResourceCollection>("carriers");

  /// <summary>
  /// The related <see cref="EmailResourceCollection" />.
  /// </summary>
  public EmailResourceCollection Emails => GetRelated<EmailResourceCollection>("emails");

  /// <summary>
  /// The related <see cref="FieldDatumResource" />.
  /// </summary>
  public FieldDatumResource FieldData => GetRelated<FieldDatumResource>("field_data");

  /// <summary>
  /// The related <see cref="FieldDefinitionResourceCollection" />.
  /// </summary>
  public FieldDefinitionResourceCollection FieldDefinitions => GetRelated<FieldDefinitionResourceCollection>("field_definitions");

  /// <summary>
  /// The related <see cref="FormResourceCollection" />.
  /// </summary>
  public FormResourceCollection Forms => GetRelated<FormResourceCollection>("forms");

  /// <summary>
  /// The related <see cref="HouseholdResourceCollection" />.
  /// </summary>
  public HouseholdResourceCollection Households => GetRelated<HouseholdResourceCollection>("households");

  /// <summary>
  /// The related <see cref="InactiveReasonResourceCollection" />.
  /// </summary>
  public InactiveReasonResourceCollection InactiveReasons => GetRelated<InactiveReasonResourceCollection>("inactive_reasons");

  /// <summary>
  /// The related <see cref="ListCategoryResourceCollection" />.
  /// </summary>
  public ListCategoryResourceCollection ListCategories => GetRelated<ListCategoryResourceCollection>("list_categories");

  /// <summary>
  /// The related <see cref="ListResourceCollection" />.
  /// </summary>
  public ListResourceCollection Lists => GetRelated<ListResourceCollection>("lists");

  /// <summary>
  /// The related <see cref="MaritalStatusResourceCollection" />.
  /// </summary>
  public MaritalStatusResourceCollection MaritalStatuses => GetRelated<MaritalStatusResourceCollection>("marital_statuses");

  /// <summary>
  /// The related <see cref="MessageGroupResourceCollection" />.
  /// </summary>
  public MessageGroupResourceCollection MessageGroups => GetRelated<MessageGroupResourceCollection>("message_groups");

  /// <summary>
  /// The related <see cref="MessageResourceCollection" />.
  /// </summary>
  public MessageResourceCollection Messages => GetRelated<MessageResourceCollection>("messages");

  /// <summary>
  /// The related <see cref="NamePrefixResourceCollection" />.
  /// </summary>
  public NamePrefixResourceCollection NamePrefixes => GetRelated<NamePrefixResourceCollection>("name_prefixes");

  /// <summary>
  /// The related <see cref="NameSuffixResourceCollection" />.
  /// </summary>
  public NameSuffixResourceCollection NameSuffixes => GetRelated<NameSuffixResourceCollection>("name_suffixes");

  /// <summary>
  /// The related <see cref="NoteCategoryResourceCollection" />.
  /// </summary>
  public NoteCategoryResourceCollection NoteCategories => GetRelated<NoteCategoryResourceCollection>("note_categories");

  /// <summary>
  /// The related <see cref="NoteCategorySubscriptionResourceCollection" />.
  /// </summary>
  public NoteCategorySubscriptionResourceCollection NoteCategorySubscriptions => GetRelated<NoteCategorySubscriptionResourceCollection>("note_category_subscriptions");

  /// <summary>
  /// The related <see cref="NoteResourceCollection" />.
  /// </summary>
  public NoteResourceCollection Notes => GetRelated<NoteResourceCollection>("notes");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource People => GetRelated<PersonResource>("people");

  /// <summary>
  /// The related <see cref="PeopleImportResourceCollection" />.
  /// </summary>
  public PeopleImportResourceCollection PeopleImports => GetRelated<PeopleImportResourceCollection>("people_imports");

  /// <summary>
  /// The related <see cref="PersonMergerResourceCollection" />.
  /// </summary>
  public PersonMergerResourceCollection PersonMergers => GetRelated<PersonMergerResourceCollection>("person_mergers");

  /// <summary>
  /// The related <see cref="PhoneNumberResourceCollection" />.
  /// </summary>
  public PhoneNumberResourceCollection PhoneNumbers => GetRelated<PhoneNumberResourceCollection>("phone_numbers");

  /// <summary>
  /// The related <see cref="ReportResourceCollection" />.
  /// </summary>
  public ReportResourceCollection Reports => GetRelated<ReportResourceCollection>("reports");

  /// <summary>
  /// The related <see cref="SchoolOptionResourceCollection" />.
  /// </summary>
  public SchoolOptionResourceCollection SchoolOptions => GetRelated<SchoolOptionResourceCollection>("school_options");

  /// <summary>
  /// The related <see cref="SocialProfileResourceCollection" />.
  /// </summary>
  public SocialProfileResourceCollection SocialProfiles => GetRelated<SocialProfileResourceCollection>("social_profiles");

  /// <summary>
  /// The related <see cref="SpamEmailAddressResourceCollection" />.
  /// </summary>
  public SpamEmailAddressResourceCollection SpamEmailAddresses => GetRelated<SpamEmailAddressResourceCollection>("spam_email_addresses");

  /// <summary>
  /// The related <see cref="OrganizationStatisticsResourceCollection" />.
  /// </summary>
  public OrganizationStatisticsResourceCollection Stats => GetRelated<OrganizationStatisticsResourceCollection>("stats");

  /// <summary>
  /// The related <see cref="TabResourceCollection" />.
  /// </summary>
  public TabResourceCollection Tabs => GetRelated<TabResourceCollection>("tabs");

  /// <summary>
  /// The related <see cref="WorkflowResourceCollection" />.
  /// </summary>
  public WorkflowResourceCollection Workflows => GetRelated<WorkflowResourceCollection>("workflows");

  internal OrganizationResource(Uri uri, HttpClient client) : base(uri, client) { }
}


