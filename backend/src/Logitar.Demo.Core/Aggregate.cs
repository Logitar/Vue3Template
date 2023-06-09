using Logitar.Demo.Core.Actors;

namespace Logitar.Demo.Core;

public abstract record Aggregate
{
  public Actor CreatedBy { get; set; } = new();
  public DateTime CreatedOn { get; set; }

  public Actor UpdatedBy { get; set; } = new();
  public DateTime UpdatedOn { get; set; }

  public long Version { get; set; }
}
