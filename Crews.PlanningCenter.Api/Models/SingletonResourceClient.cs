using System.Net.Http.Json;
using Crews.Web.JsonApiClient;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Provides a base class for making strongly-typed HTTP requests to a specified resource endpoint.
/// </summary>
/// <remarks>This class is intended to be inherited by clients that interact with RESTful APIs, providing
/// protected methods for common HTTP operations such as GET, POST, PUT, and DELETE. It handles serialization and
/// deserialization of resource data and wraps HTTP errors in a custom exception for improved error handling.</remarks>
/// <typeparam name="TModel">
/// The type representing the model contained in the <see cref="JsonApiResource.Attributes" /> property of 
/// <typeparamref name="TResource"/>.
/// </typeparam>
/// <typeparam name="TResource">The type representing the JSON:API resource structure.</typeparam>
/// <typeparam name="TResponse">The type representing the Planning Center response data.</typeparam>
/// <param name="httpClient">
/// The HTTP client instance used to send requests to the resource endpoint. Must not be null.
/// </param>
/// <param name="uri">The URI of the resource endpoint to which requests will be sent. Must not be null.</param>
public abstract class SingletonResourceClient<TModel, TResource, TResponse>(HttpClient httpClient, Uri uri) 
    : ResourceClient<TModel>(httpClient, uri)
    where TResource : JsonApiResource<TModel>, new()
    where TResponse : ResourceResponse<TResource>, new()
{
    /// <summary>
    /// Sends an asynchronous GET request to the specified resource endpoint and deserializes the response content.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="TResponse"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task<TResponse> GetAsync(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await HttpClient.GetAsync(Uri, cancellationToken);
        return await SingletonResourceClient<TModel, TResource, TResponse>.DeserializeResponseAsync(response, cancellationToken);
    }

    /// <summary>
    /// Sends an asynchronous POST request to the specified URI without a request body, typically used for triggering
    /// actions or operations on the server that do not require input data.
    /// </summary>
    /// <param name="uri">The URI to which the POST request will be sent.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected async Task PostAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = new(HttpMethod.Post, uri);
        HttpResponseMessage response = HttpClient.Send(request, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);
    }

    /// <summary>
    /// Sends an asynchronous patch request with the specified resource data to the resource endpoint and deserializes
    /// the response content.
    /// </summary>
    /// <param name="resource">The resource data to be sent in the patch request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="TResponse"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected Task<TResponse> PatchAsync(TModel resource, CancellationToken cancellationToken = default) 
        => PatchAsync(new JsonApiDocument<TResource> { Data = new() { Attributes = resource } }, cancellationToken);

    /// <summary>
    /// Sends an asynchronous patch request with the specified JSON:API document to the resource endpoint and 
    /// deserializes the response content.
    /// </summary>
    /// <param name="document">The JSON:API document to be sent in the patch request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="TResponse"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task<TResponse> PatchAsync(
        JsonApiDocument<TResource> document, CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = new(HttpMethod.Patch, Uri)
        {
            Content = JsonContent.Create(document)
        };
        HttpResponseMessage response = await HttpClient.SendAsync(request, cancellationToken);
        return await DeserializeResponseAsync(response, cancellationToken);
    }

    /// <summary>
    /// Sends an asynchronous DELETE request to the specified resource endpoint.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task DeleteAsync(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await HttpClient.DeleteAsync(Uri, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);
    }

    private static async Task<TResponse> DeserializeResponseAsync(
        HttpResponseMessage response, CancellationToken cancellationToken)
    {
        await EnsureSuccessAsync(response, cancellationToken);

        JsonApiDocument<TResource>? document = await response.ReadJsonApiDocumentAsync<TResource>(cancellationToken);
        if (document is null) return new() { ResponseMessage = response };
        if (document.Data is null) return new() { ResponseMessage = response, ResponseBody = document };

        return new() { Data = document.Data, ResponseMessage = response, ResponseBody = document };
    }

}
