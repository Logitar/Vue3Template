namespace Logitar.Demo.Core.Realms;

public record UpdateRealmInput
{
  public string? DisplayName { get; set; }
  public string? Description { get; set; }

  public string? DefaultLocale { get; set; }
  public string? Secret { get; set; }
  public string? Url { get; set; }

  public bool RequireConfirmedAccount { get; set; }
  public bool RequireUniqueEmail { get; set; }

  public UsernameSettings? UsernameSettings { get; set; }
  public PasswordSettings? PasswordSettings { get; set; }

  public IEnumerable<ClaimMapping>? ClaimMappings { get; set; }

  public IEnumerable<CustomAttribute>? CustomAttributes { get; set; }
}
