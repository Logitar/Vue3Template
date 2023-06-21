using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class CustomAttributeInputGraphType : InputObjectGraphType<CustomAttribute>
{
  public CustomAttributeInputGraphType()
  {
    Name = $"{nameof(CustomAttribute)}Input";
    Description = "Represents a custom attribute.";

    Field(x => x.Key).Description("The unique key of the custom attribute.");
    Field(x => x.Value).Description("The value of the custom attribute.");
  }
}
