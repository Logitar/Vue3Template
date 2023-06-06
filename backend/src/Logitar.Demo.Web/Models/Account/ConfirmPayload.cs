namespace Logitar.Demo.Web.Models.Account;

public record ConfirmPayload
{
  public string Token { get; set; } = string.Empty;
}
