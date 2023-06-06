namespace Logitar.Demo.Web.Models.Account;

public record RegisterPayload
{
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;

  public string EmailAddress { get; set; } = string.Empty;

  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;

  public string Locale { get; set; } = string.Empty;
}
