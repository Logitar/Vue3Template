using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class UsernameSettingsGraphType : ObjectGraphType<UsernameSettings>
{
  public UsernameSettingsGraphType()
  {
    Name = nameof(UsernameSettings);
    Description = "...";

    Field(x => x.AllowedCharacters, nullable: true).Description("...");
  }
}
