using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class ClaimMappingGraphType : ObjectGraphType<ClaimMapping>
{
  public ClaimMappingGraphType()
  {
    Name = nameof(ClaimMapping);
    Description = "Represents a mapping of a custom attribute to a claim.";

    Field(x => x.Key).Description("The unique key of the custom attribute.");
    Field(x => x.Type).Description("The type (or name) of the claim.");
    Field(x => x.ValueType, nullable: true).Description("The type of the claim values.");
  }
}
