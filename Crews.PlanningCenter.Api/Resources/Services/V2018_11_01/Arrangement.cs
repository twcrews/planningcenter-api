/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_11_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_11_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_11_01;

/// <summary>
/// A fetchable Services Arrangement resource.
/// </summary>
public class ArrangementResource
  : PlanningCenterSingletonFetchableResource<Arrangement, ArrangementResource, ServicesDocumentContext>,
  IIncludable<ArrangementResource, ArrangementIncludable>
{

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  /// <summary>
  /// The related <see cref="KeyResourceCollection" />.
  /// </summary>
  public KeyResourceCollection Keys => GetRelated<KeyResourceCollection>("keys");

  /// <summary>
  /// The related <see cref="ArrangementSectionsResourceCollection" />.
  /// </summary>
  public ArrangementSectionsResourceCollection Sections => GetRelated<ArrangementSectionsResourceCollection>("sections");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal ArrangementResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ArrangementResource Include(params ArrangementIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services Arrangement resources.
/// </summary>
public class ArrangementResourceCollection
  : PlanningCenterPaginatedFetchableResource<Arrangement, ArrangementResourceCollection, ArrangementResource, ServicesDocumentContext>,
  IIncludable<ArrangementResourceCollection, ArrangementIncludable>
{
  internal ArrangementResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ArrangementResourceCollection Include(params ArrangementIncludable[] included)
    => base.Include(included);
}

