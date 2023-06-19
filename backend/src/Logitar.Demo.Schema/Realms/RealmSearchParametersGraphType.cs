using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class RealmSearchParametersGraphType : SearchParametersGraphType<RealmSearchParameters>
{
  public RealmSearchParametersGraphType()
  {
    Name = nameof(RealmSearchParameters);
    Description = "...";

    Field(x => x.Sort, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<RealmSortGraphType>>>))
      .DefaultValue(new List<RealmSortParameters>())
      .Description("...");
  }
}
