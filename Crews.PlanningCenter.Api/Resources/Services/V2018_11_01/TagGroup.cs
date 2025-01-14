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
/// A fetchable Services TagGroup resource.
/// </summary>
public class TagGroupResource
  : PlanningCenterSingletonFetchableResource<TagGroup, TagGroupResource, ServicesDocumentContext>,
  IIncludable<TagGroupResource, TagGroupIncludable>
{

  /// <summary>
  /// The related <see cref="FolderResource" />.
  /// </summary>
  public FolderResource Folder => GetRelated<FolderResource>("folder");

  /// <summary>
  /// The related <see cref="TagResourceCollection" />.
  /// </summary>
  public TagResourceCollection Tags => GetRelated<TagResourceCollection>("tags");

  internal TagGroupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public TagGroupResource Include(params TagGroupIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services TagGroup resources.
/// </summary>
public class TagGroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<TagGroup, TagGroupResourceCollection, TagGroupResource, ServicesDocumentContext>,
  IIncludable<TagGroupResourceCollection, TagGroupIncludable>,
  IQueryable<TagGroupResourceCollection, TagGroupQueryable>
{
  internal TagGroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public TagGroupResourceCollection Include(params TagGroupIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public TagGroupResourceCollection Query(params (TagGroupQueryable, string)[] queries)
    => base.Query(queries);
}

