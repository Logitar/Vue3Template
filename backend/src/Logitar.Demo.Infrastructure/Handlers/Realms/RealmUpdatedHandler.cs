using Logitar.Demo.Core.Realms.Events;
using Logitar.Demo.Infrastructure.Actors;
using Logitar.Demo.Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Demo.Infrastructure.Handlers.Realms;

internal class RealmUpdatedHandler : INotificationHandler<RealmUpdated>
{
  private readonly IActorService _actorService;
  private readonly DemoContext _context;

  public RealmUpdatedHandler(IActorService actorService, DemoContext context)
  {
    _actorService = actorService;
    _context = context;
  }

  public async Task Handle(RealmUpdated notification, CancellationToken cancellationToken)
  {
    RealmEntity realm = await _context.Realms
      .SingleOrDefaultAsync(x => x.AggregateId == notification.AggregateId.Value, cancellationToken)
      ?? throw new InvalidOperationException($"The realm entity '{notification.AggregateId}' could not be found.");

    ActorInfo actor = await _actorService.GetAsync(notification, cancellationToken);
    realm.Update(notification, actor);

    await _context.SaveChangesAsync(cancellationToken);
  }
}
