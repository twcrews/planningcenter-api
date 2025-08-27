/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.Extensions.Http;
using Crews.PlanningCenter.Api.Extensions;

namespace Crews.PlanningCenter.Api.Clients;

/// <summary>
/// Service for interacting with Planning Center CheckIns APIs.
/// </summary>
/// <param name="client">The <see cref="HttpClient"/> used to send requests.</param>
/// <param name="baseUri">
/// An optional base <see cref="Uri"/> to use for the API address. If omitted,
/// <see cref="HttpClient.BaseAddress"/> is used instead. If this is also not set, an exception is thrown. 
/// </param>
public class CheckInsClient(HttpClient client, Uri? baseUri = null)
{
  private readonly HttpClient _client = client;
  private readonly Uri _baseUri = baseUri ?? client.BaseAddress ?? throw new InvalidOperationException(
    "A base URI for the API was not set in either the constructor or the `HttpClient.BaseAddress` property.");
  /// <summary>
  /// Gets a client for the latest version of the CheckIns API.
  /// </summary>
  public Resources.CheckIns.V2025_05_28.OrganizationResource LatestVersion => V2025_05_28;

  /// <summary>
  /// Gets a client for version 2025-05-28 of the CheckIns API.
  /// </summary>
  public Resources.CheckIns.V2025_05_28.OrganizationResource V2025_05_28
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2025-05-28");
      return new(_baseUri.SafelyAppendPath("check-ins/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2024-11-07 of the CheckIns API.
  /// </summary>
  public Resources.CheckIns.V2024_11_07.OrganizationResource V2024_11_07
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2024-11-07");
      return new(_baseUri.SafelyAppendPath("check-ins/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2024-09-03 of the CheckIns API.
  /// </summary>
  public Resources.CheckIns.V2024_09_03.OrganizationResource V2024_09_03
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2024-09-03");
      return new(_baseUri.SafelyAppendPath("check-ins/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2023-04-05 of the CheckIns API.
  /// </summary>
  public Resources.CheckIns.V2023_04_05.OrganizationResource V2023_04_05
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2023-04-05");
      return new(_baseUri.SafelyAppendPath("check-ins/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2019-07-17 of the CheckIns API.
  /// </summary>
  public Resources.CheckIns.V2019_07_17.OrganizationResource V2019_07_17
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2019-07-17");
      return new(_baseUri.SafelyAppendPath("check-ins/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2018-08-01 of the CheckIns API.
  /// </summary>
  public Resources.CheckIns.V2018_08_01.OrganizationResource V2018_08_01
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2018-08-01");
      return new(_baseUri.SafelyAppendPath("check-ins/v2"), _client);
    }
  }
}

