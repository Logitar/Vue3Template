using GraphQL.Types;
using Logitar.Demo.Core.Actors;

namespace Logitar.Demo.Schema.Actors;

internal class ActorGraphType : ObjectGraphType<Actor>
{
  public ActorGraphType()
  {
    Name = nameof(Actor);
    Description = "...";

    Field(x => x.Id).Description("...");
    Field(x => x.Type, type: typeof(NonNullGraphType<ActorTypeGraphType>)).Description("...");
    Field(x => x.IsDeleted).Description("...");

    Field(x => x.DisplayName).Description("...");
    Field(x => x.EmailAddress, nullable: true).Description("...");
    Field(x => x.Picture, nullable: true).Description("...");
  }
}
