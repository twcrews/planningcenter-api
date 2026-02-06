using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Provides a base class for making strongly-typed HTTP requests to a specified paginated resource endpoint.
/// </summary>
/// <typeparam name="TModel">The type of the model being requested.</typeparam>
/// <typeparam name="TResource">The type of the resource being requested.</typeparam>
/// <typeparam name="TResponse">The type of the response being returned.</typeparam>
/// <param name="client">The HTTP client to use for making requests.</param>
/// <param name="uri">The URI of the paginated resource endpoint.</param>
abstract class PaginatedResourceClient<TModel, TResource, TResponse>(HttpClient client, Uri uri) 
    : ResourceClient<TModel, TResource, TResponse>(client, uri) 
    where TResource : JsonApiResource 
    where TResponse : ResourceResponse<TResource>
{
    /// <summary>
    /// Sets the number of items to be returned per page in the paginated response.
    /// This sets the "per_page" query parameter.
    /// </summary>
    /// <param name="count">The number of items to be returned per page.</param>
    /// <returns>The current instance of the paginated resource client.</returns>
    public PaginatedResourceClient<TModel, TResource, TResponse> Take(int count)
    {
        SetQueryParameter("per_page", count.ToString());
        return this;
    }

    /// <summary>
    /// Sets the number of items to skip in the paginated response.
    /// This sets the "offset" query parameter.
    /// </summary>
    /// <param name="count">The number of items to skip.</param>
    /// <returns>The current instance of the paginated resource client.</returns>
    public PaginatedResourceClient<TModel, TResource, TResponse> Skip(int count)
    {
        SetQueryParameter("offset", count.ToString());
        return this;
    }
}
