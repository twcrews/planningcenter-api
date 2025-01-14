/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Entities;
using JsonApiFramework.ServiceModel.Configuration;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2022_07_07;

/// <summary>
/// JSON API document context for the Planning Center Calendar API.
/// </summary>
public class CalendarDocumentContext : PlanningCenterDocumentContext
{
  internal CalendarDocumentContext(JsonApiFramework.JsonApi.Document document) : base(document) { }
  internal CalendarDocumentContext() : base() { }

  /// <inheritdoc />
  protected override void OnServiceModelCreating(IServiceModelBuilder builder)
  {
    base.OnServiceModelCreating(builder);
  
    builder.Configurations.Add(new AttachmentConfiguration());
    builder.Configurations.Add(new ConflictConfiguration());
    builder.Configurations.Add(new EventConfiguration());
    builder.Configurations.Add(new EventConnectionConfiguration());
    builder.Configurations.Add(new EventInstanceConfiguration());
    builder.Configurations.Add(new EventResourceRequestConfiguration());
    builder.Configurations.Add(new EventTimeConfiguration());
    builder.Configurations.Add(new FeedConfiguration());
    builder.Configurations.Add(new JobStatusConfiguration());
    builder.Configurations.Add(new OrganizationConfiguration());
    builder.Configurations.Add(new PersonConfiguration());
    builder.Configurations.Add(new ReportTemplateConfiguration());
    builder.Configurations.Add(new RequiredApprovalConfiguration());
    builder.Configurations.Add(new ResourceConfiguration());
    builder.Configurations.Add(new ResourceApprovalGroupConfiguration());
    builder.Configurations.Add(new ResourceBookingConfiguration());
    builder.Configurations.Add(new ResourceFolderConfiguration());
    builder.Configurations.Add(new ResourceQuestionConfiguration());
    builder.Configurations.Add(new ResourceSuggestionConfiguration());
    builder.Configurations.Add(new RoomSetupConfiguration());
    builder.Configurations.Add(new TagConfiguration());
    builder.Configurations.Add(new TagGroupConfiguration());
  }

  internal class AttachmentConfiguration : ResourceTypeBuilder<Attachment> { public AttachmentConfiguration() { } }
  internal class ConflictConfiguration : ResourceTypeBuilder<Conflict> { public ConflictConfiguration() { } }
  internal class EventConfiguration : ResourceTypeBuilder<Event> { public EventConfiguration() { } }
  internal class EventConnectionConfiguration : ResourceTypeBuilder<EventConnection> { public EventConnectionConfiguration() { } }
  internal class EventInstanceConfiguration : ResourceTypeBuilder<EventInstance> { public EventInstanceConfiguration() { } }
  internal class EventResourceRequestConfiguration : ResourceTypeBuilder<EventResourceRequest> { public EventResourceRequestConfiguration() { } }
  internal class EventTimeConfiguration : ResourceTypeBuilder<EventTime> { public EventTimeConfiguration() { } }
  internal class FeedConfiguration : ResourceTypeBuilder<Feed> { public FeedConfiguration() { } }
  internal class JobStatusConfiguration : ResourceTypeBuilder<JobStatus> { public JobStatusConfiguration() { } }
  internal class OrganizationConfiguration : ResourceTypeBuilder<Organization> { public OrganizationConfiguration() { } }
  internal class PersonConfiguration : ResourceTypeBuilder<Person> { public PersonConfiguration() { } }
  internal class ReportTemplateConfiguration : ResourceTypeBuilder<ReportTemplate> { public ReportTemplateConfiguration() { } }
  internal class RequiredApprovalConfiguration : ResourceTypeBuilder<RequiredApproval> { public RequiredApprovalConfiguration() { } }
  internal class ResourceConfiguration : ResourceTypeBuilder<Resource> { public ResourceConfiguration() { } }
  internal class ResourceApprovalGroupConfiguration : ResourceTypeBuilder<ResourceApprovalGroup> { public ResourceApprovalGroupConfiguration() { } }
  internal class ResourceBookingConfiguration : ResourceTypeBuilder<ResourceBooking> { public ResourceBookingConfiguration() { } }
  internal class ResourceFolderConfiguration : ResourceTypeBuilder<ResourceFolder> { public ResourceFolderConfiguration() { } }
  internal class ResourceQuestionConfiguration : ResourceTypeBuilder<ResourceQuestion> { public ResourceQuestionConfiguration() { } }
  internal class ResourceSuggestionConfiguration : ResourceTypeBuilder<ResourceSuggestion> { public ResourceSuggestionConfiguration() { } }
  internal class RoomSetupConfiguration : ResourceTypeBuilder<RoomSetup> { public RoomSetupConfiguration() { } }
  internal class TagConfiguration : ResourceTypeBuilder<Tag> { public TagConfiguration() { } }
  internal class TagGroupConfiguration : ResourceTypeBuilder<TagGroup> { public TagGroupConfiguration() { } }
}

