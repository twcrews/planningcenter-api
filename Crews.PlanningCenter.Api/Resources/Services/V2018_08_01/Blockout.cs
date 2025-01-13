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
/// A fetchable Services Blockout resource.
/// </summary>
public class BlockoutResource
  : PlanningCenterSingletonFetchableResource<Blockout, BlockoutResource, ServicesDocumentContext>
{

  /// <summary>
  /// The related <see cref="BlockoutDateResourceCollection" />.
  /// </summary>
  public BlockoutDateResourceCollection BlockoutDates => GetRelated<BlockoutDateResourceCollection>("blockout_dates");

  /// <summary>
  /// The related <see cref="BlockoutExceptionResourceCollection" />.
  /// </summary>
  public BlockoutExceptionResourceCollection BlockoutExceptions => GetRelated<BlockoutExceptionResourceCollection>("blockout_exceptions");

  internal BlockoutResource(Uri uri, HttpClient client) : base(uri, client) { }
}

/// <summary>
/// A fetchable collection of Services Blockout resources.
/// </summary>
public class BlockoutResourceCollection
  : PlanningCenterPaginatedFetchableResource<Blockout, BlockoutResourceCollection, BlockoutResource, ServicesDocumentContext>,
  IQueryable<BlockoutResourceCollection, BlockoutQueryable>
{
  internal BlockoutResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public BlockoutResourceCollection Query(params KeyValuePair<BlockoutQueryable, string>[] queries)
    => base.Query(queries);
}

