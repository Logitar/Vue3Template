using GraphQL.Types;
using Logitar.Demo.Core;
using Logitar.Demo.Schema.Actors;

namespace Logitar.Demo.Schema;

internal abstract class AggregateGraphType<T> : ObjectGraphType<T> where T : Aggregate
{
  public AggregateGraphType()
  {
    Field(x => x.CreatedBy, type: typeof(NonNullGraphType<ActorGraphType>)).Description("The actor who created the entity.");
    Field(x => x.CreatedOn).Description("The date and time when the entity was created.");
    Field(x => x.UpdatedBy, type: typeof(NonNullGraphType<ActorGraphType>)).Description("The actor who updated the entity lately.");
    Field(x => x.UpdatedOn).Description("The date and time when the entity was updated lately.");
    Field(x => x.Version).Description("The ordinal version of the entity.");
  }
}
