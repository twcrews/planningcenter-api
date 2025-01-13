/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2018_08_01;

/// <summary>
/// A fetchable Publishing ChannelDefaultEpisodeResource resource.
/// </summary>
public class ChannelDefaultEpisodeResourceResource
  : PlanningCenterSingletonFetchableResource<ChannelDefaultEpisodeResource, ChannelDefaultEpisodeResourceResource, PublishingDocumentContext>
{

  internal ChannelDefaultEpisodeResourceResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Publishing ChannelDefaultEpisodeResource resources.
/// </summary>
public class ChannelDefaultEpisodeResourceResourceCollection
  : PlanningCenterPaginatedFetchableResource<ChannelDefaultEpisodeResource, ChannelDefaultEpisodeResourceResourceCollection, ChannelDefaultEpisodeResourceResource, PublishingDocumentContext>
{
  internal ChannelDefaultEpisodeResourceResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

