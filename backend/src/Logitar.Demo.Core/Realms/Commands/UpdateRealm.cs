using MediatR;

namespace Logitar.Demo.Core.Realms.Commands;

internal record UpdateRealm(Guid Id, UpdateRealmInput Input) : IRequest<Realm>;
