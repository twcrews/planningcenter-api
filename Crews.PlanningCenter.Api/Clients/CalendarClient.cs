/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.Extensions.Http;
using Crews.PlanningCenter.Api.Extensions;

namespace Crews.PlanningCenter.Api.Clients;

/// <summary>
/// Service for interacting with Planning Center Calendar APIs.
/// </summary>
/// <param name="client">The <see cref="HttpClient"/> used to send requests.</param>
/// <param name="baseUri">
/// An optional base <see cref="Uri"/> to use for the API address. If omitted,
/// <see cref="HttpClient.BaseAddress"/> is used instead. If this is also not set, an exception is thrown. 
/// </param>
public class CalendarClient(HttpClient client, Uri? baseUri = null)
{
  private readonly HttpClient _client = client;
  private readonly Uri _baseUri = baseUri ?? client.BaseAddress ?? throw new InvalidOperationException(
    "A base URI for the API was not set in either the constructor or the `HttpClient.BaseAddress` property.");
  /// <summary>
  /// Gets a client for the latest version of the Calendar API.
  /// </summary>
  public Resources.Calendar.V2022_07_07.OrganizationResource LatestVersion => V2022_07_07;

  /// <summary>
  /// Gets a client for version 2022-07-07 of the Calendar API.
  /// </summary>
  public Resources.Calendar.V2022_07_07.OrganizationResource V2022_07_07
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2022-07-07");
      return new(_baseUri.SafelyAppendPath("calendar/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2021-07-20 of the Calendar API.
  /// </summary>
  public Resources.Calendar.V2021_07_20.OrganizationResource V2021_07_20
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2021-07-20");
      return new(_baseUri.SafelyAppendPath("calendar/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2020-04-08 of the Calendar API.
  /// </summary>
  public Resources.Calendar.V2020_04_08.OrganizationResource V2020_04_08
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2020-04-08");
      return new(_baseUri.SafelyAppendPath("calendar/v2"), _client);
    }
  }

  /// <summary>
  /// Gets a client for version 2018-08-01 of the Calendar API.
  /// </summary>
  public Resources.Calendar.V2018_08_01.OrganizationResource V2018_08_01
  {
    get
    {
      _client.DefaultRequestHeaders.AddPlanningCenterVersion("2018-08-01");
      return new(_baseUri.SafelyAppendPath("calendar/v2"), _client);
    }
  }
}

