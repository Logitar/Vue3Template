using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class RealmGraphType : AggregateGraphType<Realm>
{
  public RealmGraphType()
  {
    Name = nameof(Realm);
    Description = "Represents a realm, the root entity in the identity system, containing the other entities.";

    Field(x => x.Id)
      .Description("The unique identifier of the realm.");

    Field(x => x.UniqueName)
      .Description("The unique name of the realm.");
    Field(x => x.DisplayName, nullable: true)
      .Description("The display name of the realm.");
    Field(x => x.Description, nullable: true)
      .Description("The description of the realm.");

    Field(x => x.DefaultLocale, nullable: true)
      .Description("The default locale code of the realm.");
    Field(x => x.Secret)
      .Description("The JSON Web Token secret of the realm.");
    Field(x => x.Url, nullable: true)
      .Description("The URL to the application represented by the realm.");

    Field(x => x.RequireConfirmedAccount)
      .Description("A value indicating whether or not users need confirmed accounts to sign-in.");
    Field(x => x.RequireUniqueEmail)
      .Description("A value indicating whether or not email addresses are unique in the realm.");

    Field(x => x.UsernameSettings, type: typeof(NonNullGraphType<UsernameSettingsGraphType>))
      .Description("The settings applied to usernames in the realm.");
    Field(x => x.PasswordSettings, type: typeof(NonNullGraphType<PasswordSettingsGraphType>))
      .Description("The settings applied to passwords in the realm.");

    Field(x => x.ClaimMappings, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<ClaimMappingGraphType>>>))
      .Description("The mappings of custom attributes to claims in the realm.");
    Field(x => x.CustomAttributes, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<CustomAttributeGraphType>>>))
      .Description("The custom attributes of the realm.");
  }
}
