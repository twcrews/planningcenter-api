/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Entities;
using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2022_07_07;

/// <summary>
/// A fetchable Calendar ResourceFolder resource.
/// </summary>
public class ResourceFolderResource
  : PlanningCenterSingletonFetchableResource<ResourceFolder, ResourceFolderResource, CalendarDocumentContext>,
  IIncludable<ResourceFolderResource, ResourceFolderIncludable>
{

  /// <summary>
  /// The related <see cref="ResourceResourceCollection" />.
  /// </summary>
  public ResourceResourceCollection Resources => GetRelated<ResourceResourceCollection>("resources");

  internal ResourceFolderResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ResourceFolderResource Include(params ResourceFolderIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ResourceFolder>> PostAsync(ResourceFolder resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ResourceFolder>> PatchAsync(ResourceFolder resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Calendar ResourceFolder resources.
/// </summary>
public class ResourceFolderResourceCollection
  : PlanningCenterPaginatedFetchableResource<ResourceFolder, ResourceFolderResourceCollection, ResourceFolderResource, CalendarDocumentContext>,
  IIncludable<ResourceFolderResourceCollection, ResourceFolderIncludable>,
  IOrderable<ResourceFolderResourceCollection, ResourceFolderOrderable>,
  IQueryable<ResourceFolderResourceCollection, ResourceFolderQueryable>
{
  internal ResourceFolderResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ResourceFolderResourceCollection Include(params ResourceFolderIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ResourceFolderResourceCollection OrderBy(ResourceFolderOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ResourceFolderResourceCollection Query(params (ResourceFolderQueryable, string)[] queries)
    => base.Query(queries);
}

