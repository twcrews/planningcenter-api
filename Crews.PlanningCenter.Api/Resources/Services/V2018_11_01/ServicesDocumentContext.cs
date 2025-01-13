/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using JsonApiFramework.ServiceModel.Configuration;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// JSON API document context for the Planning Center Services API.
/// </summary>
public class ServicesDocumentContext : PlanningCenterDocumentContext
{
  internal ServicesDocumentContext(JsonApiFramework.JsonApi.Document document) : base(document) { }
  internal ServicesDocumentContext() : base() { }

  /// <inheritdoc />
  protected override void OnServiceModelCreating(IServiceModelBuilder builder)
  {
    base.OnServiceModelCreating(builder);
  
    builder.Configurations.Add(new ArrangementConfiguration());
    builder.Configurations.Add(new ArrangementSectionsConfiguration());
    builder.Configurations.Add(new AttachmentConfiguration());
    builder.Configurations.Add(new AttachmentActivityConfiguration());
    builder.Configurations.Add(new AttachmentTypeConfiguration());
    builder.Configurations.Add(new AttendanceConfiguration());
    builder.Configurations.Add(new AvailableSignupConfiguration());
    builder.Configurations.Add(new BlockoutConfiguration());
    builder.Configurations.Add(new BlockoutDateConfiguration());
    builder.Configurations.Add(new BlockoutExceptionConfiguration());
    builder.Configurations.Add(new BlockoutScheduleConflictConfiguration());
    builder.Configurations.Add(new ChatConfiguration());
    builder.Configurations.Add(new ContributorConfiguration());
    builder.Configurations.Add(new CustomSlideConfiguration());
    builder.Configurations.Add(new EmailConfiguration());
    builder.Configurations.Add(new EmailTemplateConfiguration());
    builder.Configurations.Add(new EmailTemplateRenderedResponseConfiguration());
    builder.Configurations.Add(new FolderConfiguration());
    builder.Configurations.Add(new FolderPathConfiguration());
    builder.Configurations.Add(new ItemConfiguration());
    builder.Configurations.Add(new ItemNoteConfiguration());
    builder.Configurations.Add(new ItemNoteCategoryConfiguration());
    builder.Configurations.Add(new ItemTimeConfiguration());
    builder.Configurations.Add(new KeyConfiguration());
    builder.Configurations.Add(new LayoutConfiguration());
    builder.Configurations.Add(new LiveConfiguration());
    builder.Configurations.Add(new LiveControllerConfiguration());
    builder.Configurations.Add(new MediaConfiguration());
    builder.Configurations.Add(new MediaScheduleConfiguration());
    builder.Configurations.Add(new NeededPositionConfiguration());
    builder.Configurations.Add(new OrganizationConfiguration());
    builder.Configurations.Add(new PersonConfiguration());
    builder.Configurations.Add(new PersonTeamPositionAssignmentConfiguration());
    builder.Configurations.Add(new PlanConfiguration());
    builder.Configurations.Add(new PlanNoteConfiguration());
    builder.Configurations.Add(new PlanNoteCategoryConfiguration());
    builder.Configurations.Add(new PlanPersonConfiguration());
    builder.Configurations.Add(new PlanPersonTimeConfiguration());
    builder.Configurations.Add(new PlanTemplateConfiguration());
    builder.Configurations.Add(new PlanTimeConfiguration());
    builder.Configurations.Add(new PublicViewConfiguration());
    builder.Configurations.Add(new ReportTemplateConfiguration());
    builder.Configurations.Add(new ScheduleConfiguration());
    builder.Configurations.Add(new ScheduledPersonConfiguration());
    builder.Configurations.Add(new SchedulingPreferenceConfiguration());
    builder.Configurations.Add(new SeriesConfiguration());
    builder.Configurations.Add(new ServiceTypeConfiguration());
    builder.Configurations.Add(new ServiceTypePathConfiguration());
    builder.Configurations.Add(new SignupSheetConfiguration());
    builder.Configurations.Add(new SignupSheetMetadataConfiguration());
    builder.Configurations.Add(new SkippedAttachmentConfiguration());
    builder.Configurations.Add(new SongConfiguration());
    builder.Configurations.Add(new SongScheduleConfiguration());
    builder.Configurations.Add(new SongbookStatusConfiguration());
    builder.Configurations.Add(new SplitTeamRehearsalAssignmentConfiguration());
    builder.Configurations.Add(new TagConfiguration());
    builder.Configurations.Add(new TagGroupConfiguration());
    builder.Configurations.Add(new TeamConfiguration());
    builder.Configurations.Add(new TeamLeaderConfiguration());
    builder.Configurations.Add(new TeamPositionConfiguration());
    builder.Configurations.Add(new TextSettingConfiguration());
    builder.Configurations.Add(new TimePreferenceOptionConfiguration());
    builder.Configurations.Add(new ZoomConfiguration());
  }

