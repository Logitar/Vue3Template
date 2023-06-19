using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class RealmSortParametersGraphType : InputObjectGraphType<RealmSortParameters>
{
  public RealmSortParametersGraphType()
  {
    Name = nameof(RealmSortParameters);
    Description = "...";

    Field(x => x.Field, type: typeof(NonNullGraphType<RealmSortGraphType>)).Description("...");
    Field(x => x.IsDescending).DefaultValue(false).Description("...");
  }
}
