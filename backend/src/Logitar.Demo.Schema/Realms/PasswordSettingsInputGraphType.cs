using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class PasswordSettingsInputGraphType : InputObjectGraphType<PasswordSettings>
{
  public PasswordSettingsInputGraphType()
  {
    Name = $"{nameof(PasswordSettings)}Input";
    Description = "Represents the settings of usernames in a realm.";

    Field(x => x.RequiredLength).Description("The minimum number of characters passwords must include.");
    Field(x => x.RequiredUniqueChars).Description("The number of unique characters passwords must include.");
    Field(x => x.RequireNonAlphanumeric).Description("A value indicating whether or not passwords must contain a non-alphanumeric character.");
    Field(x => x.RequireLowercase).Description("A value indicating whether or not passwords must contain a lowercase letter.");
    Field(x => x.RequireUppercase).Description("A value indicating whether or not passwords must contain an uppercase letter.");
    Field(x => x.RequireDigit).Description("A value indicating whether or not passwords must contain a digit.");
  }
}
