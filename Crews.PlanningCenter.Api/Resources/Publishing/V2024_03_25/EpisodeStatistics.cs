/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2024_03_25.Entities;
using Crews.PlanningCenter.Models.Publishing.V2024_03_25.Parameters;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2024_03_25;

/// <summary>
/// A fetchable Publishing EpisodeStatistics resource.
/// </summary>
public class EpisodeStatisticsResource
  : PlanningCenterSingletonFetchableResource<EpisodeStatistics, EpisodeStatisticsResource, PublishingDocumentContext>
{

  internal EpisodeStatisticsResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Publishing EpisodeStatistics resources.
/// </summary>
public class EpisodeStatisticsResourceCollection
  : PlanningCenterPaginatedFetchableResource<EpisodeStatistics, EpisodeStatisticsResourceCollection, EpisodeStatisticsResource, PublishingDocumentContext>
{
  internal EpisodeStatisticsResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

