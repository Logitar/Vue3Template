namespace Logitar.Demo.Core.Realms;

public interface IRealmService
{
  Task<Realm> CreateAsync(CreateRealmInput input, CancellationToken cancellationToken = default);
  Task<Realm> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  Task<Realm?> GetAsync(Guid? id = null, string? uniqueName = null, CancellationToken cancellationToken = default);
  Task<SearchResults<Realm>> SearchAsync(RealmSearchParameters parameters, CancellationToken cancellationToken = default);
  Task<Realm> UpdateAsync(Guid id, UpdateRealmInput input, CancellationToken cancellationToken = default);
}
