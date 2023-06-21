using GraphQL.Builders;
using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal abstract class SearchParametersGraphType<T> : InputObjectGraphType<T> where T : SearchParameters
{
  public SearchParametersGraphType(bool noSearchField = false,
    bool noSortField = false,
    bool noSkipField = false,
    bool noLimitField = false)
  {
    if (!noSearchField)
    {
      Field(x => x.Search, type: typeof(TextSearchGraphType)).DefaultValue(new TextSearch())
        .Description("The parameters used to perform a global text-search.");
    }

    if (!noSortField)
    {
      AddSortField<ListGraphType<NonNullGraphType<SortParametersGraphType>>>()
        .DefaultValue(new List<SortParameters>());
    }

    if (!noSkipField)
    {
      Field(x => x.Skip, nullable: true).DefaultValue(0)
        .Description("The number of results to skip.");
    }

    if (!noLimitField)
    {
      Field(x => x.Limit, nullable: true).DefaultValue(0)
        .Description("The number of results to return.");
    }
  }

  protected FieldBuilder<T, IEnumerable<SortParameters>> AddSortField<TField>(string? description = null)
  {
    return Field(x => x.Sort, type: typeof(TField))
      .Description(description ?? "The parameters used to sort the results.");
  }
}
