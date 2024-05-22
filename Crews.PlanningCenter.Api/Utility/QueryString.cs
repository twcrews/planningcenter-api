using System.Collections.Immutable;
using Crews.PlanningCenter.Api.Extensions;

namespace Crews.PlanningCenter.Api.Utility;

/// <summary>
/// Represents the query string at the end of a URI.
/// </summary>
public class QueryString
{
	private readonly string _queryString;

	/// <summary>
	/// A collection of the parameters in the query string.
	/// </summary>
	public IEnumerable<QueryStringParameter> Parameters => _queryString
		.TrimStart(BeginningDelimiter)
		.TrimEnd(EndingDelimiter)
		.Split(ParameterDelimiter, StringSplitOptions.RemoveEmptyEntries)
		.Select(parameterString => 
		{
			string[] parameterParts = parameterString.Split(ParameterAssignmentDelimiter);
			if (parameterParts.Length > 2) 
			{
				throw new FormatException("A query string parameter contains multiple assignment delimiters");
			}
			if (string.IsNullOrEmpty(parameterParts[0]))
			{
				throw new FormatException("A query string parameter name is empty");
			}
			return new QueryStringParameter()
			{
				Key = parameterParts[0],
				Values = parameterParts.Length == 2 ? [.. parameterParts[1].Split(ParameterValuesDelimiter)] : []
			};
		});

	/// <summary>
	/// The string at the start of the query string. Defaults to '?'.
	/// </summary>
	public string BeginningDelimiter { get; init; } = "?";

	/// <summary>
	/// The string at the end of the query string. Defaults to an empty string.
	/// </summary>
	public string EndingDelimiter { get; init; } = string.Empty;

	/// <summary>
	/// The string used to divide parameters. Defaults to '&amp;'.
	/// </summary>
	public string ParameterDelimiter { get; init; } = "&";

	/// <summary>
	/// The string used to divide parameter keys from their values. Defaults to '='.
	/// </summary>
	public string ParameterAssignmentDelimiter { get; init; } = "=";

	/// <summary>
	/// The string used to divide values in an array-style parameter assignment. Defaults to ','.
	/// </summary>
	public string ParameterValuesDelimiter { get; init; } = ",";

	/// <summary>
	/// Parses a string into a QueryString object. 
	/// Delimiters can be changed using the object initializer.
	/// The order of the string will be preserved when parsing and serializing via ToString(). 
	/// </summary>
	/// <param name="queryString">The string to be parsed.</param>
	public QueryString(string queryString)
	{
		if (string.IsNullOrWhiteSpace(queryString))
		{
			throw new FormatException("Query string was null, empty, or consisted only of whitespace characters.");
		}

		_queryString = queryString.Trim();
	}

	/// <inheritdoc />
	public override string ToString() => 
		BeginningDelimiter +
		string.Join(ParameterDelimiter, Parameters.Select(p => 
			p.Key +
			ParameterAssignmentDelimiter +
			string.Join(ParameterValuesDelimiter, p.Values))) +
		EndingDelimiter;

	/// <summary>
	/// Represents a parameter of a URI query string.
	/// </summary>
	public class QueryStringParameter
	{
		/// <summary>
		/// The key (name) of the parameter.
		/// </summary>
		public required string Key { get; init; }

		/// <summary>
		/// The values assigned to the parameter. The order of these values will be preserved.
		/// </summary>
		public ImmutableList<string> Values { get; init;} = [];
	}
}
