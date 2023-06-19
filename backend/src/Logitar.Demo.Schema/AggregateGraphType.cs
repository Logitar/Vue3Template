using GraphQL.Types;
using Logitar.Demo.Core;
using Logitar.Demo.Schema.Actors;

namespace Logitar.Demo.Schema;

internal abstract class AggregateGraphType<T> : ObjectGraphType<T> where T : Aggregate
{
  public AggregateGraphType()
  {
    Field(x => x.CreatedBy, type: typeof(NonNullGraphType<ActorGraphType>)).Description("...");
    Field(x => x.CreatedOn).Description("...");
    Field(x => x.UpdatedBy, type: typeof(NonNullGraphType<ActorGraphType>)).Description("...");
    Field(x => x.UpdatedOn).Description("...");
    Field(x => x.Version).Description("...");
  }
}
