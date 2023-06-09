using Logitar.Demo.Core.Realms.Events;
using Logitar.Demo.Infrastructure.Actors;
using Logitar.Demo.Infrastructure.Entities;
using MediatR;

namespace Logitar.Demo.Infrastructure.Handlers.Realms;

internal class RealmCreatedHandler : INotificationHandler<RealmCreated>
{
  private readonly IActorService _actorService;
  private readonly DemoContext _context;

  public RealmCreatedHandler(IActorService actorService, DemoContext context)
  {
    _actorService = actorService;
    _context = context;
  }

  public async Task Handle(RealmCreated notification, CancellationToken cancellationToken)
  {
    ActorInfo actor = await _actorService.GetAsync(notification, cancellationToken);
    RealmEntity realm = new(notification, actor);

    _context.Realms.Add(realm);
    await _context.SaveChangesAsync(cancellationToken);
  }
}
