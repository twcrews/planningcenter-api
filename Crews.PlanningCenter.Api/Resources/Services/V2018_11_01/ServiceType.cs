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
/// A fetchable Services ServiceType resource.
/// </summary>
public class ServiceTypeResource
  : PlanningCenterSingletonFetchableResource<ServiceType, ServiceTypeResource, ServicesDocumentContext>,
  IIncludable<ServiceTypeResource, ServiceTypeIncludable>
{

  /// <summary>
  /// The related <see cref="AttachmentResourceCollection" />.
  /// </summary>
  public AttachmentResourceCollection Attachments => GetRelated<AttachmentResourceCollection>("attachments");

  /// <summary>
  /// The related <see cref="ItemNoteCategoryResourceCollection" />.
  /// </summary>
  public ItemNoteCategoryResourceCollection ItemNoteCategories => GetRelated<ItemNoteCategoryResourceCollection>("item_note_categories");

  /// <summary>
  /// The related <see cref="LiveControllerResourceCollection" />.
  /// </summary>
  public LiveControllerResourceCollection LiveControllers => GetRelated<LiveControllerResourceCollection>("live_controllers");

  /// <summary>
  /// The related <see cref="PlanNoteCategoryResourceCollection" />.
  /// </summary>
  public PlanNoteCategoryResourceCollection PlanNoteCategories => GetRelated<PlanNoteCategoryResourceCollection>("plan_note_categories");

  /// <summary>
  /// The related <see cref="PlanTemplateResourceCollection" />.
  /// </summary>
  public PlanTemplateResourceCollection PlanTemplates => GetRelated<PlanTemplateResourceCollection>("plan_templates");

  /// <summary>
  /// The related <see cref="PlanTimeResourceCollection" />.
  /// </summary>
  public PlanTimeResourceCollection PlanTimes => GetRelated<PlanTimeResourceCollection>("plan_times");

  /// <summary>
  /// The related <see cref="PlanResourceCollection" />.
  /// </summary>
  public PlanResourceCollection Plans => GetRelated<PlanResourceCollection>("plans");

  /// <summary>
  /// The related <see cref="PublicViewResource" />.
  /// </summary>
  public PublicViewResource PublicView => GetRelated<PublicViewResource>("public_view");

  /// <summary>
  /// The related <see cref="TeamPositionResourceCollection" />.
  /// </summary>
  public TeamPositionResourceCollection TeamPositions => GetRelated<TeamPositionResourceCollection>("team_positions");

  /// <summary>
  /// The related <see cref="TeamResourceCollection" />.
  /// </summary>
  public TeamResourceCollection Teams => GetRelated<TeamResourceCollection>("teams");

  /// <summary>
  /// The related <see cref="TimePreferenceOptionResourceCollection" />.
  /// </summary>
  public TimePreferenceOptionResourceCollection TimePreferenceOptions => GetRelated<TimePreferenceOptionResourceCollection>("time_preference_options");

  /// <summary>
  /// The related <see cref="PlanResourceCollection" />.
  /// </summary>
  public PlanResourceCollection UnscopedPlans => GetRelated<PlanResourceCollection>("unscoped_plans");

  internal ServiceTypeResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ServiceTypeResource Include(params ServiceTypeIncludable[] included) 
    => base.Include(included);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ServiceType>> PostAsync(ServiceType resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<ServiceType>> PatchAsync(ServiceType resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of Services ServiceType resources.
/// </summary>
public class ServiceTypeResourceCollection
  : PlanningCenterPaginatedFetchableResource<ServiceType, ServiceTypeResourceCollection, ServiceTypeResource, ServicesDocumentContext>,
  IIncludable<ServiceTypeResourceCollection, ServiceTypeIncludable>,
  IOrderable<ServiceTypeResourceCollection, ServiceTypeOrderable>,
  IQueryable<ServiceTypeResourceCollection, ServiceTypeQueryable>
{
  internal ServiceTypeResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ServiceTypeResourceCollection Include(params ServiceTypeIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ServiceTypeResourceCollection OrderBy(ServiceTypeOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public ServiceTypeResourceCollection Query(params (ServiceTypeQueryable, string)[] queries)
    => base.Query(queries);
}

