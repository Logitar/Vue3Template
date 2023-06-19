using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class TextSearchGraphType : InputObjectGraphType<TextSearch>
{
  public TextSearchGraphType()
  {
    Name = nameof(TextSearch);
    Description = "...";

    Field(x => x.Terms, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<TextSearchGraphType>>>))
      .DefaultValue(new List<SearchTerm>())
      .Description("...");
    Field(x => x.Operator).DefaultValue(QueryOperator.And);
  }
}
