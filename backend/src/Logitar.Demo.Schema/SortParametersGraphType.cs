using GraphQL.Types;
using Logitar.Demo.Core;

namespace Logitar.Demo.Schema;

internal class SortParametersGraphType : InputObjectGraphType<SortParameters>
{
  public SortParametersGraphType()
  {
    Name = nameof(SortParameters);
    Description = "Represents the options available to sort search results.";

    Field(x => x.Field)
      .Description("The field to use to sort the results.");
    Field(x => x.IsDescending).DefaultValue(false)
      .Description("A value indicating whether or not the field will be used to reverse-sort the results.");
  }
}
