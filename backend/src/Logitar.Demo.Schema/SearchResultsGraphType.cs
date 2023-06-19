using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class SearchResultsGraphType<TModel, TGraph> : ObjectGraphType<SearchResults<TModel>> where TGraph : IGraphType
{
  public SearchResultsGraphType()
  {
    Name = nameof(SearchResults<TGraph>);
    Description = "...";

    Field(x => x.Results, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<TGraph>>>)).Description("...");
    Field(x => x.Total).Description("...");
  }
}
