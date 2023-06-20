using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class RealmSortParametersGraphType : InputObjectGraphType<RealmSortParameters>
{
  public RealmSortParametersGraphType()
  {
    Name = nameof(RealmSortParameters);
    Description = "Represents the options available to sort realm search results.";

    Field(x => x.Field, type: typeof(NonNullGraphType<RealmSortGraphType>))
      .Description("The field to use to sort the results.");
    Field(x => x.IsDescending).DefaultValue(false)
      .Description("A value indicating whether or not the field will be used to reverse-sort the results.");
  }
}
