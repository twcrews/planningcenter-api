/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2023_04_05.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2023_04_05.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2023_04_05;

/// <summary>
/// A fetchable CheckIns EventLabel resource.
/// </summary>
public class EventLabelResource
  : PlanningCenterSingletonFetchableResource<EventLabel, EventLabelResource, CheckInsDocumentContext>,
  IIncludable<EventLabelResource, EventLabelIncludable>
{

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  /// <summary>
  /// The related <see cref="LabelResource" />.
  /// </summary>
  public LabelResource Label => GetRelated<LabelResource>("label");

  internal EventLabelResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public EventLabelResource Include(params EventLabelIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns EventLabel resources.
/// </summary>
public class EventLabelResourceCollection
  : PlanningCenterPaginatedFetchableResource<EventLabel, EventLabelResourceCollection, EventLabelResource, CheckInsDocumentContext>,
  IIncludable<EventLabelResourceCollection, EventLabelIncludable>
{
  internal EventLabelResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EventLabelResourceCollection Include(params EventLabelIncludable[] included)
    => base.Include(included);
}

