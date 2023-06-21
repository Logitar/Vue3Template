using GraphQL.Types;
using Logitar.Demo.Core.Realms;
using Logitar.Demo.Schema.Extensions;

namespace Logitar.Demo.Schema.Realms;

internal class RealmSortGraphType : EnumerationGraphType<RealmSort>
{
  public RealmSortGraphType()
  {
    Name = nameof(RealmSort);
    Description = "Defines the available options to sort realms.";

    this.AddValue(RealmSort.DisplayName, "The realms will be sorted by their display name.");
    this.AddValue(RealmSort.UniqueName, "The realms will be sorted by their unique name.");
    this.AddValue(RealmSort.UpdatedOn, "The realms will be sorted by their latest update date and time.");
  }
}
