using Logitar.Demo.Core.Realms;
using Logitar.EventSourcing;
using Logitar.EventSourcing.EntityFrameworkCore.PostgreSQL;
using Logitar.EventSourcing.EntityFrameworkCore.PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Demo.Infrastructure.Repositories;

internal class RealmRepository : EventStore, IRealmRepository
{
  private static string AggregateType { get; } = typeof(RealmAggregate).GetName();

  public RealmRepository(EventContext context, IEventBus eventBus) : base(context, eventBus)
  {
  }

  public async Task<RealmAggregate?> LoadAsync(Guid id, CancellationToken cancellationToken)
  {
    return await LoadAsync<RealmAggregate>(new AggregateId(id), cancellationToken);
  }

  public async Task<RealmAggregate?> LoadByUniqueNameAsync(string uniqueName, CancellationToken cancellationToken)
  {
    EventEntity[] events = await Context.Events.FromSqlInterpolated($@"SELECT e.* FROM ""Events"" e JOIN ""Realms"" r on r.""AggregateId"" = e.""AggregateId"" WHERE e.""AggregateType"" = {AggregateType} AND r.""UniqueNameNormalized"" = {uniqueName.ToUpper()}")
      .AsNoTracking()
      .OrderBy(x => x.Version)
      .ToArrayAsync(cancellationToken);

    return Load<RealmAggregate>(events).SingleOrDefault();
  }

  public async Task SaveAsync(RealmAggregate realm, CancellationToken cancellationToken)
  {
    await base.SaveAsync(realm, cancellationToken);
  }
}
