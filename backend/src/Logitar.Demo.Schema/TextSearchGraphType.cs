using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class TextSearchGraphType : InputObjectGraphType<TextSearch>
{
  public TextSearchGraphType()
  {
    Name = nameof(TextSearch);
    Description = "Represents the options available to perform a text-search.";

    Field(x => x.Terms, type: typeof(ListGraphType<NonNullGraphType<SearchTermGraphType>>))
      .DefaultValue(new List<SearchTerm>())
      .Description("The terms to use in the text-search.");
    Field(x => x.Operator, nullable: true).DefaultValue(QueryOperator.And)
      .Description("The operator to use between each term.");
  }
}
