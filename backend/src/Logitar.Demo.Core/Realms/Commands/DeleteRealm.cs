using MediatR;

namespace Logitar.Demo.Core.Realms.Commands;

internal record DeleteRealm(Guid Id) : IRequest<Realm>;
