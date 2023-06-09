using Logitar.Demo.Core;
using Logitar.Demo.Infrastructure.Entities;
using Logitar.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Demo.Infrastructure.Actors;

internal class ActorService : IActorService
{
  private readonly IApplicationContext _applicationContext;
  private readonly DbSet<ActorEntity> _actors;

  public ActorService(IApplicationContext applicationContext, DemoContext context)
  {
    _applicationContext = applicationContext;
    _actors = context.Actors;
  }

  public async Task<ActorInfo> GetAsync(DomainEvent change, CancellationToken cancellationToken)
  {
    if (change.ActorId == _applicationContext.ActorId)
    {
      return ActorInfo.From(_applicationContext.Actor);
    }

    Guid id = change.ActorId.ToGuid();

    ActorEntity actor = await _actors.AsNoTracking()
      .SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
      ?? throw new InvalidOperationException($"The actor '{id}' could not be found.");

    return ActorInfo.From(actor);
  }
}
