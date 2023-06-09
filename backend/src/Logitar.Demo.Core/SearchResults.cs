namespace Logitar.Demo.Core;

public record SearchResults<T>
{
  public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();
  public long Total { get; set; }
}
