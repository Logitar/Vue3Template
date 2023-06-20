using GraphQL;
using GraphQL.Types;
using Logitar.Demo.Schema.Realms;

namespace Logitar.Demo.Schema;

internal class RootMutation : ObjectGraphType
{
  public RootMutation()
  {
    Name = nameof(RootMutation);

    this.Authorize();

    RealmMutations.Register(this);
  }
}
