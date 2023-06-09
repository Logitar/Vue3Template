namespace Logitar.Demo.Core;

public record SearchParameters
{
  public TextSearch Search { get; set; } = new();

  public IEnumerable<SortParameters> Sort { get; set; } = Enumerable.Empty<SortParameters>();

  public int Skip { get; set; }
  public int Limit { get; set; }
}
