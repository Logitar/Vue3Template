using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class RealmSortGraphType : EnumerationGraphType<RealmSort>
{
  public RealmSortGraphType()
  {
    Name = nameof(RealmSort);
    Description = "...";

    Add(new EnumValueDefinition(nameof(RealmSort.DisplayName), RealmSort.DisplayName)
    {
      Description = "..."
    });
    Add(new EnumValueDefinition(nameof(RealmSort.UniqueName), RealmSort.UniqueName)
    {
      Description = "..."
    });
    Add(new EnumValueDefinition(nameof(RealmSort.UpdatedOn), RealmSort.UpdatedOn)
    {
      Description = "..."
    });
  }
}
