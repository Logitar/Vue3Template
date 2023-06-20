using GraphQL.Types;
using Logitar.Demo.Core.Realms;

namespace Logitar.Demo.Schema.Realms;

internal class CreateRealmGraphType : InputObjectGraphType<CreateRealmInput>
{
  public CreateRealmGraphType()
  {
    Name = nameof(CreateRealmInput);
    Description = "The data used to create a new realm.";

    Field(x => x.UniqueName)
      .Description("The unique name of the realm.");
    Field(x => x.DisplayName, nullable: true)
      .Description("The display name of the realm.");
    Field(x => x.Description, nullable: true)
      .Description("The description of the realm.");

    Field(x => x.DefaultLocale, nullable: true)
      .Description("The default locale code of the realm.");
    Field(x => x.Secret, nullable: true)
      .Description("The JSON Web Token secret of the realm.");
    Field(x => x.Url, nullable: true)
      .Description("The URL to the application represented by the realm.");

    Field(x => x.RequireConfirmedAccount, nullable: true).DefaultValue(false)
      .Description("A value indicating whether or not users need confirmed accounts to sign-in.");
    Field(x => x.RequireUniqueEmail, nullable: true).DefaultValue(false)
      .Description("A value indicating whether or not email addresses are unique in the realm.");

    Field(x => x.UsernameSettings, type: typeof(UsernameSettingsInputGraphType))
      .Description("The settings applied to usernames in the realm.");
    Field(x => x.PasswordSettings, type: typeof(PasswordSettingsInputGraphType))
      .Description("The settings applied to passwords in the realm.");

    Field(x => x.ClaimMappings, type: typeof(ListGraphType<NonNullGraphType<ClaimMappingInputGraphType>>))
      .Description("The mappings of custom attributes to claims in the realm.");
    Field(x => x.CustomAttributes, type: typeof(ListGraphType<NonNullGraphType<CustomAttributeInputGraphType>>))
      .Description("The custom attributes of the realm.");
  }
}
