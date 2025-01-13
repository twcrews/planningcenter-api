/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2018_08_01;

/// <summary>
/// A fetchable Publishing Organization resource.
/// </summary>
public class OrganizationResource
  : PlanningCenterSingletonFetchableResource<Organization, OrganizationResource, PublishingDocumentContext>
{

  /// <summary>
  /// The related <see cref="ChannelResourceCollection" />.
  /// </summary>
  public ChannelResourceCollection Channels => GetRelated<ChannelResourceCollection>("channels");

  /// <summary>
  /// The related <see cref="EpisodeResourceCollection" />.
  /// </summary>
  public EpisodeResourceCollection Episodes => GetRelated<EpisodeResourceCollection>("episodes");

  /// <summary>
  /// The related <see cref="SeriesResourceCollection" />.
  /// </summary>
  public SeriesResourceCollection Series => GetRelated<SeriesResourceCollection>("series");

  /// <summary>
  /// The related <see cref="SpeakerResourceCollection" />.
  /// </summary>
  public SpeakerResourceCollection Speakers => GetRelated<SpeakerResourceCollection>("speakers");

  internal OrganizationResource(Uri uri, HttpClient client) : base(uri, client) { }
}


