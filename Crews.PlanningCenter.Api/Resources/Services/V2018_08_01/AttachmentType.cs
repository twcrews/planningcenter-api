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
/// A fetchable Services AttachmentType resource.
/// </summary>
public class AttachmentTypeResource
  : PlanningCenterSingletonFetchableResource<AttachmentType, AttachmentTypeResource, ServicesDocumentContext>
{

  internal AttachmentTypeResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services AttachmentType resources.
/// </summary>
public class AttachmentTypeResourceCollection
  : PlanningCenterPaginatedFetchableResource<AttachmentType, AttachmentTypeResourceCollection, AttachmentTypeResource, ServicesDocumentContext>,
  IOrderable<AttachmentTypeResourceCollection, AttachmentTypeOrderable>
{
  internal AttachmentTypeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public AttachmentTypeResourceCollection OrderBy(AttachmentTypeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

