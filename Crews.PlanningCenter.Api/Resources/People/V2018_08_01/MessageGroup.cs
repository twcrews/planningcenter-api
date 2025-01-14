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
/// A fetchable People MessageGroup resource.
/// </summary>
public class MessageGroupResource
  : PlanningCenterSingletonFetchableResource<MessageGroup, MessageGroupResource, PeopleDocumentContext>,
  IIncludable<MessageGroupResource, MessageGroupIncludable>
{

  /// <summary>
  /// The related <see cref="AppResource" />.
  /// </summary>
  public AppResource App => GetRelated<AppResource>("app");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource From => GetRelated<PersonResource>("from");

  /// <summary>
  /// The related <see cref="MessageResourceCollection" />.
  /// </summary>
  public MessageResourceCollection Messages => GetRelated<MessageResourceCollection>("messages");

  internal MessageGroupResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public MessageGroupResource Include(params MessageGroupIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People MessageGroup resources.
/// </summary>
public class MessageGroupResourceCollection
  : PlanningCenterPaginatedFetchableResource<MessageGroup, MessageGroupResourceCollection, MessageGroupResource, PeopleDocumentContext>,
  IIncludable<MessageGroupResourceCollection, MessageGroupIncludable>,
  IOrderable<MessageGroupResourceCollection, MessageGroupOrderable>,
  IQueryable<MessageGroupResourceCollection, MessageGroupQueryable>
{
  internal MessageGroupResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public MessageGroupResourceCollection Include(params MessageGroupIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public MessageGroupResourceCollection OrderBy(MessageGroupOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public MessageGroupResourceCollection Query(params (MessageGroupQueryable, string)[] queries)
    => base.Query(queries);
}

