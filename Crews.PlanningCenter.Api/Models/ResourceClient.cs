using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
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
public abstract class ResourceClient<TModel, TResource, TResponse>(HttpClient httpClient, Uri uri) 
    where TResponse : ResourceResponse<TResource>
{
    /// <summary>
    /// The URI of the resource endpoint to which requests will be sent. 
    /// This property is initialized in the constructor and can be modified using the provided methods for managing query 
    /// parameters.
    /// </summary>
    protected Uri Uri { get; set; } = uri ?? throw new ArgumentNullException(nameof(uri));

    /// <summary>
    /// The HTTP client instance used to send requests to the resource endpoint.
    /// </summary>
    protected HttpClient HttpClient { get; } = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    /// <summary>
    /// Sets a query parameter and its associated value in the instance's URI.
    /// </summary>
    /// <param name="parameter">The name of the query parameter.</param>
    /// <param name="value">The value associated with the parameter.</param>
    protected ResourceClient<TModel, TResource, TResponse> SetQueryParameter(string parameter, string value)
    {
        UriBuilder builder = new(Uri);
        NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
        query[parameter] = value;
        builder.Query = query.ToString() ?? string.Empty;
        Uri = builder.Uri;
        return this;
    }

    /// <summary>
    /// Adds a custom query parameter and its associated value to the instance's URI.
    /// </summary>
    /// <param name="parameter">The name of the query parameter to be added.</param>
    /// <param name="value">The value of the query parameter to be added.</param>
    public void AddCustomParameter(string parameter, string value) =>
        SetQueryParameter(parameter, value);

    /// <summary>
    /// Removes the entire query string from the instance's URI.
    /// </summary>
    public void ClearParameters()
    {
        UriBuilder builder = new(Uri)
        {
            Query = string.Empty
        };
        Uri = builder.Uri;
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
    protected async Task<TResponse> GetAsync(CancellationToken cancellationToken = default)
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
    protected async Task<TResponse> PostAsync(
        TModel resource,
        CancellationToken cancellationToken = default)
    {
        StringContent content = new(JsonSerializer.Serialize(resource));
        HttpResponseMessage response = await HttpClient.PostAsync(Uri, content, cancellationToken);
        return await DeserializeResponseAsync(response, cancellationToken);
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
    protected async Task<TResponse> PatchAsync(
        TModel resource,
        CancellationToken cancellationToken = default)
    {
        StringContent content = new(JsonSerializer.Serialize(resource));
        HttpResponseMessage response = await HttpClient.PatchAsync(Uri, content, cancellationToken);
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

    /// <summary>
    /// Deserializes the HTTP response message into an instance of <typeparamref name="TResponse"/>.
    /// </summary>
    /// <param name="response">The HTTP response message to be deserialized.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the deserialized resource of type 
    /// <typeparamref name="TResponse"/>.
    /// </returns>
    protected abstract Task<TResponse> DeserializeResponseAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken);

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
