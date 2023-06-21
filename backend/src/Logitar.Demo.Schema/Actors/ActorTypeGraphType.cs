using GraphQL.Types;
using Logitar.Demo.Core.Actors;
using Logitar.Demo.Schema.Extensions;

namespace Logitar.Demo.Schema.Actors;

internal class ActorTypeGraphType : EnumerationGraphType<ActorType>
{
  public ActorTypeGraphType()
  {
    Name = nameof(ActorType);
    Description = "Defines the different actor types.";

    this.AddValue(ActorType.System, "The actor is the system.");
    this.AddValue(ActorType.User, "The actor is an user.");
  }
}
