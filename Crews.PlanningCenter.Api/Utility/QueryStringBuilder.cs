namespace Crews.PlanningCenter.Api.Utility;

/// <summary>
/// A builder class that allows for easy creation and modification of URI query strings.
/// </summary>
public class QueryStringBuilder
{
	/// <summary>
	/// Gets or sets the parameters in the query string.
	/// </summary>
	public List<QueryString.QueryStringParameter> Parameters { get; set; } = [];

	/// <summary>
	/// Gets or sets the query string's starting delimiter. Defaults to '?'.
	/// </summary>
	public string BeginningDelimiter { get; set; } = "?";

	/// <summary>
	/// Gets or sets the query string's ending delimiter. Defaults to an empty string.
	/// </summary>
	public string EndingDelimiter { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the query string's parameter separation delimiter. Defaults to '&amp;'.
	/// </summary>
	public string ParameterDelimiter { get; set; } = "&";

	/// <summary>
	/// Gets or sets the query string's parameter value assignment delimiter. Defaults to '='.
	/// </summary>
	public string ParameterAssignmentDelimiter { get; set; } = "=";

	/// <summary>
	/// Gets or sets the query string's parameter value array separation delimiter. Defaults to ','.
	/// </summary>
	public string ParameterValuesDelimiter { get; set; } = ",";

	/// <summary>
	/// Creates a new query string builder preconfigured using the given QueryString instance.
	/// </summary>
	/// <param name="source">The query string instance from which to configure the builder.</param>
	public QueryStringBuilder(QueryString source) => Parameters = [.. source.Parameters];

	/// <summary>
	/// Creates a new query string builder preconfigured using the given string.
	/// </summary>
	/// <param name="source">The string from which to configure the builder.</param>
	public QueryStringBuilder(string source) 
		=> Parameters = string.IsNullOrWhiteSpace(source) ? [] : [.. new QueryString(source).Parameters];

	/// <summary>
	/// Creates a new empty query string builder.
	/// </summary>
	public QueryStringBuilder() { }

	/// <summary>
	/// Gets the QueryString instance constructed by the specified QueryStringBuilder instance.
	/// </summary>
	/// <returns>A QueryString instance.</returns>
	public QueryString QueryString =>
		new(BeginningDelimiter +
			string.Join(ParameterDelimiter, Parameters.Select(p =>
				p.Key +
				ParameterAssignmentDelimiter +
				string.Join(ParameterValuesDelimiter, p.Values))) +
			EndingDelimiter)
		{
			BeginningDelimiter = BeginningDelimiter,
			ParameterDelimiter = ParameterDelimiter,
			ParameterAssignmentDelimiter = ParameterAssignmentDelimiter,
			ParameterValuesDelimiter = ParameterValuesDelimiter,
			EndingDelimiter = EndingDelimiter
		};
}
