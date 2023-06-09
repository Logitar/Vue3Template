namespace Logitar.Demo.Core.Realms;

public record RealmSortParameters : SortParameters
{
  public new RealmSort Field { get; set; }
}
