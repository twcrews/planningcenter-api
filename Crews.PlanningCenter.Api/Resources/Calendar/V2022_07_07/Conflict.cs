/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Entities;
using Crews.PlanningCenter.Models.Calendar.V2022_07_07.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.Calendar.V2022_07_07;

/// <summary>
/// A fetchable Calendar Conflict resource.
/// </summary>
public class ConflictResource
  : PlanningCenterSingletonFetchableResource<Conflict, ConflictResource, CalendarDocumentContext>,
  IIncludable<ConflictResource, ConflictIncludable>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource ResolvedBy => GetRelated<PersonResource>("resolved_by");

  /// <summary>
  /// The related <see cref="ResourceResource" />.
  /// </summary>
  public ResourceResource Resource => GetRelated<ResourceResource>("resource");

  /// <summary>
  /// The related <see cref="EventResource" />.
  /// </summary>
  public EventResource Winner => GetRelated<EventResource>("winner");

  internal ConflictResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public ConflictResource Include(params ConflictIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of Calendar Conflict resources.
/// </summary>
public class ConflictResourceCollection
  : PlanningCenterPaginatedFetchableResource<Conflict, ConflictResourceCollection, ConflictResource, CalendarDocumentContext>,
  IIncludable<ConflictResourceCollection, ConflictIncludable>,
  IOrderable<ConflictResourceCollection, ConflictOrderable>
{
  internal ConflictResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public ConflictResourceCollection Include(params ConflictIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public ConflictResourceCollection OrderBy(ConflictOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);
}

