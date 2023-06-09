namespace Logitar.Demo.Web.Models.Account;

public record Address : Contact
{
  public string Line1 { get; set; } = string.Empty;
  public string? Line2 { get; set; }
  public string Locality { get; set; } = string.Empty;
  public string? PostalCode { get; set; }
  public string Country { get; set; } = string.Empty;
  public string? Region { get; set; }
  public string Formatted { get; set; } = string.Empty;
}
