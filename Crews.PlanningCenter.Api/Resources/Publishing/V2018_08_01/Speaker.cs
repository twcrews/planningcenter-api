/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Publishing.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Publishing.V2018_08_01;

/// <summary>
/// A fetchable Publishing Speaker resource.
/// </summary>
public class SpeakerResource
  : PlanningCenterSingletonFetchableResource<Speaker, SpeakerResource, PublishingDocumentContext>
{

  internal SpeakerResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Publishing Speaker resources.
/// </summary>
public class SpeakerResourceCollection
  : PlanningCenterPaginatedFetchableResource<Speaker, SpeakerResourceCollection, SpeakerResource, PublishingDocumentContext>,
  IOrderable<SpeakerResourceCollection, SpeakerOrderable>
{
  internal SpeakerResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SpeakerResourceCollection OrderBy(SpeakerOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

