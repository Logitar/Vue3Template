using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class SearchResultsGraphType<TModel, TGraph> : ObjectGraphType<SearchResults<TModel>> where TGraph : IGraphType
{
  public SearchResultsGraphType()
  {
    Name = nameof(SearchResults<TGraph>);
    Description = "Represents a set of results.";

    Field(x => x.Results, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<TGraph>>>))
      .Description("The list of results.");
    Field(x => x.Total)
      .Description("The total number of results.");
  }
}
