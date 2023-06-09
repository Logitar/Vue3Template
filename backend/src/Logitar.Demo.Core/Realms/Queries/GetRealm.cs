using MediatR;

namespace Logitar.Demo.Core.Realms.Queries;

internal record GetRealm(Guid? Id, string? UniqueName) : IRequest<Realm?>;
