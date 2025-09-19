using Crews.PlanningCenter.Api.Clients;
using Crews.PlanningCenter.Api.Resources.People.V2025_07_17;
using Crews.PlanningCenter.Models.People.V2025_07_17.Parameters;
using Crews.PlanningCenter.Models.People.V2025_07_17.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using Crews.PlanningCenter.Api.Models;

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
    /// <inheritdoc />
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

        // Log the number of claims that were successfully extracted
        Logger.LogDebug("Successfully extracted {ClaimCount} claims from user information", context.Identity?.Claims.Count() ?? 0);

        // Get email claims from separate endpoint
        if (!string.IsNullOrEmpty(tokens.AccessToken))
        {
            await AddClaimsAsync(context, tokens.AccessToken);
        }

        await Events.CreatingTicket(context);
        return new AuthenticationTicket(context.Principal!, context.Properties, Scheme.Name);
    }

    private async Task AddClaimsAsync(OAuthCreatingTicketContext context, string accessToken)
    {
        try
        {
            Backchannel.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            PeopleClient client = new(Backchannel);
            JsonApiSingletonResponse<Person> userInfoResponse = await client.LatestVersion
                .GetRelated<PersonResource>("me")
                .Include(PersonIncludable.Emails)
                .GetAsync();

            using HttpResponseMessage response = userInfoResponse.RawResponse;
            if (response.IsSuccessStatusCode)
            {
                MapUserClaims(context.Identity, userInfoResponse.Data);

                string? emailAddress = userInfoResponse.JsonApiDocument?
                    .GetIncludedResources()
                    .Select(i => i.Attributes.SingleOrDefault(attribute => attribute.Name == "address"))
                    .FirstOrDefault()?
                    .ToClrObject<string>();

                if (emailAddress is not null)
                {
                    context.Identity?.AddClaim(
                        new Claim(ClaimTypes.Email, emailAddress, ClaimValueTypes.String, Options.ClaimsIssuer));

                    Logger.LogDebug("Successfully added email claim");
                }
            }
            else
            {
                Logger.LogWarning("Failed to fetch user claims: {StatusCode}", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "Failed to fetch user claims");
        }
    }

    private void MapUserClaims(ClaimsIdentity? identity, Person? data)
    {
        if (identity is null || data is null) return;

        if (data.ID is not null) identity.AddClaim(
            new Claim(ClaimTypes.NameIdentifier, data.ID, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.Name is not null) identity.AddClaim(
            new Claim(ClaimTypes.Name, data.Name, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.FirstName is not null) identity.AddClaim(
            new Claim(ClaimTypes.GivenName, data.FirstName, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.LastName is not null) identity.AddClaim(
            new Claim(ClaimTypes.Surname, data.LastName, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.Birthdate is not null) identity.AddClaim(
            new Claim(ClaimTypes.DateOfBirth, ((DateOnly)data.Birthdate).ToString("O"), ClaimValueTypes.Date, Options.ClaimsIssuer));
        if (data.Gender is not null) identity.AddClaim(
            new Claim(ClaimTypes.Gender, data.Gender, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.MiddleName is not null) identity.AddClaim(
            new Claim("middle_name", data.MiddleName, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.Nickname is not null) identity.AddClaim(
            new Claim("nickname", data.Nickname, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.Avatar is not null) identity.AddClaim(
            new Claim("picture", data.Avatar, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.Status is not null) identity.AddClaim(
            new Claim("urn:planningcenter:status", data.Status, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.Child is not null) identity.AddClaim(
            new Claim("urn:planningcenter:child", ((bool)data.Child).ToString(), ClaimValueTypes.Boolean, Options.ClaimsIssuer));
        if (data.Grade is not null) identity.AddClaim(
            new Claim("urn:planningcenter:grade", ((int)data.Grade).ToString(), ClaimValueTypes.Integer, Options.ClaimsIssuer));
        if (data.GraduationYear is not null) identity.AddClaim(
            new Claim("urn:planningcenter:graduation_year", ((int)data.GraduationYear).ToString(), ClaimValueTypes.Integer, Options.ClaimsIssuer));
        if (data.Membership is not null) identity.AddClaim(
            new Claim("urn:planningcenter:membership", data.Membership, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.SchoolType is not null) identity.AddClaim(
            new Claim("urn:planningcenter:school_type", data.SchoolType, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.Anniversary is not null) identity.AddClaim(
            new Claim("urn:planningcenter:anniversary", ((DateOnly)data.Anniversary).ToString("O"), ClaimValueTypes.Date, Options.ClaimsIssuer));
        if (data.PeoplePermissions is not null) identity.AddClaim(
            new Claim("urn:planningcenter:people_permissions", data.PeoplePermissions, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.AccountingAdministrator is not null) identity.AddClaim(
            new Claim("urn:planningcenter:accounting_admin", ((bool)data.AccountingAdministrator).ToString(), ClaimValueTypes.Boolean, Options.ClaimsIssuer));
        if (data.SiteAdministrator is not null) identity.AddClaim(
            new Claim("urn:planningcenter:site_admin", ((bool)data.SiteAdministrator).ToString(), ClaimValueTypes.Boolean, Options.ClaimsIssuer));
        if (data.CanCreateForms is not null) identity.AddClaim(
            new Claim("urn:planningcenter:can_create_forms", ((bool)data.CanCreateForms).ToString(), ClaimValueTypes.Boolean, Options.ClaimsIssuer));
        if (data.CanEmailLists is not null) identity.AddClaim(
            new Claim("urn:planningcenter:can_email_lists", ((bool)data.CanEmailLists).ToString(), ClaimValueTypes.Boolean, Options.ClaimsIssuer));
        if (data.PassedBackgroundCheck is not null) identity.AddClaim(
            new Claim("urn:planningcenter:background_check", ((bool)data.PassedBackgroundCheck).ToString(), ClaimValueTypes.Boolean, Options.ClaimsIssuer));
        if (data.GivenName is not null) identity.AddClaim(
            new Claim("urn:planningcenter:given_name", data.GivenName, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.RemoteId is not null) identity.AddClaim(
            new Claim("urn:planningcenter:remote_id", ((int)data.RemoteId).ToString(), ClaimValueTypes.Integer, Options.ClaimsIssuer));
        if (data.DemographicAvatarUrl is not null) identity.AddClaim(
            new Claim("urn:planningcenter:demographic_avatar_url", data.DemographicAvatarUrl, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.InactivatedAt is not null) identity.AddClaim(
            new Claim("urn:planningcenter:inactivated_at", ((DateTime)data.InactivatedAt).ToString("O"), ClaimValueTypes.DateTime, Options.ClaimsIssuer));
        if (data.MedicalNotes is not null) identity.AddClaim(
            new Claim("urn:planningcenter:medical_notes", data.MedicalNotes, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.CreatedAt is not null) identity.AddClaim(
            new Claim("urn:planningcenter:created_at", ((DateTime)data.CreatedAt).ToString("O"), ClaimValueTypes.DateTime, Options.ClaimsIssuer));
        if (data.UpdatedAt is not null) identity.AddClaim(
            new Claim("urn:planningcenter:updated_at", ((DateTime)data.UpdatedAt).ToString("O"), ClaimValueTypes.DateTime, Options.ClaimsIssuer));
        if (data.DirectoryStatus is not null) identity.AddClaim(
            new Claim("urn:planningcenter:directory_status", data.DirectoryStatus, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.LoginIdentifier is not null) identity.AddClaim(
            new Claim("urn:planningcenter:login_identifier", data.LoginIdentifier, ClaimValueTypes.String, Options.ClaimsIssuer));
        if (data.MfaConfigured is not null) identity.AddClaim(
            new Claim("urn:planningcenter:mfa_configured", ((bool)data.MfaConfigured).ToString(), ClaimValueTypes.Boolean, Options.ClaimsIssuer));
        if (data.StripeCustomerIdentifier is not null) identity.AddClaim(
            new Claim("urn:planningcenter:stripe_customer_identifier", data.StripeCustomerIdentifier, ClaimValueTypes.String, Options.ClaimsIssuer));
    }
}