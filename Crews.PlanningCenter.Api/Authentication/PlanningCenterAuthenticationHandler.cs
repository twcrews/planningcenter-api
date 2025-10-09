using Crews.PlanningCenter.Api.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// Delegating handler that automatically adds authentication to outgoing Planning Center API requests.
/// Supports both Personal Access Token (from configuration) and OAuth access tokens (from user context).
/// </summary>
/// <remarks>
/// Creates a new instance of the handler.
/// </remarks>
/// <param name="httpContextAccessor">Accessor for the current HTTP context.</param>
/// <param name="options">Configuration options for the Planning Center API.</param>
public class PlanningCenterAuthenticationHandler(
	IHttpContextAccessor httpContextAccessor,
	IOptions<PlanningCenterClientOptions> options) : DelegatingHandler
{
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
	private readonly PlanningCenterClientOptions _options = options.Value;

	/// <inheritdoc />
	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request, CancellationToken cancellationToken)
	{
		// Only add authentication if there isn't already an authorization header
		// This allows per-request overrides if needed
		if (request.Headers.Authorization is null)
		{
			if (_options.PersonalAccessToken is not null)
			{
				request.Headers.Authorization = _options.PersonalAccessToken;
			}
			else
			{
				HttpContext? httpContext = _httpContextAccessor.HttpContext;

				if (httpContext is not null)
				{
					string? accessToken = await httpContext.GetTokenAsync("access_token");

					if (!string.IsNullOrEmpty(accessToken))
					{
						request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
					}
				}
			}
		}

		return await base.SendAsync(request, cancellationToken);
	}
}
