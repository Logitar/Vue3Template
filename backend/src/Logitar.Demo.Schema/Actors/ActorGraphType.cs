using GraphQL.Types;
using Logitar.Demo.Core.Actors;

namespace Logitar.Demo.Schema.Actors;

internal class ActorGraphType : ObjectGraphType<Actor>
{
  public ActorGraphType()
  {
    Name = nameof(Actor);
    Description = "Represents an actor who performed one or more operations.";

    Field(x => x.Id)
      .Description("The unique identifier of the actor.");
    Field(x => x.Type, type: typeof(NonNullGraphType<ActorTypeGraphType>))
      .Description("The type of the actor.");
    Field(x => x.IsDeleted)
      .Description("A value indicating whether or not the actor is deleted.");

    Field(x => x.DisplayName)
      .Description("The display name of the actor.");
    Field(x => x.EmailAddress, nullable: true)
      .Description("The email address of the actor.");
    Field(x => x.Picture, nullable: true)
      .Description("The URL to the picture of the actor.");
  }
}
