/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2024_03_25.Entities;
using JsonApiFramework.ServiceModel.Configuration;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2024_03_25;

/// <summary>
/// JSON API document context for the Planning Center Publishing API.
/// </summary>
public class PublishingDocumentContext : PlanningCenterDocumentContext
{
  internal PublishingDocumentContext(JsonApiFramework.JsonApi.Document document) : base(document) { }
  internal PublishingDocumentContext() : base() { }

  /// <inheritdoc />
  protected override void OnServiceModelCreating(IServiceModelBuilder builder)
  {
    base.OnServiceModelCreating(builder);
  
    builder.Configurations.Add(new ChannelConfiguration());
    builder.Configurations.Add(new ChannelDefaultEpisodeResourceConfiguration());
    builder.Configurations.Add(new ChannelDefaultTimeConfiguration());
    builder.Configurations.Add(new ChannelNextTimeConfiguration());
    builder.Configurations.Add(new EpisodeConfiguration());
    builder.Configurations.Add(new EpisodeResourceConfiguration());
    builder.Configurations.Add(new EpisodeStatisticsConfiguration());
    builder.Configurations.Add(new EpisodeTimeConfiguration());
    builder.Configurations.Add(new NoteTemplateConfiguration());
    builder.Configurations.Add(new OrganizationConfiguration());
    builder.Configurations.Add(new SeriesConfiguration());
    builder.Configurations.Add(new SpeakerConfiguration());
    builder.Configurations.Add(new SpeakershipConfiguration());
  }

  internal class ChannelConfiguration : ResourceTypeBuilder<Channel> { public ChannelConfiguration() { } }
  internal class ChannelDefaultEpisodeResourceConfiguration : ResourceTypeBuilder<ChannelDefaultEpisodeResource> { public ChannelDefaultEpisodeResourceConfiguration() { } }
  internal class ChannelDefaultTimeConfiguration : ResourceTypeBuilder<ChannelDefaultTime> { public ChannelDefaultTimeConfiguration() { } }
  internal class ChannelNextTimeConfiguration : ResourceTypeBuilder<ChannelNextTime> { public ChannelNextTimeConfiguration() { } }
  internal class EpisodeConfiguration : ResourceTypeBuilder<Episode> { public EpisodeConfiguration() { } }
  internal class EpisodeResourceConfiguration : ResourceTypeBuilder<EpisodeResource> { public EpisodeResourceConfiguration() { } }
  internal class EpisodeStatisticsConfiguration : ResourceTypeBuilder<EpisodeStatistics> { public EpisodeStatisticsConfiguration() { } }
  internal class EpisodeTimeConfiguration : ResourceTypeBuilder<EpisodeTime> { public EpisodeTimeConfiguration() { } }
  internal class NoteTemplateConfiguration : ResourceTypeBuilder<NoteTemplate> { public NoteTemplateConfiguration() { } }
  internal class OrganizationConfiguration : ResourceTypeBuilder<Organization> { public OrganizationConfiguration() { } }
  internal class SeriesConfiguration : ResourceTypeBuilder<Series> { public SeriesConfiguration() { } }
  internal class SpeakerConfiguration : ResourceTypeBuilder<Speaker> { public SpeakerConfiguration() { } }
  internal class SpeakershipConfiguration : ResourceTypeBuilder<Speakership> { public SpeakershipConfiguration() { } }
}

