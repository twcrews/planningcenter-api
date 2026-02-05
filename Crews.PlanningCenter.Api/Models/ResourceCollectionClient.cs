namespace Crews.PlanningCenter.Api.Models;

public abstract class ResourceCollectionClient<T> : ResourceClient<IEnumerable<T>>
{
    public ResourceCollectionClient(HttpClient httpClient, Uri uri)
        : base(httpClient, uri)
    {
    }
}