/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using Crews.PlanningCenter.Models.Giving.V2019_10_18.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// A fetchable Giving Label resource.
/// </summary>
public class LabelResource
  : PlanningCenterSingletonFetchableResource<Label, LabelResource, GivingDocumentContext>
{

  internal LabelResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Giving Label resources.
/// </summary>
public class LabelResourceCollection
  : PlanningCenterPaginatedFetchableResource<Label, LabelResourceCollection, LabelResource, GivingDocumentContext>,
  IQueryable<LabelResourceCollection, LabelQueryable>
{
  internal LabelResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public LabelResourceCollection Query(params KeyValuePair<LabelQueryable, string>[] queries)
    => base.Query(queries);
}

