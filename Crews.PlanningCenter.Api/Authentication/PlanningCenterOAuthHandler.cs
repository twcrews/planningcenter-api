using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// OAuth handler for Planning Center authentication.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PlanningCenterOAuthHandler"/> class.
/// </remarks>
/// <param name="options">Configuration options for Planning Center OAuth.</param>
/// <param name="logger">Logger factory for logging.</param>
/// <param name="encoder">Encoder for URL encoding.</param>
public class PlanningCenterOAuthHandler(
    IOptionsMonitor<PlanningCenterOAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder) 
    : OAuthHandler<PlanningCenterOAuthOptions>(options, logger, encoder)
{

	/// <summary>
	/// Creates an authentication ticket from the OAuth token response.
	/// </summary>
	/// <param name="identity">The claims identity.</param>
	/// <param name="properties">The authentication properties.</param>
	/// <param name="tokens">The OAuth token response.</param>
	/// <returns>An authentication ticket.</returns>
	protected override async Task<AuthenticationTicket> CreateTicketAsync(
		ClaimsIdentity identity,
		AuthenticationProperties properties,
		OAuthTokenResponse tokens)
    {
        using HttpRequestMessage request = new(HttpMethod.Get, Options.UserInformationEndpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        using HttpResponseMessage response = await Backchannel.SendAsync(request, Context.RequestAborted);
        if (!response.IsSuccessStatusCode)
        {
            Logger.LogError("An error occurred while retrieving the user profile: the remote server " +
                "returned a {Status} response with the following payload: {Headers} {Body}.",
                response.StatusCode, response.Headers.ToString(),
                await response.Content.ReadAsStringAsync(Context.RequestAborted));

            throw new HttpRequestException("An error occurred while retrieving the user profile.");
        }

        using JsonDocument payload = JsonDocument.Parse(
            await response.Content.ReadAsStringAsync(Context.RequestAborted));
		JsonElement data = payload.RootElement.GetProperty("data");

		OAuthCreatingTicketContext context = new(
            new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, data);

        context.RunClaimActions();

        // Get email from separate endpoint
        if (!string.IsNullOrEmpty(tokens.AccessToken))
        {
            await AddEmailClaimsAsync(context, tokens.AccessToken);
        }

        await Events.CreatingTicket(context);
        return new AuthenticationTicket(context.Principal!, context.Properties, Scheme.Name);
    }

    private async Task AddEmailClaimsAsync(OAuthCreatingTicketContext context, string accessToken)
    {
        try
        {
            using HttpRequestMessage request = new(HttpMethod.Get, PlanningCenterOAuthDefaults.UserEmailsEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using HttpResponseMessage response = await Backchannel.SendAsync(request, Context.RequestAborted);
            if (response.IsSuccessStatusCode)
            {
                using JsonDocument payload = JsonDocument.Parse(
                    await response.Content.ReadAsStringAsync(Context.RequestAborted));
				JsonElement emails = payload.RootElement.GetProperty("data");

                // Find primary email or first email
                foreach (JsonElement email in emails.EnumerateArray())
                {
					JsonElement attributes = email.GetProperty("attributes");
					string? emailAddress = attributes.GetProperty("address").GetString();
					bool isPrimary = attributes.TryGetProperty("primary", out JsonElement primary)
                        && primary.GetBoolean();

                    if (!string.IsNullOrEmpty(emailAddress))
                    {
                        context.Identity?.AddClaim(
                            new Claim(ClaimTypes.Email, emailAddress, ClaimValueTypes.String, Options.ClaimsIssuer));
                        
                        // If this is the primary email, we can break early
                        if (isPrimary)
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "Failed to retrieve user email information from Planning Center");
        }
    }
}