using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class SearchTermGraphType : ObjectGraphType<SearchTerm>
{
  public SearchTermGraphType()
  {
    Name = nameof(SearchTerm);
    Description = "...";

    Field(x => x.Value).Description("...");
  }
}
