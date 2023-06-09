using MediatR;

namespace Logitar.Demo.Core.Realms.Events;

public record RealmCreated : RealmSaved, INotification
{
  public string UniqueName { get; init; } = string.Empty;
}
