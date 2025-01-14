/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.CheckIns.V2024_11_07.Entities;
using Crews.PlanningCenter.Models.CheckIns.V2024_11_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.CheckIns.V2024_11_07;

/// <summary>
/// A fetchable CheckIns Pass resource.
/// </summary>
public class PassResource
  : PlanningCenterSingletonFetchableResource<Pass, PassResource, CheckInsDocumentContext>,
  IIncludable<PassResource, PassIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal PassResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public PassResource Include(params PassIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of CheckIns Pass resources.
/// </summary>
public class PassResourceCollection
  : PlanningCenterPaginatedFetchableResource<Pass, PassResourceCollection, PassResource, CheckInsDocumentContext>,
  IIncludable<PassResourceCollection, PassIncludable>,
  IQueryable<PassResourceCollection, PassQueryable>
{
  internal PassResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public PassResourceCollection Include(params PassIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public PassResourceCollection Query(params (PassQueryable, string)[] queries)
    => base.Query(queries);
}

