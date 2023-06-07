using Logitar.Portal.Contracts.Users.Contact;

namespace Logitar.Demo.Web.Models.Account;

public record SaveProfilePayload
{
  public EmailInput Email { get; set; } = new();

  public string FirstName { get; set; } = string.Empty;
  public string? MiddleName { get; set; }
  public string LastName { get; set; } = string.Empty;
  public string? Nickname { get; set; }

  public string Locale { get; set; } = string.Empty;
  public string? TimeZone { get; set; }
}
