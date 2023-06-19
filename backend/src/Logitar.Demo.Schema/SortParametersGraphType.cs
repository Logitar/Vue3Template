using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class SortParametersGraphType : InputObjectGraphType<SortParameters>
{
  public SortParametersGraphType()
  {
    Name = nameof(SortParameters);
    Description = "...";

    Field(x => x.Field).Description("...");
    Field(x => x.IsDescending).DefaultValue(false);
  }
}
