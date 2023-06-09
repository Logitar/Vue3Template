using MediatR;

namespace Logitar.Demo.Core.Realms.Queries;

public record SearchRealms(RealmSearchParameters Parameters) : IRequest<SearchResults<Realm>>;
