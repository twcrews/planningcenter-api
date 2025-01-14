/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2020_04_08.Entities;
using Crews.PlanningCenter.Models.Calendar.V2020_04_08.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2020_04_08;

/// <summary>
/// A fetchable Calendar Attachment resource.
/// </summary>
public class AttachmentResource
  : PlanningCenterSingletonFetchableResource<Attachment, AttachmentResource, CalendarDocumentContext>,
  IIncludable<AttachmentResource, AttachmentIncludable>
{

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Event => GetRelated<EventResource>("event");

  internal AttachmentResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public AttachmentResource Include(params AttachmentIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar Attachment resources.
/// </summary>
public class AttachmentResourceCollection
  : PlanningCenterPaginatedFetchableResource<Attachment, AttachmentResourceCollection, AttachmentResource, CalendarDocumentContext>,
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

