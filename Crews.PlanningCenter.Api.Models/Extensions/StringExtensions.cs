using System.Text;

namespace Crews.PlanningCenter.Api.Models.Extensions;

public static class StringExtensions
{
  /// <summary>
  /// Idempotently converts a string to PascalCase.
  /// </summary>
  public static string ToPascalCase(this string input)
  {
    if (string.IsNullOrWhiteSpace(input)) return input;

    var result = new StringBuilder();
    bool capitalizeNext = true;
    bool previousWasUpper = false;

    for (int i = 0; i < input.Length; i++)
    {
      char c = input[i];

      // Check if this is a delimiter
      if (c == '_' || c == '-' || c == ' ')
      {
        capitalizeNext = true;
        previousWasUpper = false;
        continue;
      }

      // Handle transition from lowercase to uppercase (camelCase detection)
      if (char.IsUpper(c) && i > 0 && !previousWasUpper &&
          char.IsLower(input[i - 1]))
      {
        capitalizeNext = true;
      }

      if (capitalizeNext)
      {
        result.Append(char.ToUpper(c));
        capitalizeNext = false;
      }
      else
      {
        result.Append(char.ToLower(c));
      }

      previousWasUpper = char.IsUpper(c);
    }

    return result.ToString();
  }
}
