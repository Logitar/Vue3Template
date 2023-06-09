using MediatR;

namespace Logitar.Demo.Core.Realms.Commands;

internal class DeleteRealmHandler : IRequestHandler<DeleteRealm, Realm>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IRealmQuerier _realmQuerier;
  private readonly IRealmRepository _realmRepository;

  public DeleteRealmHandler(IApplicationContext applicationContext,
    IRealmQuerier realmQuerier,
    IRealmRepository realmRepository)
  {
    _applicationContext = applicationContext;
    _realmQuerier = realmQuerier;
    _realmRepository = realmRepository;
  }

  public async Task<Realm> Handle(DeleteRealm request, CancellationToken cancellationToken)
  {
    RealmAggregate realm = await _realmRepository.LoadAsync(request.Id, cancellationToken)
      ?? throw new AggregateNotFoundException<RealmAggregate>(request.Id);
    Realm output = await _realmQuerier.GetAsync(realm, cancellationToken);

    realm.Delete(_applicationContext.ActorId);

    await _realmRepository.SaveAsync(realm, cancellationToken);

    return output;
  }
}
