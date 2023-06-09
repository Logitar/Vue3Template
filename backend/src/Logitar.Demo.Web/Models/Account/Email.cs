namespace Logitar.Demo.Web.Models.Account;

public record Email : Contact
{
  public string Address { get; set; } = string.Empty;
}
