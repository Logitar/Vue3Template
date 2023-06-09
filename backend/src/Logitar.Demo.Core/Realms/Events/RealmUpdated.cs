using MediatR;

namespace Logitar.Demo.Core.Realms.Events;

public record RealmUpdated : RealmSaved, INotification;
