namespace Logitar.Demo.Core.Realms;

public record Realm : Aggregate
{
  public Guid Id { get; set; }

  public string UniqueName { get; set; } = string.Empty;
  public string? DisplayName { get; set; }
  public string? Description { get; set; }

  public string? DefaultLocale { get; set; }
  public string Secret { get; set; } = string.Empty;
  public string? Url { get; set; }

  public bool RequireConfirmedAccount { get; set; }
  public bool RequireUniqueEmail { get; set; }

  public UsernameSettings UsernameSettings { get; set; } = new();
  public PasswordSettings PasswordSettings { get; set; } = new();

  public IEnumerable<ClaimMapping> ClaimMappings { get; set; } = Enumerable.Empty<ClaimMapping>();

  public IEnumerable<CustomAttribute> CustomAttributes { get; set; } = Enumerable.Empty<CustomAttribute>();
}
