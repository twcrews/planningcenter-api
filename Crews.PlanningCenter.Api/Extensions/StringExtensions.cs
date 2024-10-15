using System.Text;

namespace Crews.PlanningCenter.Api.Extensions;

static class StringExtensions
{
	public static string TrimStart(this string target, string trimString)
	{
    if (string.IsNullOrEmpty(trimString)) return target;

    string result = target;
    while (result.StartsWith(trimString))
    {
      result = result[trimString.Length..];
    }

    return result;
	}

	public static string TrimEnd(this string target, string trimString)
	{
    if (string.IsNullOrEmpty(trimString)) return target;

    string result = target;
    while (result.EndsWith(trimString))
    {
      result = result[..^trimString.Length];
    }

    return result;
	}

  public static string Base64Encode(this string target, Encoding? encoding = null)
  {
    encoding ??= Encoding.UTF8;
    byte[] data = encoding.GetBytes(target);
    return Convert.ToBase64String(data);
  }
}
