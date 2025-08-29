using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Crews.PlanningCenter.Api.Authentication;

/// <summary>
/// OAuth handler for Planning Center authentication.
/// </summary>
public class PlanningCenterOAuthHandler : OAuthHandler<PlanningCenterOAuthOptions>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlanningCenterOAuthHandler"/> class.
    /// </summary>
    /// <param name="options">Configuration options for Planning Center OAuth.</param>
    /// <param name="logger">Logger factory for logging.</param>
    /// <param name="encoder">Encoder for URL encoding.</param>
    public PlanningCenterOAuthHandler(IOptionsMonitor<PlanningCenterOAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        using var response = await Backchannel.SendAsync(request, Context.RequestAborted);
        if (!response.IsSuccessStatusCode)
        {
            Logger.LogError("An error occurred while retrieving the user profile: the remote server " +
                           "returned a {Status} response with the following payload: {Headers} {Body}.",
                           response.StatusCode, response.Headers.ToString(), await response.Content.ReadAsStringAsync(Context.RequestAborted));

            throw new HttpRequestException("An error occurred while retrieving the user profile.");
        }

        using var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync(Context.RequestAborted));
        var data = payload.RootElement.GetProperty("data");

        var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, data);

        context.RunClaimActions();

        // Get email from separate endpoint
        await AddEmailClaimsAsync(context, tokens.AccessToken);

        await Events.CreatingTicket(context);
        return new AuthenticationTicket(context.Principal!, context.Properties, Scheme.Name);
    }

    private async Task AddEmailClaimsAsync(OAuthCreatingTicketContext context, string accessToken)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, PlanningCenterOAuthDefaults.UserEmailsEndpoint);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await Backchannel.SendAsync(request, Context.RequestAborted);
            if (response.IsSuccessStatusCode)
            {
                using var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync(Context.RequestAborted));
                var emails = payload.RootElement.GetProperty("data");

                // Find primary email or first email
                foreach (var email in emails.EnumerateArray())
                {
                    var attributes = email.GetProperty("attributes");
                    var emailAddress = attributes.GetProperty("address").GetString();
                    var isPrimary = attributes.TryGetProperty("primary", out var primary) && primary.GetBoolean();

                    if (!string.IsNullOrEmpty(emailAddress))
                    {
                        context.Identity.AddClaim(new Claim(ClaimTypes.Email, emailAddress, ClaimValueTypes.String, Options.ClaimsIssuer));
                        
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