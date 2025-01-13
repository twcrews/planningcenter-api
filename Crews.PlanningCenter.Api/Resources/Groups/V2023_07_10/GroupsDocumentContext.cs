/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Groups.V2023_07_10.Entities;
using JsonApiFramework.ServiceModel.Configuration;

namespace Crews.PlanningCenter.Api.Resources.Groups.V2023_07_10;

/// <summary>
/// JSON API document context for the Planning Center Groups API.
/// </summary>
public class GroupsDocumentContext : PlanningCenterDocumentContext
{
  internal GroupsDocumentContext(JsonApiFramework.JsonApi.Document document) : base(document) { }
  internal GroupsDocumentContext() : base() { }

  /// <inheritdoc />
  protected override void OnServiceModelCreating(IServiceModelBuilder builder)
  {
    base.OnServiceModelCreating(builder);
  
    builder.Configurations.Add(new AttendanceConfiguration());
    builder.Configurations.Add(new EnrollmentConfiguration());
    builder.Configurations.Add(new EventConfiguration());
    builder.Configurations.Add(new EventNoteConfiguration());
    builder.Configurations.Add(new GroupConfiguration());
    builder.Configurations.Add(new GroupApplicationConfiguration());
    builder.Configurations.Add(new GroupTypeConfiguration());
    builder.Configurations.Add(new LocationConfiguration());
    builder.Configurations.Add(new MembershipConfiguration());
    builder.Configurations.Add(new OrganizationConfiguration());
    builder.Configurations.Add(new OwnerConfiguration());
    builder.Configurations.Add(new PersonConfiguration());
    builder.Configurations.Add(new ResourceConfiguration());
    builder.Configurations.Add(new TagConfiguration());
    builder.Configurations.Add(new TagGroupConfiguration());
  }

  internal class AttendanceConfiguration : ResourceTypeBuilder<Attendance> { public AttendanceConfiguration() { } }
  internal class EnrollmentConfiguration : ResourceTypeBuilder<Enrollment> { public EnrollmentConfiguration() { } }
  internal class EventConfiguration : ResourceTypeBuilder<Event> { public EventConfiguration() { } }
  internal class EventNoteConfiguration : ResourceTypeBuilder<EventNote> { public EventNoteConfiguration() { } }
  internal class GroupConfiguration : ResourceTypeBuilder<Group> { public GroupConfiguration() { } }
  internal class GroupApplicationConfiguration : ResourceTypeBuilder<GroupApplication> { public GroupApplicationConfiguration() { } }
  internal class GroupTypeConfiguration : ResourceTypeBuilder<GroupType> { public GroupTypeConfiguration() { } }
  internal class LocationConfiguration : ResourceTypeBuilder<Location> { public LocationConfiguration() { } }
  internal class MembershipConfiguration : ResourceTypeBuilder<Membership> { public MembershipConfiguration() { } }
  internal class OrganizationConfiguration : ResourceTypeBuilder<Organization> { public OrganizationConfiguration() { } }
  internal class OwnerConfiguration : ResourceTypeBuilder<Owner> { public OwnerConfiguration() { } }
  internal class PersonConfiguration : ResourceTypeBuilder<Person> { public PersonConfiguration() { } }
  internal class ResourceConfiguration : ResourceTypeBuilder<Resource> { public ResourceConfiguration() { } }
  internal class TagConfiguration : ResourceTypeBuilder<Tag> { public TagConfiguration() { } }
  internal class TagGroupConfiguration : ResourceTypeBuilder<TagGroup> { public TagGroupConfiguration() { } }
}

