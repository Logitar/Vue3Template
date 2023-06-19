using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class CustomAttributeGraphType : ObjectGraphType<CustomAttribute>
{
  public CustomAttributeGraphType()
  {
    Name = nameof(CustomAttribute);
    Description = "...";

    Field(x => x.Key).Description("...");
    Field(x => x.Value).Description("...");
  }
}
