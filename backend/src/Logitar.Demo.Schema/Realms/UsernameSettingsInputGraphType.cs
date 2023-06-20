using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class UsernameSettingsInputGraphType : InputObjectGraphType<UsernameSettings>
{
  public UsernameSettingsInputGraphType()
  {
    Name = $"{nameof(UsernameSettings)}Input";
    Description = "Represents the settings of usernames in a realm.";

    Field(x => x.AllowedCharacters, nullable: true).Description("The list of allowed characters in usernames.");
  }
}
