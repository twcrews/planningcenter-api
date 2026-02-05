using System.Collections.Specialized;
using System.Text.Json;
using System.Web;

namespace Crews.PlanningCenter.Api.Models;

/// <summary>
/// Provides a base class for making strongly-typed HTTP requests to a specified resource endpoint.
/// </summary>
/// <remarks>This class is intended to be inherited by clients that interact with RESTful APIs, providing
/// protected methods for common HTTP operations such as GET, POST, PUT, and DELETE. It handles serialization and
/// deserialization of resource data and wraps HTTP errors in a custom exception for improved error handling.</remarks>
/// <typeparam name="T">The type representing the resource data to be sent or received from the endpoint.</typeparam>
/// <param name="httpClient">
/// The HTTP client instance used to send requests to the resource endpoint. Must not be null.
/// </param>
/// <param name="uri">The URI of the resource endpoint to which requests will be sent. Must not be null.</param>
public abstract class ResourceClient<T>(HttpClient httpClient, Uri uri)
{
    /// <summary>
    /// Removes the entire query string from the instance's URI.
    /// </summary>
    public void ClearParameters()
    {
        UriBuilder builder = new(uri)
        {
            Query = string.Empty
        };
        uri = builder.Uri;
    }

    /// <summary>
    /// Sends an asynchronous GET request to the specified resource endpoint and deserializes the response content.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="T"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task<T?> GetAsync(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await httpClient.GetAsync(uri, cancellationToken);
        return await DeserializeResponseAsync<T>(response, cancellationToken);
    }

    /// <summary>
    /// Sends an asynchronous POST request with the specified resource data to the resource endpoint and deserializes
    /// the response content.
    /// </summary>
    /// <typeparam name="TResponse">The type representing the resource data expected in the response.</typeparam>
    /// <param name="request">The resource data to be sent in the POST request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="TResponse"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task<TResponse?> PostAsync<TResponse>(
        T request,
        CancellationToken cancellationToken = default)
    {
        HttpContent content = CreateJsonContent(request);
        HttpResponseMessage response = await httpClient.PostAsync(uri, content, cancellationToken);
        return await DeserializeResponseAsync<TResponse>(response, cancellationToken);
    }

    /// <summary>
    /// Sends an asynchronous patch request with the specified resource data to the resource endpoint and deserializes
    /// the response content.
    /// </summary>
    /// <typeparam name="TResponse">The type representing the resource data expected in the response.</typeparam>
    /// <param name="request">The resource data to be sent in the patch request.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type
    /// <typeparamref name="TResponse"/> if deserialization is successful; otherwise, null.
    /// </returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task<TResponse?> PatchAsync<TResponse>(
        T request,
        CancellationToken cancellationToken = default)
    {
        HttpContent content = CreateJsonContent(request);
        HttpResponseMessage response = await httpClient.PatchAsync(uri, content, cancellationToken);
        return await DeserializeResponseAsync<TResponse>(response, cancellationToken);
    }

    /// <summary>
    /// Sends an asynchronous DELETE request to the specified resource endpoint.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    protected async Task DeleteAsync(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await httpClient.DeleteAsync(uri, cancellationToken);
        await EnsureSuccessAsync(response, cancellationToken);
    }

    /// <summary>
    /// Adds a parameter and its associated value to the end of the instance's URI query string.
    /// </summary>
    /// <param name="parameter">The name of the query parameter.</param>
    /// <param name="value">The value associated with the parameter.</param>
    protected void AddQueryParameter(string parameter, string value)
    {
        UriBuilder builder = new(uri);
        NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
        query[parameter] = value;
        builder.Query = query.ToString();
        uri = builder.Uri;
    }

    private static StringContent CreateJsonContent<TRequest>(TRequest request) =>
        new(JsonSerializer.Serialize(request));

    private static async Task<TResponse?> DeserializeResponseAsync<TResponse>(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        await EnsureSuccessAsync(response, content);
        return JsonSerializer.Deserialize<TResponse>(content);
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
