using Crews.PlanningCenter.Api.Utility;

namespace Crews.PlanningCenter.Api.Extensions;

static class UriExtensions
{
	public static Uri SetQueryString(this Uri uri, QueryString queryString)
	{
		UriBuilder uriBuilder = new(uri)
		{
			Query = queryString.ToString()
		};
		return uriBuilder.Uri;
	}

	public static Uri ClearQueryString(this Uri uri)
	{
		UriBuilder uriBuilder = new(uri)
		{
			Query = string.Empty
		};
		return uriBuilder.Uri;
	}
}
