using System.Collections.Specialized;
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
/// The type representing the model contained in the <see cref="JsonApiResource.Attributes" /> property of a resource.
/// </typeparam>
/// <param name="httpClient">
/// The HTTP client instance used to send requests to the resource endpoint. Must not be null.
/// </param>
/// <param name="uri">The URI of the resource endpoint to which requests will be sent. Must not be null.</param>
public abstract class ResourceClient<TModel>(HttpClient httpClient, Uri uri)
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
    /// Appends a value to a query parameter in the instance's URI, preserving any existing values for that parameter.
    /// </summary>
    /// <param name="parameter">The name of the query parameter.</param>
    /// <param name="value">The value to append to the parameter.</param>
    protected ResourceClient<TModel> AddQueryParameter(string parameter, string value)
    {
        UriBuilder builder = new(Uri);
        NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
        query[parameter] = query[parameter] is null ? value : $"{query[parameter]},{value}";
        builder.Query = query.ToString() ?? string.Empty;
        Uri = builder.Uri;
        return this;
    }

    /// <summary>
    /// Sets a query parameter in the instance's URI, replacing any existing value for that parameter.
    /// </summary>
    /// <param name="parameter">The name of the query parameter.</param>
    /// <param name="value">The value to set for the parameter.</param>
    protected ResourceClient<TModel> ReplaceQueryParameter(string parameter, string value)
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
    public ResourceClient<TModel> AddCustomParameter(string parameter, string value) =>
        AddQueryParameter(parameter, value);

    /// <summary>
    /// Removes the entire query string from the instance's URI.
    /// </summary>
    public ResourceClient<TModel> ClearParameters()
    {
        UriBuilder builder = new(Uri)
        {
            Query = string.Empty
        };
        Uri = builder.Uri;
        return this;
    }
}
