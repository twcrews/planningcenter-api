/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.Giving.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2018_08_01;

/// <summary>
/// A fetchable Giving Fund resource.
/// </summary>
public class FundResource
  : PlanningCenterSingletonFetchableResource<Fund, FundResource, GivingDocumentContext>
{

  internal FundResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Giving Fund resources.
/// </summary>
public class FundResourceCollection
  : PlanningCenterPaginatedFetchableResource<Fund, FundResourceCollection, FundResource, GivingDocumentContext>,
  IQueryable<FundResourceCollection, FundQueryable>
{
  internal FundResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public FundResourceCollection Query(params KeyValuePair<FundQueryable, string>[] queries)
    => base.Query(queries);
}

