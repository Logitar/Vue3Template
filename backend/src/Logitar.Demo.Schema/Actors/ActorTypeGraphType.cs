using GraphQL.Types;
using Logitar.Demo.Core.Actors;

namespace Logitar.Demo.Schema.Actors;

internal class ActorTypeGraphType : EnumerationGraphType<ActorType>
{
  public ActorTypeGraphType()
  {
    Name = nameof(ActorType);
    Description = "...";

    Add(new EnumValueDefinition(nameof(ActorType.System), ActorType.System)
    {
      Description = "..."
    });
    Add(new EnumValueDefinition(nameof(ActorType.User), ActorType.User)
    {
      Description = "..."
    });
  }
}
