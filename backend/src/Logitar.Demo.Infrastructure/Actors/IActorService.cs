using Logitar.EventSourcing;

namespace Logitar.Demo.Infrastructure.Actors;

internal interface IActorService
{
  Task<ActorInfo> GetAsync(DomainEvent change, CancellationToken cancellationToken = default);
}
