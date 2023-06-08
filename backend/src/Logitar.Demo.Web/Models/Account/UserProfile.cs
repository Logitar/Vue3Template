using Logitar.Portal.Contracts.Users;
using Logitar.Portal.Contracts.Users.Contact;

namespace Logitar.Demo.Web.Models.Account;

public record UserProfile
{
  public UserProfile(User user)
  {
    Username = user.Username;

    SignedInOn = user.SignedInOn;

    Address = user.Address;
    Email = user.Email ?? throw new ArgumentException($"The {nameof(user.Email)} is required.", nameof(user));
    Phone = user.Phone;

    FirstName = user.FirstName ?? throw new ArgumentException($"The {nameof(user.FirstName)} is required.", nameof(user));
    MiddleName = user.MiddleName;
    LastName = user.LastName ?? throw new ArgumentException($"The {nameof(user.LastName)} is required.", nameof(user));
    Nickname = user.Nickname;
    FullName = user.FullName ?? throw new ArgumentException($"The {nameof(user.FullName)} is required.", nameof(user));

    Birthdate = user.Birthdate;
    Gender = user.Gender;

    Locale = user.Locale ?? throw new ArgumentException($"The {nameof(user.Locale)} is required.", nameof(user));
    TimeZone = user.TimeZone;

    Picture = user.Picture;
    Profile = user.Profile;
    Website = user.Website;

    CreatedOn = user.CreatedOn;
    UpdatedOn = user.UpdatedOn;
  }

  public string Username { get; }

  public DateTime? SignedInOn { get; }

  public Address? Address { get; }
  public Email Email { get; }
  public Phone? Phone { get; }

  public string FirstName { get; }
  public string? MiddleName { get; }
  public string LastName { get; }
  public string? Nickname { get; }
  public string FullName { get; }

  public DateTime? Birthdate { get; }
  public string? Gender { get; }

  public string Locale { get; }
  public string? TimeZone { get; }

  public string? Picture { get; }
  public string? Profile { get; }
  public string? Website { get; }

  public DateTime CreatedOn { get; }
  public DateTime UpdatedOn { get; }
}
