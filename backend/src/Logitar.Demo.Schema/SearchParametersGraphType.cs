using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal abstract class SearchParametersGraphType<T> : InputObjectGraphType<T> where T : SearchParameters
{
  public SearchParametersGraphType()
  {
    Field(x => x.Search, type: typeof(NonNullGraphType<TextSearchGraphType>))
      .DefaultValue(new TextSearch())
      .Description("...");

    Field(x => x.Sort, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<SortParametersGraphType>>>))
      .DefaultValue(new List<SortParameters>())
      .Description("...");

    Field(x => x.Skip).DefaultValue(0).Description("...");

    Field(x => x.Limit).DefaultValue(0).Description("...");
  }
}
