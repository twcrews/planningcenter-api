/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2019_10_10.Entities;
using Crews.PlanningCenter.Models.People.V2019_10_10.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2019_10_10;

/// <summary>
/// A fetchable People SocialProfile resource.
/// </summary>
public class SocialProfileResource
  : PlanningCenterSingletonFetchableResource<SocialProfile, SocialProfileResource, PeopleDocumentContext>,
  IIncludable<SocialProfileResource, SocialProfileIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal SocialProfileResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public SocialProfileResource Include(params SocialProfileIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People SocialProfile resources.
/// </summary>
public class SocialProfileResourceCollection
  : PlanningCenterPaginatedFetchableResource<SocialProfile, SocialProfileResourceCollection, SocialProfileResource, PeopleDocumentContext>,
  IIncludable<SocialProfileResourceCollection, SocialProfileIncludable>,
  IOrderable<SocialProfileResourceCollection, SocialProfileOrderable>,
  IQueryable<SocialProfileResourceCollection, SocialProfileQueryable>
{
  internal SocialProfileResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public SocialProfileResourceCollection Include(params SocialProfileIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public SocialProfileResourceCollection OrderBy(SocialProfileOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public SocialProfileResourceCollection Query(params KeyValuePair<SocialProfileQueryable, string>[] queries)
    => base.Query(queries);
}

