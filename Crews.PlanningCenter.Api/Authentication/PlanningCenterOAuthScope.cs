namespace Crews.PlanningCenter.Auth.Models;

/// <summary>
/// Represents the various OAuth scopes available for Planning Center API access.
/// </summary>
public readonly record struct PlanningCenterOAuthScope
{
    private readonly string _value;

    /// <summary>
    /// OAuth scope for Planning Center's Calendar API.
    /// </summary>
    public static PlanningCenterOAuthScope Calendar => new("calendar");

    /// <summary>
    /// OAuth scope for Planning Center's Check-Ins API.
    /// </summary>
    public static PlanningCenterOAuthScope CheckIns => new("check_ins");

    /// <summary>
    /// OAuth scope for Planning Center's Giving API.
    /// </summary>
    public static PlanningCenterOAuthScope Giving => new("giving");

    /// <summary>
    /// OAuth scope for Planning Center's Groups API.
    /// </summary>
    public static PlanningCenterOAuthScope Groups => new("groups");

    /// <summary>
    /// OAuth scope for Planning Center's People API.
    /// </summary>
    public static PlanningCenterOAuthScope People => new("people");

    /// <summary>
    /// OAuth scope for Planning Center's Publishing API.
    /// </summary>
    public static PlanningCenterOAuthScope Publishing => new("publishing");

    /// <summary>
    /// OAuth scope for Planning Center's Registrations API.
    /// </summary>
    public static PlanningCenterOAuthScope Registrations => new("registrations");

    /// <summary>
    /// OAuth scope for Planning Center's Services API.
    /// </summary>
    public static PlanningCenterOAuthScope Services => new("services");

    public static implicit operator string(PlanningCenterOAuthScope scope) => scope._value;

    public static implicit operator PlanningCenterOAuthScope(string value) => new(value);

    public override string ToString() => _value;

    private PlanningCenterOAuthScope(string value) => _value = value;
}
