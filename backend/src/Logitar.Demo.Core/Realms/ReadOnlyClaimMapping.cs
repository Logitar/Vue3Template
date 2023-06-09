using Logitar.EventSourcing;

namespace Logitar.Demo.Core.Realms;

public record ReadOnlyClaimMapping
{
  public ReadOnlyClaimMapping(string type, string? valueType = null)
  {
    if (string.IsNullOrWhiteSpace(type))
    {
      throw new ArgumentException("The claim type is required.", nameof(type));
    }

    Type = type.Trim();
    ValueType = valueType?.CleanTrim();
  }

  public string Type { get; }
  public string? ValueType { get; }
}
