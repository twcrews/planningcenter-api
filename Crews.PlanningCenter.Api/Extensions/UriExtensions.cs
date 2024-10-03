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

	/// <summary>
	/// Appends the given <paramref name="path"/> to the <paramref name="uri"/>.
	/// </summary>
	/// <param name="uri">The original URI instance.</param>
	/// <param name="path">The path to append.</param>
	/// <returns>A new URI instance with the <paramref name="path"/> appended.</returns>
	/// <remarks>
	/// The <see cref="Uri"/> class provides a helpful constructor for appending paths: <c>new Uri(Uri, string)</c>. 
	/// Unfortunately, this constructor can produce siginificantly variable results depending on the value of its
	/// parameters; specifically, the presence of leading and trailing slashes can cause parts of the URI path to be lost.
	/// </remarks>
	public static Uri SafelyAppendPath(this Uri uri, string path)
	{
		string uriString = uri.OriginalString;
		uriString = uriString.TrimEnd('/');
		path = path.TrimStart('/');
		return new($"{uriString}/{path}");
	}
}
