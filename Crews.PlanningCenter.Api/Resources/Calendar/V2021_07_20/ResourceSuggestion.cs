/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Entities;
using Crews.PlanningCenter.Models.Calendar.V2021_07_20.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2021_07_20;

/// <summary>
/// A fetchable Calendar ResourceSuggestion resource.
/// </summary>
public class ResourceSuggestionResource
  : PlanningCenterSingletonFetchableResource<ResourceSuggestion, ResourceSuggestionResource, CalendarDocumentContext>,
  IIncludable<ResourceSuggestionResource, ResourceSuggestionIncludable>
{

  /// <summary>
  /// The related <see cref="ResourceResource" />.
  /// </summary>
  public ResourceResource Resource => GetRelated<ResourceResource>("resource");

  internal ResourceSuggestionResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ResourceSuggestionResource Include(params ResourceSuggestionIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar ResourceSuggestion resources.
/// </summary>
public class ResourceSuggestionResourceCollection
  : PlanningCenterPaginatedFetchableResource<ResourceSuggestion, ResourceSuggestionResourceCollection, ResourceSuggestionResource, CalendarDocumentContext>,
  IIncludable<ResourceSuggestionResourceCollection, ResourceSuggestionIncludable>
{
  internal ResourceSuggestionResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ResourceSuggestionResourceCollection Include(params ResourceSuggestionIncludable[] included)
    => base.Include(included);
}

