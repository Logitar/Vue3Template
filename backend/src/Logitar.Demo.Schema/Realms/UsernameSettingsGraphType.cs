using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class UsernameSettingsGraphType : ObjectGraphType<UsernameSettings>
{
  public UsernameSettingsGraphType()
  {
    Name = nameof(UsernameSettings);
    Description = "Represents the settings of usernames in a realm.";

    Field(x => x.AllowedCharacters, nullable: true).Description("The list of allowed characters in usernames.");
  }
}
