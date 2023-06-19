using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class RealmGraphType : AggregateGraphType<Realm>
{
  public RealmGraphType()
  {
    Name = nameof(Realm);
    Description = "...";

    Field(x => x.Id).Description("...");

    Field(x => x.UniqueName).Description("...");
    Field(x => x.DisplayName, nullable: true).Description("...");
    Field(x => x.Description, nullable: true).Description("...");

    Field(x => x.DefaultLocale, nullable: true).Description("...");
    Field(x => x.Secret).Description("...");
    Field(x => x.Url, nullable: true).Description("...");

    Field(x => x.RequireConfirmedAccount).Description("...");
    Field(x => x.RequireUniqueEmail).Description("...");

    Field(x => x.UsernameSettings, type: typeof(NonNullGraphType<UsernameSettingsGraphType>)).Description("...");
    Field(x => x.PasswordSettings, type: typeof(NonNullGraphType<PasswordSettingsGraphType>)).Description("...");

    Field(x => x.ClaimMappings, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<ClaimMappingGraphType>>>)).Description("...");
    Field(x => x.CustomAttributes, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<CustomAttributeGraphType>>>)).Description("...");
  }
}
