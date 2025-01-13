/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2024_11_07.Entities;
using JsonApiFramework.ServiceModel.Configuration;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2024_11_07;

/// <summary>
/// JSON API document context for the Planning Center CheckIns API.
/// </summary>
public class CheckInsDocumentContext : PlanningCenterDocumentContext
{
  internal CheckInsDocumentContext(JsonApiFramework.JsonApi.Document document) : base(document) { }
  internal CheckInsDocumentContext() : base() { }

  /// <inheritdoc />
  protected override void OnServiceModelCreating(IServiceModelBuilder builder)
  {
    base.OnServiceModelCreating(builder);
  
    builder.Configurations.Add(new AttendanceTypeConfiguration());
    builder.Configurations.Add(new CheckInConfiguration());
    builder.Configurations.Add(new CheckInGroupConfiguration());
    builder.Configurations.Add(new CheckInTimeConfiguration());
    builder.Configurations.Add(new EventConfiguration());
    builder.Configurations.Add(new EventLabelConfiguration());
    builder.Configurations.Add(new EventPeriodConfiguration());
    builder.Configurations.Add(new EventTimeConfiguration());
    builder.Configurations.Add(new HeadcountConfiguration());
    builder.Configurations.Add(new IntegrationLinkConfiguration());
    builder.Configurations.Add(new LabelConfiguration());
    builder.Configurations.Add(new LocationConfiguration());
    builder.Configurations.Add(new LocationEventPeriodConfiguration());
    builder.Configurations.Add(new LocationEventTimeConfiguration());
    builder.Configurations.Add(new LocationLabelConfiguration());
    builder.Configurations.Add(new OptionConfiguration());
    builder.Configurations.Add(new OrganizationConfiguration());
    builder.Configurations.Add(new PassConfiguration());
    builder.Configurations.Add(new PersonConfiguration());
    builder.Configurations.Add(new PersonEventConfiguration());
    builder.Configurations.Add(new PreCheckConfiguration());
    builder.Configurations.Add(new RosterListPersonConfiguration());
    builder.Configurations.Add(new StationConfiguration());
    builder.Configurations.Add(new ThemeConfiguration());
  }

  internal class AttendanceTypeConfiguration : ResourceTypeBuilder<AttendanceType> { public AttendanceTypeConfiguration() { } }
  internal class CheckInConfiguration : ResourceTypeBuilder<CheckIn> { public CheckInConfiguration() { } }
  internal class CheckInGroupConfiguration : ResourceTypeBuilder<CheckInGroup> { public CheckInGroupConfiguration() { } }
  internal class CheckInTimeConfiguration : ResourceTypeBuilder<CheckInTime> { public CheckInTimeConfiguration() { } }
  internal class EventConfiguration : ResourceTypeBuilder<Event> { public EventConfiguration() { } }
  internal class EventLabelConfiguration : ResourceTypeBuilder<EventLabel> { public EventLabelConfiguration() { } }
  internal class EventPeriodConfiguration : ResourceTypeBuilder<EventPeriod> { public EventPeriodConfiguration() { } }
  internal class EventTimeConfiguration : ResourceTypeBuilder<EventTime> { public EventTimeConfiguration() { } }
  internal class HeadcountConfiguration : ResourceTypeBuilder<Headcount> { public HeadcountConfiguration() { } }
  internal class IntegrationLinkConfiguration : ResourceTypeBuilder<IntegrationLink> { public IntegrationLinkConfiguration() { } }
  internal class LabelConfiguration : ResourceTypeBuilder<Label> { public LabelConfiguration() { } }
  internal class LocationConfiguration : ResourceTypeBuilder<Location> { public LocationConfiguration() { } }
  internal class LocationEventPeriodConfiguration : ResourceTypeBuilder<LocationEventPeriod> { public LocationEventPeriodConfiguration() { } }
  internal class LocationEventTimeConfiguration : ResourceTypeBuilder<LocationEventTime> { public LocationEventTimeConfiguration() { } }
  internal class LocationLabelConfiguration : ResourceTypeBuilder<LocationLabel> { public LocationLabelConfiguration() { } }
  internal class OptionConfiguration : ResourceTypeBuilder<Option> { public OptionConfiguration() { } }
  internal class OrganizationConfiguration : ResourceTypeBuilder<Organization> { public OrganizationConfiguration() { } }
  internal class PassConfiguration : ResourceTypeBuilder<Pass> { public PassConfiguration() { } }
  internal class PersonConfiguration : ResourceTypeBuilder<Person> { public PersonConfiguration() { } }
  internal class PersonEventConfiguration : ResourceTypeBuilder<PersonEvent> { public PersonEventConfiguration() { } }
  internal class PreCheckConfiguration : ResourceTypeBuilder<PreCheck> { public PreCheckConfiguration() { } }
  internal class RosterListPersonConfiguration : ResourceTypeBuilder<RosterListPerson> { public RosterListPersonConfiguration() { } }
  internal class StationConfiguration : ResourceTypeBuilder<Station> { public StationConfiguration() { } }
  internal class ThemeConfiguration : ResourceTypeBuilder<Theme> { public ThemeConfiguration() { } }
}

