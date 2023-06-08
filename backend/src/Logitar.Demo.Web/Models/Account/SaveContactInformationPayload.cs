using Logitar.Portal.Contracts.Users.Contact;

namespace Logitar.Demo.Web.Models.Account;

public record SaveContactInformationPayload
{
  public AddressInput? Address { get; set; }
  public EmailInput Email { get; set; } = new();
  public PhoneInput? Phone { get; set; }
}
