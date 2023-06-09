namespace Logitar.Demo.Web.Models.Account;

public record UserProfile
{
  public string Username { get; set; } = string.Empty;

  public DateTime PasswordChangedOn { get; set; }

  public DateTime? SignedInOn { get; set; }

  public Address? Address { get; set; }
  public Email Email { get; set; } = new();
  public Phone? Phone { get; set; }

  public string FirstName { get; set; } = string.Empty;
  public string? MiddleName { get; set; }
  public string LastName { get; set; } = string.Empty;
  public string? Nickname { get; set; }
  public string FullName { get; set; } = string.Empty;

  public DateTime? Birthdate { get; set; }
  public string? Gender { get; set; }

  public string Locale { get; set; } = string.Empty;
  public string? TimeZone { get; set; }

  public string? Picture { get; set; }
  public string? Profile { get; set; }
  public string? Website { get; set; }

  public DateTime CreatedOn { get; set; }
  public DateTime UpdatedOn { get; set; }
}
