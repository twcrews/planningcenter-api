/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services Attachment resource.
/// </summary>
public class AttachmentResource
  : PlanningCenterSingletonFetchableResource<Attachment, AttachmentResource, ServicesDocumentContext>,
  IIncludable<AttachmentResource, AttachmentIncludable>
{

  /// <summary>
  /// The related <see cref="ZoomResourceCollection" />.
  /// </summary>
  public ZoomResourceCollection Zooms => GetRelated<ZoomResourceCollection>("zooms");

  internal AttachmentResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public AttachmentResource Include(params AttachmentIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Attachment>> PostAsync(Attachment resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Attachment>> PatchAsync(Attachment resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services Attachment resources.
/// </summary>
public class AttachmentResourceCollection
  : PlanningCenterPaginatedFetchableResource<Attachment, AttachmentResourceCollection, AttachmentResource, ServicesDocumentContext>,
  IIncludable<AttachmentResourceCollection, AttachmentIncludable>,
  IOrderable<AttachmentResourceCollection, AttachmentOrderable>,
  IQueryable<AttachmentResourceCollection, AttachmentQueryable>
{
  internal AttachmentResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public AttachmentResourceCollection Include(params AttachmentIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public AttachmentResourceCollection OrderBy(AttachmentOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public AttachmentResourceCollection Query(params (AttachmentQueryable, string)[] queries)
    => base.Query(queries);
}

