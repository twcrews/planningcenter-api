using System.Text;
using Crews.PlanningCenter.Api.Extensions;

namespace Crews.PlanningCenter.Api.Tests.Extensions;

public class StringExtensionsTest
{
	[Theory(DisplayName = "Base64Encode correctly encodes strings.")]
	[InlineData("apple", "utf-8", "YXBwbGU=")]
	[InlineData("banana", "us-ascii", "YmFuYW5h")]
	[InlineData("cranberry", "utf-32", "YwAAAHIAAABhAAAAbgAAAGIAAABlAAAAcgAAAHIAAAB5AAAA")]
	[InlineData(
		"The quick brown fox jumps over the lazy dog.", 
		null,  // Encoder should default to UTF8.
		"VGhlIHF1aWNrIGJyb3duIGZveCBqdW1wcyBvdmVyIHRoZSBsYXp5IGRvZy4=")]
	public void Base64Encode_EncodesCorrectly(string input, string? encoding, string expected)
	{
		Encoding expectedEncoding = Encoding.UTF8;
		if (encoding != null)
		{
			expectedEncoding = Encoding.GetEncodings()
				.Select(i => i.GetEncoding())
				.Single(i => i.BodyName == encoding);
		}

		string actual = encoding == null ? input.Base64Encode() : input.Base64Encode(expectedEncoding);
		Assert.Equal(expected, actual);
	}
}
