using Logitar.Demo.Core.Realms.Commands;
using Logitar.Demo.Core.Realms.Queries;
using MediatR;

namespace Logitar.Demo.Core.Realms;

internal class RealmService : IRealmService
{
  private readonly IMediator _mediator;

  public RealmService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<Realm> CreateAsync(CreateRealmInput input, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new CreateRealm(input), cancellationToken);
  }

  public async Task<Realm> DeleteAsync(Guid id, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new DeleteRealm(id), cancellationToken);
  }

  public async Task<Realm?> GetAsync(Guid? id, string? uniqueName, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new GetRealm(id, uniqueName), cancellationToken);
  }

  public async Task<SearchResults<Realm>> SearchAsync(RealmSearchParameters parameters, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SearchRealms(parameters), cancellationToken);
  }

  public async Task<Realm> UpdateAsync(Guid id, UpdateRealmInput input, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new UpdateRealm(id, input), cancellationToken);
  }
}
