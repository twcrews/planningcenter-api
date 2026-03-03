using System.Text.Json.Serialization;
using Crews.PlanningCenter.Api.Converters;

namespace Crews.PlanningCenter.Api.Groups;

/// <summary>
/// Represents the settings for a Group.
/// </summary>
public record GroupSettings
{
    /// <summary>
    /// A human-readable string representing the schedule for the group, such as "Sundays at 10am".
    /// </summary>
    [JsonPropertyName("schedule")]
    public string? Schedule { get; init; }

    /// <summary>
    /// The ID of the Location associated with the group.
    /// </summary>
    [JsonPropertyName("location_id")]
    public string? LocationId { get; init; }

    /// <summary>
    /// The URL of the virtual location for the group, if applicable (e.g., a Zoom link).
    /// </summary>
    [JsonPropertyName("virtual_location_url")]
    public Uri? VirtualLocationUrl { get; init; }

    /// <summary>
    /// The group's preference for location type.
    /// </summary>
    [JsonPropertyName("location_type_preference")]
    public string? LocationTypePreference { get; init; }

    /// <summary>
    /// The date until which enrollment is open for the group.
    /// </summary>
    [JsonPropertyName("enrollment_open_until")]
    public DateOnly? EnrollmentOpenUntil { get; init; }

    /// <summary>
    /// The maximum number of members that can be enrolled in the group.
    /// </summary>
    [JsonPropertyName("enrollment_limit")]
    public int? EnrollmentLimit { get; init; }

    /// <summary>
    /// Whether pending enrollments should be included in the enrollment limit count.
    /// </summary>
    [JsonConverter(typeof(BoolFromStringConverter))]
    [JsonPropertyName("enrollment_limit_include_pending")]
    public bool? EnrollmentLimitIncludePending { get; init; }

    /// <summary>
    /// The number of members that can be enrolled in the group before an alert is triggered.
    /// </summary>
    [JsonPropertyName("member_limit_maximum_alert")]
    public int? MemberLimitMaximumAlert { get; init; }

    /// <summary>
    /// Whether to request that leaders record attendance for events in this group.
    /// </summary>
    [JsonPropertyName("request_event_attendance_from_leaders")]
    public bool? RequestEventAttendanceFromLeaders { get; init; }

    /// <summary>
    /// The email address for the party responsible for the group.
    /// </summary>
    [JsonPropertyName("contact_email")]
    public string? ContactEmail { get; init; }

    /// <summary>
    /// The visibility level of the group.
    /// </summary>
    [JsonPropertyName("events_visibility")]
    public string? EventsVisibility { get; init; }

    /// <summary>
    /// A description of the group, represented in HTML.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Whether leaders of the group can search the entire People database when managing group members and attendance.
    /// </summary>
    [JsonPropertyName("leaders_can_search_people_database")]
    public bool? LeadersCanSearchPeopleDatabase { get; init; }

    /// <summary>
    /// Whether to publicly display the group's meeting schedule on the group page.
    /// </summary>
    [JsonPropertyName("publicly_display_meeting_schedule")]
    public bool? PubliclyDisplayMeetingSchedule { get; init; }

    /// <summary>
    /// Whether alerts for upcoming events should be automatically sent to group members.
    /// </summary>
    [JsonConverter(typeof(BoolFromStringConverter))]
    [JsonPropertyName("default_event_automated_reminders_enabled")]
    public bool? DefaultEventAutomatedRemindersEnabled { get; init; }

    /// <summary>
    /// How long before an event's start time automated reminders should be sent to group members.
    /// </summary>
    [JsonConverter(typeof(TimeSpanFromSecondsConverter))]
    [JsonPropertyName("default_event_automated_reminders_schedule_offset")]
    public TimeSpan? DefaultEventAutomatedRemindersScheduleOffset { get; init; }

    /// <summary>
    /// Whether to automatically enable RSVP for events in this group.
    /// </summary>
    [JsonPropertyName("default_event_rsvps_enabled")]
    public bool? DefaultEventRsvpsEnabled { get; init; }

    /// <summary>
    /// Unknown setting; use with caution.
    /// </summary>
    [JsonPropertyName("communication_enabled")]
    public bool? CommunicationEnabled { get; init; }

    /// <summary>
    /// Unknown setting; use with caution.
    /// </summary>
    [JsonPropertyName("members_can_create_forum_topics")]
    public bool? MembersCanCreateForumTopics { get; init; }

    /// <summary>
    /// The ID of the Person to whom attendance reply notifications should be sent for events in this group.
    /// </summary>
    [JsonPropertyName("attendance_reply_to_person_id")]
    public string? AttendanceReplyToPersonId { get; init; }

    /// <summary>
    /// Whether to publicly display the name of the group leader on the group page.
    /// </summary>
    [JsonPropertyName("leader_name_visible_on_public_page")]
    public bool? LeaderNameVisibleOnPublicPage { get; init; }

    /// <summary>
    /// Whether the members of the group should be publicly visible on the group page.
    /// </summary>
    [JsonPropertyName("members_are_confidential")]
    public bool? MembersAreConfidential { get; init; }

    /// <summary>
    /// Whether to disable scam prevention alerts for the group.
    /// </summary>
    [JsonPropertyName("scam_prevention_alerts_disabled")]
    public bool? ScamPreventionAlertsDisabled { get; init; }
}
