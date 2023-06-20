using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class RealmSearchParametersGraphType : SearchParametersGraphType<RealmSearchParameters>
{
  public RealmSearchParametersGraphType() : base(noSortField: true)
  {
    Name = nameof(RealmSearchParameters);
    Description = "Represents the parameters used to search realms.";

    AddSortField<NonNullGraphType<ListGraphType<NonNullGraphType<RealmSortParametersGraphType>>>>()
      .DefaultValue(new List<RealmSortParameters>());
  }
}
