namespace Logitar.Demo.Core.Realms;

public record ReadOnlyPasswordSettings : IPasswordSettings
{
  public int RequiredLength { get; init; } = 6;
  public int RequiredUniqueChars { get; init; } = 1;
  public bool RequireNonAlphanumeric { get; init; } = false;
  public bool RequireLowercase { get; init; } = true;
  public bool RequireUppercase { get; init; } = true;
  public bool RequireDigit { get; init; } = true;

  public static ReadOnlyPasswordSettings? From(PasswordSettings? passwordSettings)
  {
    return passwordSettings == null ? null : new()
    {
      RequiredLength = passwordSettings.RequiredLength,
      RequiredUniqueChars = passwordSettings.RequiredUniqueChars,
      RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric,
      RequireLowercase = passwordSettings.RequireLowercase,
      RequireUppercase = passwordSettings.RequireUppercase,
      RequireDigit = passwordSettings.RequireDigit
    };
  }
}
