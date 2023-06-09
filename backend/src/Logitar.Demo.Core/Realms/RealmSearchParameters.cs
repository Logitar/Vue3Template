namespace Logitar.Demo.Core.Realms;

public record RealmSearchParameters : SearchParameters
{
  public new IEnumerable<RealmSortParameters> Sort { get; set; } = Enumerable.Empty<RealmSortParameters>();
}
