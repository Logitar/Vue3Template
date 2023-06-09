using Logitar.EventSourcing;

namespace Logitar.Demo.Core.Realms;

public record ReadOnlyUsernameSettings : IUsernameSettings
{
  public string? AllowedCharacters { get; init; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

  public static ReadOnlyUsernameSettings? From(UsernameSettings? usernameSettings)
  {
    return usernameSettings == null ? null : new()
    {
      AllowedCharacters = usernameSettings.AllowedCharacters?.CleanTrim()
    };
  }
}