  internal class ArrangementConfiguration : ResourceTypeBuilder<Arrangement> { public ArrangementConfiguration() { } }
  internal class ArrangementSectionsConfiguration : ResourceTypeBuilder<ArrangementSections> { public ArrangementSectionsConfiguration() { } }
  internal class AttachmentConfiguration : ResourceTypeBuilder<Attachment> { public AttachmentConfiguration() { } }
  internal class AttachmentActivityConfiguration : ResourceTypeBuilder<AttachmentActivity> { public AttachmentActivityConfiguration() { } }
  internal class AttachmentTypeConfiguration : ResourceTypeBuilder<AttachmentType> { public AttachmentTypeConfiguration() { } }
  internal class AttendanceConfiguration : ResourceTypeBuilder<Attendance> { public AttendanceConfiguration() { } }
  internal class AvailableSignupConfiguration : ResourceTypeBuilder<AvailableSignup> { public AvailableSignupConfiguration() { } }
  internal class BlockoutConfiguration : ResourceTypeBuilder<Blockout> { public BlockoutConfiguration() { } }
  internal class BlockoutDateConfiguration : ResourceTypeBuilder<BlockoutDate> { public BlockoutDateConfiguration() { } }
  internal class BlockoutExceptionConfiguration : ResourceTypeBuilder<BlockoutException> { public BlockoutExceptionConfiguration() { } }
  internal class BlockoutScheduleConflictConfiguration : ResourceTypeBuilder<BlockoutScheduleConflict> { public BlockoutScheduleConflictConfiguration() { } }
  internal class ChatConfiguration : ResourceTypeBuilder<Chat> { public ChatConfiguration() { } }
  internal class ContributorConfiguration : ResourceTypeBuilder<Contributor> { public ContributorConfiguration() { } }
  internal class CustomSlideConfiguration : ResourceTypeBuilder<CustomSlide> { public CustomSlideConfiguration() { } }
  internal class EmailConfiguration : ResourceTypeBuilder<Email> { public EmailConfiguration() { } }
  internal class EmailTemplateConfiguration : ResourceTypeBuilder<EmailTemplate> { public EmailTemplateConfiguration() { } }
  internal class EmailTemplateRenderedResponseConfiguration : ResourceTypeBuilder<EmailTemplateRenderedResponse> { public EmailTemplateRenderedResponseConfiguration() { } }
  internal class FolderConfiguration : ResourceTypeBuilder<Folder> { public FolderConfiguration() { } }
  internal class FolderPathConfiguration : ResourceTypeBuilder<FolderPath> { public FolderPathConfiguration() { } }
  internal class ItemConfiguration : ResourceTypeBuilder<Item> { public ItemConfiguration() { } }
  internal class ItemNoteConfiguration : ResourceTypeBuilder<ItemNote> { public ItemNoteConfiguration() { } }
  internal class ItemNoteCategoryConfiguration : ResourceTypeBuilder<ItemNoteCategory> { public ItemNoteCategoryConfiguration() { } }
  internal class ItemTimeConfiguration : ResourceTypeBuilder<ItemTime> { public ItemTimeConfiguration() { } }
  internal class KeyConfiguration : ResourceTypeBuilder<Key> { public KeyConfiguration() { } }
  internal class LayoutConfiguration : ResourceTypeBuilder<Layout> { public LayoutConfiguration() { } }
  internal class LiveConfiguration : ResourceTypeBuilder<Live> { public LiveConfiguration() { } }
  internal class LiveControllerConfiguration : ResourceTypeBuilder<LiveController> { public LiveControllerConfiguration() { } }
  internal class MediaConfiguration : ResourceTypeBuilder<Media> { public MediaConfiguration() { } }
  internal class MediaScheduleConfiguration : ResourceTypeBuilder<MediaSchedule> { public MediaScheduleConfiguration() { } }
  internal class NeededPositionConfiguration : ResourceTypeBuilder<NeededPosition> { public NeededPositionConfiguration() { } }
  internal class OrganizationConfiguration : ResourceTypeBuilder<Organization> { public OrganizationConfiguration() { } }
  internal class PersonConfiguration : ResourceTypeBuilder<Person> { public PersonConfiguration() { } }
  internal class PersonTeamPositionAssignmentConfiguration : ResourceTypeBuilder<PersonTeamPositionAssignment> { public PersonTeamPositionAssignmentConfiguration() { } }
  internal class PlanConfiguration : ResourceTypeBuilder<Plan> { public PlanConfiguration() { } }
  internal class PlanNoteConfiguration : ResourceTypeBuilder<PlanNote> { public PlanNoteConfiguration() { } }
  internal class PlanNoteCategoryConfiguration : ResourceTypeBuilder<PlanNoteCategory> { public PlanNoteCategoryConfiguration() { } }
  internal class PlanPersonConfiguration : ResourceTypeBuilder<PlanPerson> { public PlanPersonConfiguration() { } }
  internal class PlanPersonTimeConfiguration : ResourceTypeBuilder<PlanPersonTime> { public PlanPersonTimeConfiguration() { } }
  internal class PlanTemplateConfiguration : ResourceTypeBuilder<PlanTemplate> { public PlanTemplateConfiguration() { } }
  internal class PlanTimeConfiguration : ResourceTypeBuilder<PlanTime> { public PlanTimeConfiguration() { } }
  internal class PublicViewConfiguration : ResourceTypeBuilder<PublicView> { public PublicViewConfiguration() { } }
  internal class ReportTemplateConfiguration : ResourceTypeBuilder<ReportTemplate> { public ReportTemplateConfiguration() { } }
  internal class ScheduleConfiguration : ResourceTypeBuilder<Schedule> { public ScheduleConfiguration() { } }
  internal class ScheduledPersonConfiguration : ResourceTypeBuilder<ScheduledPerson> { public ScheduledPersonConfiguration() { } }
  internal class SchedulingPreferenceConfiguration : ResourceTypeBuilder<SchedulingPreference> { public SchedulingPreferenceConfiguration() { } }
  internal class SeriesConfiguration : ResourceTypeBuilder<Series> { public SeriesConfiguration() { } }
  internal class ServiceTypeConfiguration : ResourceTypeBuilder<ServiceType> { public ServiceTypeConfiguration() { } }
  internal class ServiceTypePathConfiguration : ResourceTypeBuilder<ServiceTypePath> { public ServiceTypePathConfiguration() { } }
  internal class SignupSheetConfiguration : ResourceTypeBuilder<SignupSheet> { public SignupSheetConfiguration() { } }
  internal class SignupSheetMetadataConfiguration : ResourceTypeBuilder<SignupSheetMetadata> { public SignupSheetMetadataConfiguration() { } }
  internal class SkippedAttachmentConfiguration : ResourceTypeBuilder<SkippedAttachment> { public SkippedAttachmentConfiguration() { } }
  internal class SongConfiguration : ResourceTypeBuilder<Song> { public SongConfiguration() { } }
  internal class SongScheduleConfiguration : ResourceTypeBuilder<SongSchedule> { public SongScheduleConfiguration() { } }
  internal class SongbookStatusConfiguration : ResourceTypeBuilder<SongbookStatus> { public SongbookStatusConfiguration() { } }
  internal class SplitTeamRehearsalAssignmentConfiguration : ResourceTypeBuilder<SplitTeamRehearsalAssignment> { public SplitTeamRehearsalAssignmentConfiguration() { } }
  internal class TagConfiguration : ResourceTypeBuilder<Tag> { public TagConfiguration() { } }
  internal class TagGroupConfiguration : ResourceTypeBuilder<TagGroup> { public TagGroupConfiguration() { } }
  internal class TeamConfiguration : ResourceTypeBuilder<Team> { public TeamConfiguration() { } }
  internal class TeamLeaderConfiguration : ResourceTypeBuilder<TeamLeader> { public TeamLeaderConfiguration() { } }
  internal class TeamPositionConfiguration : ResourceTypeBuilder<TeamPosition> { public TeamPositionConfiguration() { } }
  internal class TextSettingConfiguration : ResourceTypeBuilder<TextSetting> { public TextSettingConfiguration() { } }
  internal class TimePreferenceOptionConfiguration : ResourceTypeBuilder<TimePreferenceOption> { public TimePreferenceOptionConfiguration() { } }
  internal class ZoomConfiguration : ResourceTypeBuilder<Zoom> { public ZoomConfiguration() { } }
}

