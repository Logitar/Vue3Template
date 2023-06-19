using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class ClaimMappingGraphType : ObjectGraphType<ClaimMapping>
{
  public ClaimMappingGraphType()
  {
    Name = nameof(ClaimMapping);
    Description = "...";

    Field(x => x.Key).Description("...");
    Field(x => x.Type).Description("...");
    Field(x => x.ValueType, nullable: true).Description("...");
  }
}
