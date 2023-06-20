using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class SearchTermGraphType : ObjectGraphType<SearchTerm>
{
  public SearchTermGraphType()
  {
    Name = nameof(SearchTerm);
    Description = "Represents a term in a text-search.";

    Field(x => x.Value).Description("The textual value of the term.");
  }
}
