using MediatR;
using System.Globalization;

namespace Logitar.Demo.Core.Realms.Commands;

internal class UpdateRealmHandler : IRequestHandler<UpdateRealm, Realm>
{
  private readonly IApplicationContext _applicationContext;
  private readonly IRealmQuerier _realmQuerier;
  private readonly IRealmRepository _realmRepository;

  public UpdateRealmHandler(IApplicationContext applicationContext,
    IRealmQuerier realmQuerier,
    IRealmRepository realmRepository)
  {
    _applicationContext = applicationContext;
    _realmQuerier = realmQuerier;
    _realmRepository = realmRepository;
  }

  public async Task<Realm> Handle(UpdateRealm request, CancellationToken cancellationToken)
  {
    RealmAggregate realm = await _realmRepository.LoadAsync(request.Id, cancellationToken)
      ?? throw new AggregateNotFoundException<RealmAggregate>(request.Id);

    UpdateRealmInput input = request.Input;

    CultureInfo? defaultLocale = input.DefaultLocale?.GetCultureInfo(nameof(input.DefaultLocale));
    Uri? url = input.Url?.GetUri(nameof(input.Url));
    ReadOnlyUsernameSettings? usernameSettings = ReadOnlyUsernameSettings.From(input.UsernameSettings);
    ReadOnlyPasswordSettings? passwordSettings = ReadOnlyPasswordSettings.From(input.PasswordSettings);

    realm.Update(_applicationContext.ActorId, input.DisplayName, input.Description,
      defaultLocale, input.Secret, url,
      input.RequireConfirmedAccount, input.RequireUniqueEmail, usernameSettings, passwordSettings,
      input.ClaimMappings?.ToDictionary(), input.CustomAttributes?.ToDictionary());

    await _realmRepository.SaveAsync(realm, cancellationToken);

    return await _realmQuerier.GetAsync(realm, cancellationToken);
  }
}
