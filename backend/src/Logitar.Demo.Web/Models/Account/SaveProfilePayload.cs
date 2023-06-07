using Logitar.Portal.Contracts.Users.Contact;

namespace Logitar.Demo.Web.Models.Account;

public record SaveProfilePayload
{
  public AddressInput? Address { get; set; }
  public EmailInput Email { get; set; } = new();
  public PhoneInput? Phone { get; set; }

  public string FirstName { get; set; } = string.Empty;
  public string? MiddleName { get; set; }
  public string LastName { get; set; } = string.Empty;
  public string? Nickname { get; set; }

  public DateTime? Birthdate { get; set; }
  public string? Gender { get; set; }

  public string Locale { get; set; } = string.Empty;
  public string? TimeZone { get; set; }

  public string? Picture { get; set; }
  public string? Profile { get; set; }
  public string? Website { get; set; }
}
