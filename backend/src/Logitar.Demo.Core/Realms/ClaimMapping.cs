namespace Logitar.Demo.Core.Realms;

public record ClaimMapping
{
  public string Key { get; set; } = string.Empty;

  public string Type { get; set; } = string.Empty;
  public string? ValueType { get; set; }
}
