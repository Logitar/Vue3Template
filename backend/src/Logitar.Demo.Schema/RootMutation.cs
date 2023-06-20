using GraphQL;
using GraphQL.Types;

namespace Logitar.Demo.Schema;

internal class RootMutation : ObjectGraphType
{
  public RootMutation()
  {
    Name = nameof(RootMutation);

    this.Authorize();
  }
}
