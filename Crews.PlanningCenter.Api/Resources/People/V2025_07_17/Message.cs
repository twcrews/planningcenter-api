/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_07_17.Entities;
using Crews.PlanningCenter.Models.People.V2025_07_17.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_07_17;

/// <summary>
/// A fetchable People Message resource.
/// </summary>
public class MessageResource
  : PlanningCenterSingletonFetchableResource<Message, MessageResource, PeopleDocumentContext>,
  IIncludable<MessageResource, MessageIncludable>
{

  /// <summary>
  /// The related <see cref="MessageGroupResource" />.
  /// </summary>
  public MessageGroupResource MessageGroup => GetRelated<MessageGroupResource>("message_group");

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource To => GetRelated<PersonResource>("to");

  internal MessageResource(Uri uri, HttpClient client) : base(uri, client) { }

	/// <inheritdoc />
	public MessageResource Include(params MessageIncludable[] included) 
    => base.Include(included);
}

/// <summary>
/// A fetchable collection of People Message resources.
/// </summary>
public class MessageResourceCollection
  : PlanningCenterPaginatedFetchableResource<Message, MessageResourceCollection, MessageResource, PeopleDocumentContext>,
  IIncludable<MessageResourceCollection, MessageIncludable>,
  IOrderable<MessageResourceCollection, MessageOrderable>,
  IQueryable<MessageResourceCollection, MessageQueryable>
{
  internal MessageResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public MessageResourceCollection Include(params MessageIncludable[] included)
    => base.Include(included);

  /// <inheritdoc />
  public MessageResourceCollection OrderBy(MessageOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public MessageResourceCollection Query(params (MessageQueryable, string)[] queries)
    => base.Query(queries);
}

