using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Crews.PlanningCenter.Api.Authentication;

public static class PlanningCenterOAuthExtensions
{
    /// <summary>
    /// Adds Planning Center OAuth authentication to the application.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddPlanningCenterOAuth(this AuthenticationBuilder builder)
        => builder.AddPlanningCenterOAuth(PlanningCenterOAuthDefaults.AuthenticationScheme, _ => { });

    /// <summary>
    /// Adds Planning Center OAuth authentication to the application.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="configureOptions">The action to configure the Planning Center OAuth options.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddPlanningCenterOAuth(this AuthenticationBuilder builder, Action<PlanningCenterOAuthOptions> configureOptions)
        => builder.AddPlanningCenterOAuth(PlanningCenterOAuthDefaults.AuthenticationScheme, configureOptions);

    /// <summary>
    /// Adds Planning Center OAuth authentication to the application.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="authenticationScheme">The authentication scheme.</param>
    /// <param name="configureOptions">The action to configure the Planning Center OAuth options.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddPlanningCenterOAuth(this AuthenticationBuilder builder, string authenticationScheme, Action<PlanningCenterOAuthOptions> configureOptions)
        => builder.AddPlanningCenterOAuth(authenticationScheme, PlanningCenterOAuthDefaults.DisplayName, configureOptions);

    /// <summary>
    /// Adds Planning Center OAuth authentication to the application.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="authenticationScheme">The authentication scheme.</param>
    /// <param name="displayName">The display name for the authentication scheme.</param>
    /// <param name="configureOptions">The action to configure the Planning Center OAuth options.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthenticationBuilder AddPlanningCenterOAuth(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<PlanningCenterOAuthOptions> configureOptions)
    {
        return builder.AddOAuth<PlanningCenterOAuthOptions, PlanningCenterOAuthHandler>(authenticationScheme, displayName, configureOptions);
    }
}