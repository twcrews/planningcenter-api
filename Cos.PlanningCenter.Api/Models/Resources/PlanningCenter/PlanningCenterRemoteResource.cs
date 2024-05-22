namespace Cos.PlanningCenter.Api.Models.Resources.PlanningCenter;

/// <summary>
/// Represents any resource on the Planning Center API.
/// </summary>
/// <param name="uri">The URI of the remote resource.</param>
public abstract class PlanningCenterRemoteResource(Uri uri) : IRemoteResource
{
    /// <inheritdoc />
    public Uri Uri { get; protected set; } = uri;
}