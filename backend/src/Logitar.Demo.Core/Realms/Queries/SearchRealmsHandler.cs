using MediatR;

namespace Logitar.Demo.Core.Realms.Queries;

internal class SearchRealmsHandler : IRequestHandler<SearchRealms, SearchResults<Realm>>
{
  private readonly IRealmQuerier _realmQuerier;

  public SearchRealmsHandler(IRealmQuerier realmQuerier)
  {
    _realmQuerier = realmQuerier;
  }

  public async Task<SearchResults<Realm>> Handle(SearchRealms request, CancellationToken cancellationToken)
  {
    return await _realmQuerier.SearchAsync(request.Parameters, cancellationToken);
  }
}
