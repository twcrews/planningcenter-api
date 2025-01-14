/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Services.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Services.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.Services.V2018_08_01;

/// <summary>
/// A fetchable Services Key resource.
/// </summary>
public class KeyResource
  : PlanningCenterSingletonFetchableResource<Key, KeyResource, ServicesDocumentContext>
{

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  internal KeyResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Key>> PostAsync(Key resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Key>> PatchAsync(Key resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services Key resources.
/// </summary>
public class KeyResourceCollection
  : PlanningCenterPaginatedFetchableResource<Key, KeyResourceCollection, KeyResource, ServicesDocumentContext>,
  IOrderable<KeyResourceCollection, KeyOrderable>
{
  internal KeyResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public KeyResourceCollection OrderBy(KeyOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

