namespace Logitar.Demo.Web.Models.Account;

public record RecoverPasswordPayload
{
  public string Username { get; set; } = string.Empty;
}
