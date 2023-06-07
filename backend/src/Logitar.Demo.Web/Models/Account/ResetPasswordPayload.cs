namespace Logitar.Demo.Web.Models.Account;

public record ResetPasswordPayload
{
  public string Token { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
}
