using Crews.PlanningCenter.Api.Models;

namespace Crews.PlanningCenter.Api.Webhooks;

/// <summary>
/// Client for interacting with the Person resource.
/// </summary>
public class PersonClient(HttpClient httpClient, Uri uri) 
    : SingletonResourceClient<Person, PersonResource, PersonResponse>(httpClient, uri)
{    
    /// <summary>
    /// Fetches the <see cref="Person"/> resource asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation, containing the <see cref="Person"/> resource.</returns>
    /// <exception cref="JsonApiException">Thrown when the HTTP response indicates a failure status code.</exception>
    public new Task<PersonResponse> GetAsync(CancellationToken cancellationToken = default) 
        => base.GetAsync(cancellationToken);
            
    /// <summary>
    /// Adds a custom query parameter to the request URI.
    /// </summary>
    /// <param name="parameter">The name of the query parameter.</param>
    /// <param name="value">The value of the query parameter.</param>
    /// <returns>The current <see cref="PersonClient"/> instance.</returns>
    public new PersonClient AddCustomParameter(string parameter, string value) => (PersonClient)base.AddCustomParameter(parameter, value);
    
    /// <summary>
    /// Removes the entire query string from the request URI.
    /// </summary>
    /// <returns>The current <see cref="PersonClient"/> instance.</returns>
    public new PersonClient ClearParameters() => (PersonClient)base.ClearParameters();
}
