using GraphQL;
using GraphQL.Types;
using Logitar.Demo.Schema.Realms;

namespace Logitar.Demo.Schema;

internal class RootQuery : ObjectGraphType
{
  public RootQuery()
  {
    Name = nameof(RootQuery);

    this.Authorize();

    RealmQueries.Register(this);
  }
}
