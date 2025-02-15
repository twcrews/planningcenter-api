/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.Extensions.Http;
using Crews.PlanningCenter.Api.Extensions;

namespace Crews.PlanningCenter.Api.Clients;

/// <summary>
/// Service for interacting with Planning Center Publishing APIs.
/// </summary>
/// <param name="client">The <see cref="HttpClient"/> used to send requests.</param>
/// <param name="baseUri">
/// An optional base <see cref="Uri"/> to use for the API address. If omitted,
/// <see cref="HttpClient.BaseAddress"/> is used instead. If this is also not set, an exception is thrown. 
/// </param>
public class PublishingClient(HttpClient client, Uri? baseUri = null)
{
  private readonly HttpClient _client = client;
  private readonly Uri _baseUri = baseUri ?? client.BaseAddress ?? throw new InvalidOperationException(
    "A base URI for the API was not set in either the constructor or the `HttpClient.BaseAddress` property.");
  /// <summary>
  /// Gets a client for the latest version of the Publishing API.
  /// </summary>
  public Resources.Publishing.V2024_03_25.OrganizationResource LatestVersion => V2024_03_25;

  /// <summary>
  /// Gets a client for version 2024-03-25 of the Publishing API.
  /// </summary>
  public Resources.Publishing.V2024_03_25.OrganizationResource V2024_03_25
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2024-03-25");
      return new(_baseUri.SafelyAppendPath("publishing/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2018-08-01 of the Publishing API.
  /// </summary>
  public Resources.Publishing.V2018_08_01.OrganizationResource V2018_08_01
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2018-08-01");
      return new(_baseUri.SafelyAppendPath("publishing/v2"), _client);
    }
  }
}

