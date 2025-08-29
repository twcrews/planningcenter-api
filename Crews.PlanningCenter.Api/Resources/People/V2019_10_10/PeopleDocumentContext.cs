/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_10_10.Entities;
using JsonApiFramework.ServiceModel.Configuration;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_10_10;

/// <summary>
/// JSON API document context for the Planning Center People API.
/// </summary>
public class PeopleDocumentContext : PlanningCenterDocumentContext
{
  internal PeopleDocumentContext(JsonApiFramework.JsonApi.Document document) : base(document) { }
  internal PeopleDocumentContext() : base() { }

  /// <inheritdoc />
  protected override void OnServiceModelCreating(IServiceModelBuilder builder)
  {
    base.OnServiceModelCreating(builder);
  
    builder.Configurations.Add(new AddressConfiguration());
    builder.Configurations.Add(new AppConfiguration());
    builder.Configurations.Add(new BirthdayPeopleConfiguration());
    builder.Configurations.Add(new CampusConfiguration());
    builder.Configurations.Add(new CarrierConfiguration());
    builder.Configurations.Add(new ConditionConfiguration());
    builder.Configurations.Add(new ConnectedPersonConfiguration());
    builder.Configurations.Add(new CustomSenderConfiguration());
    builder.Configurations.Add(new EmailConfiguration());
    builder.Configurations.Add(new FieldDatumConfiguration());
    builder.Configurations.Add(new FieldDefinitionConfiguration());
    builder.Configurations.Add(new FieldOptionConfiguration());
    builder.Configurations.Add(new FormConfiguration());
    builder.Configurations.Add(new FormCategoryConfiguration());
    builder.Configurations.Add(new FormFieldConfiguration());
    builder.Configurations.Add(new FormFieldOptionConfiguration());
    builder.Configurations.Add(new FormSubmissionConfiguration());
    builder.Configurations.Add(new FormSubmissionValueConfiguration());
    builder.Configurations.Add(new HouseholdConfiguration());
    builder.Configurations.Add(new HouseholdMembershipConfiguration());
    builder.Configurations.Add(new InactiveReasonConfiguration());
    builder.Configurations.Add(new ListConfiguration());
    builder.Configurations.Add(new ListCategoryConfiguration());
    builder.Configurations.Add(new ListResultConfiguration());
    builder.Configurations.Add(new ListShareConfiguration());
    builder.Configurations.Add(new ListStarConfiguration());
    builder.Configurations.Add(new MailchimpSyncStatusConfiguration());
    builder.Configurations.Add(new MaritalStatusConfiguration());
    builder.Configurations.Add(new MessageConfiguration());
    builder.Configurations.Add(new MessageGroupConfiguration());
    builder.Configurations.Add(new NamePrefixConfiguration());
    builder.Configurations.Add(new NameSuffixConfiguration());
    builder.Configurations.Add(new NoteConfiguration());
    builder.Configurations.Add(new NoteCategoryConfiguration());
    builder.Configurations.Add(new NoteCategoryShareConfiguration());
    builder.Configurations.Add(new NoteCategorySubscriptionConfiguration());
    builder.Configurations.Add(new OrganizationConfiguration());
    builder.Configurations.Add(new OrganizationStatisticsConfiguration());
    builder.Configurations.Add(new PeopleImportConfiguration());
    builder.Configurations.Add(new PeopleImportConflictConfiguration());
    builder.Configurations.Add(new PeopleImportHistoryConfiguration());
    builder.Configurations.Add(new PersonConfiguration());
    builder.Configurations.Add(new PersonAppConfiguration());
    builder.Configurations.Add(new PersonMergerConfiguration());
    builder.Configurations.Add(new PhoneNumberConfiguration());
    builder.Configurations.Add(new PlatformNotificationConfiguration());
    builder.Configurations.Add(new ReportConfiguration());
    builder.Configurations.Add(new RuleConfiguration());
    builder.Configurations.Add(new SchoolOptionConfiguration());
    builder.Configurations.Add(new ServiceTimeConfiguration());
    builder.Configurations.Add(new SocialProfileConfiguration());
    builder.Configurations.Add(new SpamEmailAddressConfiguration());
    builder.Configurations.Add(new TabConfiguration());
    builder.Configurations.Add(new WorkflowConfiguration());
    builder.Configurations.Add(new WorkflowCardConfiguration());
    builder.Configurations.Add(new WorkflowCardActivityConfiguration());
    builder.Configurations.Add(new WorkflowCardNoteConfiguration());
    builder.Configurations.Add(new WorkflowCategoryConfiguration());
    builder.Configurations.Add(new WorkflowShareConfiguration());
    builder.Configurations.Add(new WorkflowStepConfiguration());
    builder.Configurations.Add(new WorkflowStepAssigneeSummaryConfiguration());
  }

