namespace Logitar.Demo.Core.Actors;

public record Actor
{
  public Guid Id { get; set; }
  public ActorType Type { get; set; }
  public bool IsDeleted { get; set; }

  public string DisplayName { get; set; } = ActorType.System.ToString();
  public string? EmailAddress { get; set; }
  public string? Picture { get; set; }
}
