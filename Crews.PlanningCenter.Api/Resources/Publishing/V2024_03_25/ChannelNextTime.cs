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
/// A fetchable Publishing ChannelNextTime resource.
/// </summary>
public class ChannelNextTimeResource
  : PlanningCenterSingletonFetchableResource<ChannelNextTime, ChannelNextTimeResource, PublishingDocumentContext>
{

  internal ChannelNextTimeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Publishing ChannelNextTime resources.
/// </summary>
public class ChannelNextTimeResourceCollection
  : PlanningCenterPaginatedFetchableResource<ChannelNextTime, ChannelNextTimeResourceCollection, ChannelNextTimeResource, PublishingDocumentContext>
{
  internal ChannelNextTimeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

