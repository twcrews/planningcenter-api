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

    StringBuilder result = new();
    bool capitalizeNext = true;
    bool previousWasUpper = false;

    for (int i = 0; i < input.Length; i++)
    {
      char c = input[i];

      // Check if this is a delimiter (skipped in output)
      if (c == '_' || c == '-' || c == ' ')
      {
        capitalizeNext = true;
        previousWasUpper = false;
        continue;
      }

      // Check if this is a visible separator (kept in output but triggers capitalization)
      if (c == '.')
      {
        result.Append(c);
        capitalizeNext = true;
        previousWasUpper = false;
        continue;
      }

      // Handle digit-to-letter transition
      if (char.IsLetter(c) && i > 0 && char.IsDigit(input[i - 1]))
      {
        capitalizeNext = true;
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
