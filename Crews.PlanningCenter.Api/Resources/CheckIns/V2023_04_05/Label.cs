/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2023_04_05.Entities;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2023_04_05;

/// <summary>
/// A fetchable CheckIns Label resource.
/// </summary>
public class LabelResource
  : PlanningCenterSingletonFetchableResource<Label, LabelResource, CheckInsDocumentContext>
{

  /// <summary>
  /// The related <see cref="EventLabelResourceCollection" />.
  /// </summary>
  public EventLabelResourceCollection EventLabels => GetRelated<EventLabelResourceCollection>("event_labels");

  /// <summary>
  /// The related <see cref="LocationLabelResourceCollection" />.
  /// </summary>
  public LocationLabelResourceCollection LocationLabels => GetRelated<LocationLabelResourceCollection>("location_labels");

  internal LabelResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of CheckIns Label resources.
/// </summary>
public class LabelResourceCollection
  : PlanningCenterPaginatedFetchableResource<Label, LabelResourceCollection, LabelResource, CheckInsDocumentContext>
{
  internal LabelResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }
}

