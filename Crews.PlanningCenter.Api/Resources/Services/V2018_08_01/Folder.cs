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
/// A fetchable Services Folder resource.
/// </summary>
public class FolderResource
  : PlanningCenterSingletonFetchableResource<Folder, FolderResource, ServicesDocumentContext>,
  IIncludable<FolderResource, FolderIncludable>
{

  /// <summary>
  /// The related <see cref="FolderResourceCollection" />.
  /// </summary>
  public FolderResourceCollection Folders => GetRelated<FolderResourceCollection>("folders");

  /// <summary>
  /// The related <see cref="ServiceTypeResourceCollection" />.
  /// </summary>
  public ServiceTypeResourceCollection ServiceTypes => GetRelated<ServiceTypeResourceCollection>("service_types");

  internal FolderResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public FolderResource Include(params FolderIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Services Folder resources.
/// </summary>
public class FolderResourceCollection
  : PlanningCenterPaginatedFetchableResource<Folder, FolderResourceCollection, FolderResource, ServicesDocumentContext>,
  IIncludable<FolderResourceCollection, FolderIncludable>,
  IOrderable<FolderResourceCollection, FolderOrderable>
{
  internal FolderResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FolderResourceCollection Include(params FolderIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public FolderResourceCollection OrderBy(FolderOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

