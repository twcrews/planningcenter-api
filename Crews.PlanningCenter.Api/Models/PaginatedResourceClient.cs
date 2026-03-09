using System.Net.Http.Json;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Provides a base class for making strongly-typed HTTP requests to a specified paginated resource endpoint.
/// </summary>
/// <typeparam name="TModel">The type of the model being requested.</typeparam>
/// <typeparam name="TResource">The type of the resource being requested.</typeparam>
/// <typeparam name="TResponse">The type of the response being returned.</typeparam>
/// <typeparam name="TSingletonResponse">The type of the singleton form of the response being returned.</typeparam>
/// <param name="client">The HTTP client to use for making requests.</param>
/// <param name="uri">The URI of the paginated resource endpoint.</param>
public abstract class PaginatedResourceClient<TModel, TResource, TResponse, TSingletonResponse>(HttpClient client, Uri uri)
    : ResourceClient<TModel>(client, uri)
    where TResource : JsonApiResource<TModel>, new()
    where TResponse : ResourceResponse<IEnumerable<TResource>>, new()
    where TSingletonResponse : ResourceResponse<TResource>, new()
{
    /// <summary>
    /// Sets the number of items to be returned per page in the paginated response.
    /// </summary>
    /// <param name="count">The number of items to be returned per page.</param>
    /// <returns>The current instance of the paginated resource client.</returns>
    public PaginatedResourceClient<TModel, TResource, TResponse, TSingletonResponse> PerPage(int count)
    {
        ReplaceQueryParameter("per_page", count.ToString());
        return this;
    }

    /// <summary>
    /// Sets the item offset in the paginated response.
    /// </summary>
    /// <param name="count">The number of items to skip.</param>
    /// <returns>The current instance of the paginated resource client.</returns>
    public PaginatedResourceClient<TModel, TResource, TResponse, TSingletonResponse> Offset(int count)
    {
        ReplaceQueryParameter("offset", count.ToString());
        return this;
    }

    /// <summary>
    /// Adds a filter query parameter to the request to filter the results based on the specified criteria.
    /// </summary>
    /// <remarks>
    /// See Planning Center API documentation for details on supported filter values for this resource.
    /// </remarks>
    /// <param name="filter">The filter criteria.</param>
    /// <returns>The current instance of the paginated resource client.</returns>
    public PaginatedResourceClient<TModel, TResource, TResponse, TSingletonResponse> Filter(string filter)
    {
        AddQueryParameter("filter", filter);
        return this;
    }

    /// <summary>
    /// Sends an asynchronous GET request to the specified resource endpoint and deserializes the response content.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="TResponse"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task<TResponse> GetAsync(CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await HttpClient.GetAsync(Uri, cancellationToken);
        return await DeserializeResponseAsync(response, cancellationToken);
    }

    /// <summary>
    /// Sends an asynchronous POST request with the specified resource data to the resource endpoint and deserializes
    /// the response content.
    /// </summary>
    /// <param name="resource">The resource data to be sent in the POST request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="TResponse"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected Task<TSingletonResponse> PostAsync(TModel resource, CancellationToken cancellationToken = default) 
        => PostAsync(new JsonApiDocument<TResource> { Data = new() { Attributes = resource } }, cancellationToken);

    /// <summary>
    /// Sends an asynchronous POST request with a custom JSON:API document to the resource endpoint and deserializes
    /// the response content.
    /// </summary>
    /// <param name="document">The JSON:API document to send in the request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="TResponse"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task<TSingletonResponse> PostAsync(
        JsonApiDocument<TResource> document, CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = new(HttpMethod.Post, Uri)
        {
            Content = JsonContent.Create(document)
        };
        HttpResponseMessage response = await HttpClient.SendAsync(request, cancellationToken);
        return await DeserializeSingletonResponseAsync(response, cancellationToken);
    }

    private static async Task<TSingletonResponse> DeserializeSingletonResponseAsync(
        HttpResponseMessage response, CancellationToken cancellationToken)
    {
        await EnsureSuccessAsync(response, cancellationToken);

        string json = await response.Content.ReadAsStringAsync();
        JsonApiDocument<TResource>? document = await response.ReadJsonApiDocumentAsync<TResource>(cancellationToken);
        if (document is null) return new() { ResponseMessage = response };
        if (document.Data is null) return new() { ResponseMessage = response, ResponseBody = document };

        return new() { Data = document.Data, ResponseMessage = response, ResponseBody = document };
    }

    private static async Task<TResponse> DeserializeResponseAsync(
        HttpResponseMessage response, CancellationToken cancellationToken)
    {
        await EnsureSuccessAsync(response, cancellationToken);

        JsonApiCollectionDocument<TResource>? document = await response
            .ReadJsonApiCollectionDocumentAsync<TResource>(cancellationToken);
        if (document is null) return new() { ResponseMessage = response };
        if (document.Data is null) return new() { ResponseMessage = response, ResponseBody = document };

        return new() { Data = document.Data, ResponseMessage = response, ResponseBody = document };
    }

    private static async Task EnsureSuccessAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        await EnsureSuccessAsync(response, content);
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response, string content)
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new JsonApiException(content, ex);
        }
    }
}
