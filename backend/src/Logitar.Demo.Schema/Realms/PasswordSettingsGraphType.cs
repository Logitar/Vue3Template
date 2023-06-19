using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class PasswordSettingsGraphType : ObjectGraphType<PasswordSettings>
{
  public PasswordSettingsGraphType()
  {
    Name = nameof(PasswordSettings);
    Description = "...";

    Field(x => x.RequiredLength).Description("...");
    Field(x => x.RequiredUniqueChars).Description("...");
    Field(x => x.RequireNonAlphanumeric).Description("...");
    Field(x => x.RequireLowercase).Description("...");
    Field(x => x.RequireUppercase).Description("...");
    Field(x => x.RequireDigit).Description("...");
  }
}
