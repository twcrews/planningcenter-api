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
/// A fetchable Services AttachmentTypeGroup resource.
/// </summary>
public class AttachmentTypeGroupResource
  : PlanningCenterSingletonFetchableResource<AttachmentTypeGroup, AttachmentTypeGroupResource, ServicesDocumentContext>
{

  internal AttachmentTypeGroupResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services AttachmentTypeGroup resources.
/// </summary>
public class AttachmentTypeGroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<AttachmentTypeGroup, AttachmentTypeGroupResourceCollection, AttachmentTypeGroupResource, ServicesDocumentContext>,
  IOrderable<AttachmentTypeGroupResourceCollection, AttachmentTypeGroupOrderable>
{
  internal AttachmentTypeGroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public AttachmentTypeGroupResourceCollection OrderBy(AttachmentTypeGroupOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

