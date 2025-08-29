/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.People.V2025_07_02.Entities;
using Crews.PlanningCenter.Models.People.V2025_07_02.Parameters;
using Crews.PlanningCenter.Api.Models.Resources.Querying;
using Crews.PlanningCenter.Api.Models.Resources;
using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Resources.People.V2025_07_02;

/// <summary>
/// A fetchable People Email resource.
/// </summary>
public class EmailResource
  : PlanningCenterSingletonFetchableResource<Email, EmailResource, PeopleDocumentContext>
{

  /// <summary>
  /// The related <see cref="PersonResource" />.
  /// </summary>
  public PersonResource Person => GetRelated<PersonResource>("person");

  internal EmailResource(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Email>> PostAsync(Email resource)
    => base.PostAsync(resource);

  /// <inheritdoc />
  public new Task<JsonApiSingletonResponse<Email>> PatchAsync(Email resource)
    => base.PatchAsync(resource);

  /// <inheritdoc />
  public new Task DeleteAsync() => base.DeleteAsync();
}

/// <summary>
/// A fetchable collection of People Email resources.
/// </summary>
public class EmailResourceCollection
  : PlanningCenterPaginatedFetchableResource<Email, EmailResourceCollection, EmailResource, PeopleDocumentContext>,
  IOrderable<EmailResourceCollection, EmailOrderable>,
  IQueryable<EmailResourceCollection, EmailQueryable>
{
  internal EmailResourceCollection(Uri uri, HttpClient client) : base(uri, client) { }

  /// <inheritdoc />
  public EmailResourceCollection OrderBy(EmailOrderable orderer, Order order = Order.Ascending)
    => base.OrderBy(orderer, order);

  /// <inheritdoc />
  public EmailResourceCollection Query(params (EmailQueryable, string)[] queries)
    => base.Query(queries);
}

