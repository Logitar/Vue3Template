namespace Logitar.Demo.Core.Realms;

public interface IRealmQuerier
{
  Task<Realm> GetAsync(RealmAggregate realm, CancellationToken cancellationToken = default);
  Task<Realm?> GetAsync(Guid id, CancellationToken cancellationToken = default);
  Task<Realm?> GetAsync(string uniqueName, CancellationToken cancellationToken = default);
  Task<SearchResults<Realm>> SearchAsync(RealmSearchParameters parameters, CancellationToken cancellationToken = default);
}
