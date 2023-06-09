using Logitar.EventSourcing;
using MediatR;

namespace Logitar.Demo.Core.Realms.Events;

public record RealmDeleted : DomainEvent, INotification
{
  public RealmDeleted() => DeleteAction = DeleteAction.Delete;
}
