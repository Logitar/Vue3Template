using MediatR;

namespace Logitar.Demo.Core.Realms.Commands;

internal record CreateRealm(CreateRealmInput Input) : IRequest<Realm>;
