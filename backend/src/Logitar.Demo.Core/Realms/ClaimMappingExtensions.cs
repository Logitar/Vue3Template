namespace Logitar.Demo.Core.Realms;

internal static class ClaimMappingExtensions
{
  public static Dictionary<string, ReadOnlyClaimMapping> CleanTrim(this Dictionary<string, ReadOnlyClaimMapping> claimMappings)
  {
    return claimMappings.Where(x => !string.IsNullOrWhiteSpace(x.Key))
      .GroupBy(x => x.Key.Trim())
      .ToDictionary(x => x.Key, x => x.Last().Value);
  }

  public static Dictionary<string, ReadOnlyClaimMapping> ToDictionary(this IEnumerable<ClaimMapping> claimMappings)
  {
    return claimMappings.Where(x => !string.IsNullOrWhiteSpace(x.Key) && !string.IsNullOrWhiteSpace(x.Type))
      .GroupBy(x => x.Key.Trim())
      .ToDictionary(x => x.Key, x => new ReadOnlyClaimMapping(x.Last().Type, x.Last().ValueType));
  }
}
