using Logitar.EventSourcing;
using System.Globalization;

namespace Logitar.Demo.Core.Realms.Events;

public abstract record RealmSaved : DomainEvent
{
  public string? DisplayName { get; init; }
  public string? Description { get; init; }

  public CultureInfo? DefaultLocale { get; init; }
  public string Secret { get; init; } = string.Empty;
  public Uri? Url { get; init; }

  public bool RequireConfirmedAccount { get; init; }
  public bool RequireUniqueEmail { get; init; }

  public ReadOnlyUsernameSettings UsernameSettings { get; init; } = new();
  public ReadOnlyPasswordSettings PasswordSettings { get; init; } = new();

  public Dictionary<string, ReadOnlyClaimMapping> ClaimMappings { get; init; } = new();
  public Dictionary<string, string> CustomAttributes { get; init; } = new();
}
