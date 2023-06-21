using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class CustomAttributeGraphType : ObjectGraphType<CustomAttribute>
{
  public CustomAttributeGraphType()
  {
    Name = nameof(CustomAttribute);
    Description = "Represents a custom attribute.";

    Field(x => x.Key).Description("The unique key of the custom attribute.");
    Field(x => x.Value).Description("The value of the custom attribute.");
  }
}
