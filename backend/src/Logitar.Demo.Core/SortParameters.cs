namespace Logitar.Demo.Core;

public record SortParameters
{
  public string Field { get; set; } = string.Empty;
  public bool IsDescending { get; set; }
}
