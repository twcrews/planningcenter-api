/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services PlanTemplate resource.
/// </summary>
public class PlanTemplateResource
  : PlanningCenterSingletonFetchableResource<PlanTemplate, PlanTemplateResource, ServicesDocumentContext>
{

  /// <summary>
  /// The related <see cref="ItemResourceCollection" />.
  /// </summary>
  public ItemResourceCollection Items => GetRelated<ItemResourceCollection>("items");

  /// <summary>
  /// The related <see cref="PlanNoteResourceCollection" />.
  /// </summary>
  public PlanNoteResourceCollection Notes => GetRelated<PlanNoteResourceCollection>("notes");

  /// <summary>
  /// The related <see cref="PlanPersonResourceCollection" />.
  /// </summary>
  public PlanPersonResourceCollection TeamMembers => GetRelated<PlanPersonResourceCollection>("team_members");

  internal PlanTemplateResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services PlanTemplate resources.
/// </summary>
public class PlanTemplateResourceCollection
  : PlanningCenterPaginatedFetchableResource<PlanTemplate, PlanTemplateResourceCollection, PlanTemplateResource, ServicesDocumentContext>,
  IOrderable<PlanTemplateResourceCollection, PlanTemplateOrderable>
{
  internal PlanTemplateResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PlanTemplateResourceCollection OrderBy(PlanTemplateOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

