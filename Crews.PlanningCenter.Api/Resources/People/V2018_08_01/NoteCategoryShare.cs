/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2018_08_01.Entities;
using Crews.PlanningCenter.Models.People.V2018_08_01.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2018_08_01;

/// <summary>
/// A fetchable People NoteCategoryShare resource.
/// </summary>
public class NoteCategoryShareResource
  : PlanningCenterSingletonFetchableResource<NoteCategoryShare, NoteCategoryShareResource, PeopleDocumentContext>,
  IIncludable<NoteCategoryShareResource, NoteCategoryShareIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal NoteCategoryShareResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public NoteCategoryShareResource Include(params NoteCategoryShareIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People NoteCategoryShare resources.
/// </summary>
public class NoteCategoryShareResourceCollection
  : PlanningCenterPaginatedFetchableResource<NoteCategoryShare, NoteCategoryShareResourceCollection, NoteCategoryShareResource, PeopleDocumentContext>,
  IIncludable<NoteCategoryShareResourceCollection, NoteCategoryShareIncludable>,
  IQueryable<NoteCategoryShareResourceCollection, NoteCategoryShareQueryable>
{
  internal NoteCategoryShareResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public NoteCategoryShareResourceCollection Include(params NoteCategoryShareIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public NoteCategoryShareResourceCollection Query(params KeyValuePair<NoteCategoryShareQueryable, string>[] queries)
    => base.Query(queries);
}

