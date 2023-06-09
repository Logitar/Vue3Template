using Logitar.Demo.Core.Actors;

namespace Logitar.Demo.Web.Models.Account;

public abstract record Contact
{
  public Actor? VerifiedBy { get; set; }
  public DateTime? VerifiedOn { get; set; }
  public bool IsVerified { get; set; }
}