  internal class AddressConfiguration : ResourceTypeBuilder<Address> { public AddressConfiguration() { } }
  internal class AppConfiguration : ResourceTypeBuilder<App> { public AppConfiguration() { } }
  internal class BirthdayPeopleConfiguration : ResourceTypeBuilder<BirthdayPeople> { public BirthdayPeopleConfiguration() { } }
  internal class CampusConfiguration : ResourceTypeBuilder<Campus> { public CampusConfiguration() { } }
  internal class CarrierConfiguration : ResourceTypeBuilder<Carrier> { public CarrierConfiguration() { } }
  internal class ConditionConfiguration : ResourceTypeBuilder<Condition> { public ConditionConfiguration() { } }
  internal class ConnectedPersonConfiguration : ResourceTypeBuilder<ConnectedPerson> { public ConnectedPersonConfiguration() { } }
  internal class CustomSenderConfiguration : ResourceTypeBuilder<CustomSender> { public CustomSenderConfiguration() { } }
  internal class EmailConfiguration : ResourceTypeBuilder<Email> { public EmailConfiguration() { } }
  internal class FieldDatumConfiguration : ResourceTypeBuilder<FieldDatum> { public FieldDatumConfiguration() { } }
  internal class FieldDefinitionConfiguration : ResourceTypeBuilder<FieldDefinition> { public FieldDefinitionConfiguration() { } }
  internal class FieldOptionConfiguration : ResourceTypeBuilder<FieldOption> { public FieldOptionConfiguration() { } }
  internal class FormConfiguration : ResourceTypeBuilder<Form> { public FormConfiguration() { } }
  internal class FormCategoryConfiguration : ResourceTypeBuilder<FormCategory> { public FormCategoryConfiguration() { } }
  internal class FormFieldConfiguration : ResourceTypeBuilder<FormField> { public FormFieldConfiguration() { } }
  internal class FormFieldOptionConfiguration : ResourceTypeBuilder<FormFieldOption> { public FormFieldOptionConfiguration() { } }
  internal class FormSubmissionConfiguration : ResourceTypeBuilder<FormSubmission> { public FormSubmissionConfiguration() { } }
  internal class FormSubmissionValueConfiguration : ResourceTypeBuilder<FormSubmissionValue> { public FormSubmissionValueConfiguration() { } }
  internal class HouseholdConfiguration : ResourceTypeBuilder<Household> { public HouseholdConfiguration() { } }
  internal class HouseholdMembershipConfiguration : ResourceTypeBuilder<HouseholdMembership> { public HouseholdMembershipConfiguration() { } }
  internal class InactiveReasonConfiguration : ResourceTypeBuilder<InactiveReason> { public InactiveReasonConfiguration() { } }
  internal class ListConfiguration : ResourceTypeBuilder<List> { public ListConfiguration() { } }
  internal class ListCategoryConfiguration : ResourceTypeBuilder<ListCategory> { public ListCategoryConfiguration() { } }
  internal class ListResultConfiguration : ResourceTypeBuilder<ListResult> { public ListResultConfiguration() { } }
  internal class ListShareConfiguration : ResourceTypeBuilder<ListShare> { public ListShareConfiguration() { } }
  internal class ListStarConfiguration : ResourceTypeBuilder<ListStar> { public ListStarConfiguration() { } }
  internal class MailchimpSyncStatusConfiguration : ResourceTypeBuilder<MailchimpSyncStatus> { public MailchimpSyncStatusConfiguration() { } }
  internal class MaritalStatusConfiguration : ResourceTypeBuilder<MaritalStatus> { public MaritalStatusConfiguration() { } }
  internal class MessageConfiguration : ResourceTypeBuilder<Message> { public MessageConfiguration() { } }
  internal class MessageGroupConfiguration : ResourceTypeBuilder<MessageGroup> { public MessageGroupConfiguration() { } }
  internal class NamePrefixConfiguration : ResourceTypeBuilder<NamePrefix> { public NamePrefixConfiguration() { } }
  internal class NameSuffixConfiguration : ResourceTypeBuilder<NameSuffix> { public NameSuffixConfiguration() { } }
  internal class NoteConfiguration : ResourceTypeBuilder<Note> { public NoteConfiguration() { } }
  internal class NoteCategoryConfiguration : ResourceTypeBuilder<NoteCategory> { public NoteCategoryConfiguration() { } }
  internal class NoteCategoryShareConfiguration : ResourceTypeBuilder<NoteCategoryShare> { public NoteCategoryShareConfiguration() { } }
  internal class NoteCategorySubscriptionConfiguration : ResourceTypeBuilder<NoteCategorySubscription> { public NoteCategorySubscriptionConfiguration() { } }
  internal class OrganizationConfiguration : ResourceTypeBuilder<Organization> { public OrganizationConfiguration() { } }
  internal class OrganizationStatisticsConfiguration : ResourceTypeBuilder<OrganizationStatistics> { public OrganizationStatisticsConfiguration() { } }
  internal class PeopleImportConfiguration : ResourceTypeBuilder<PeopleImport> { public PeopleImportConfiguration() { } }
  internal class PeopleImportConflictConfiguration : ResourceTypeBuilder<PeopleImportConflict> { public PeopleImportConflictConfiguration() { } }
  internal class PeopleImportHistoryConfiguration : ResourceTypeBuilder<PeopleImportHistory> { public PeopleImportHistoryConfiguration() { } }
  internal class PersonConfiguration : ResourceTypeBuilder<Person> { public PersonConfiguration() { } }
  internal class PersonAppConfiguration : ResourceTypeBuilder<PersonApp> { public PersonAppConfiguration() { } }
  internal class PersonMergerConfiguration : ResourceTypeBuilder<PersonMerger> { public PersonMergerConfiguration() { } }
  internal class PhoneNumberConfiguration : ResourceTypeBuilder<PhoneNumber> { public PhoneNumberConfiguration() { } }
  internal class PlatformNotificationConfiguration : ResourceTypeBuilder<PlatformNotification> { public PlatformNotificationConfiguration() { } }
  internal class ReportConfiguration : ResourceTypeBuilder<Report> { public ReportConfiguration() { } }
  internal class RuleConfiguration : ResourceTypeBuilder<Rule> { public RuleConfiguration() { } }
  internal class SchoolOptionConfiguration : ResourceTypeBuilder<SchoolOption> { public SchoolOptionConfiguration() { } }
  internal class ServiceTimeConfiguration : ResourceTypeBuilder<ServiceTime> { public ServiceTimeConfiguration() { } }
  internal class SocialProfileConfiguration : ResourceTypeBuilder<SocialProfile> { public SocialProfileConfiguration() { } }
  internal class SpamEmailAddressConfiguration : ResourceTypeBuilder<SpamEmailAddress> { public SpamEmailAddressConfiguration() { } }
  internal class TabConfiguration : ResourceTypeBuilder<Tab> { public TabConfiguration() { } }
  internal class WorkflowConfiguration : ResourceTypeBuilder<Workflow> { public WorkflowConfiguration() { } }
  internal class WorkflowCardConfiguration : ResourceTypeBuilder<WorkflowCard> { public WorkflowCardConfiguration() { } }
  internal class WorkflowCardActivityConfiguration : ResourceTypeBuilder<WorkflowCardActivity> { public WorkflowCardActivityConfiguration() { } }
  internal class WorkflowCardNoteConfiguration : ResourceTypeBuilder<WorkflowCardNote> { public WorkflowCardNoteConfiguration() { } }
  internal class WorkflowCategoryConfiguration : ResourceTypeBuilder<WorkflowCategory> { public WorkflowCategoryConfiguration() { } }
  internal class WorkflowShareConfiguration : ResourceTypeBuilder<WorkflowShare> { public WorkflowShareConfiguration() { } }
  internal class WorkflowStepConfiguration : ResourceTypeBuilder<WorkflowStep> { public WorkflowStepConfiguration() { } }
  internal class WorkflowStepAssigneeSummaryConfiguration : ResourceTypeBuilder<WorkflowStepAssigneeSummary> { public WorkflowStepAssigneeSummaryConfiguration() { } }
}

